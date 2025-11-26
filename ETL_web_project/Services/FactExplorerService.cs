using ETL_web_project.Data.Context;
using ETL_web_project.DTOs;
using ETL_web_project.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ETL_web_project.Services
{
    public class FactExplorerService : IFactExplorerService
    {
        private readonly ProjectContext _context;

        public FactExplorerService(ProjectContext context)
        {
            _context = context;
        }

        public async Task<FactExplorerPageDto> GetFactExplorerAsync(
            DateTime? fromDate,
            DateTime? toDate,
            string? storeSearch,
            string? productSearch,
            string? customerSearch)
        {
            // FactSales + Dim join
            var query = _context.FactSales
                .Include(f => f.Date)
                .Include(f => f.Store)
                .Include(f => f.Product)
                .Include(f => f.Customer)
                .AsQueryable();

            // --------- FILTERS ----------
            if (fromDate.HasValue)
                query = query.Where(f => f.Date.Date >= fromDate.Value.Date);

            if (toDate.HasValue)
                query = query.Where(f => f.Date.Date <= toDate.Value.Date);

            if (!string.IsNullOrWhiteSpace(storeSearch))
            {
                var term = storeSearch.Trim();
                query = query.Where(f =>
                    f.Store.StoreName.Contains(term) ||
                    f.Store.StoreCode.Contains(term));
            }

            if (!string.IsNullOrWhiteSpace(productSearch))
            {
                var term = productSearch.Trim();
                query = query.Where(f =>
                    f.Product.ProductName.Contains(term) ||
                    f.Product.ProductCode.Contains(term));
            }

            if (!string.IsNullOrWhiteSpace(customerSearch))
            {
                var term = customerSearch.Trim();
                query = query.Where(f =>
                    f.Customer != null &&
                    (f.Customer.FullName.Contains(term) ||
                     f.Customer.CustomerCode.Contains(term)));
            }

            // --------- SUMMARY ----------
            var totalSales = await query.SumAsync(f => (decimal?)f.TotalAmount) ?? 0m;
            var totalQuantity = await query.SumAsync(f => (int?)f.Quantity) ?? 0;

            var distinctStores = await query
                .Select(f => f.StoreKey)
                .Distinct()
                .CountAsync();

            var distinctProducts = await query
                .Select(f => f.ProductKey)
                .Distinct()
                .CountAsync();

            var distinctCustomers = await query
                .Where(f => f.CustomerKey != null)
                .Select(f => f.CustomerKey!.Value)
                .Distinct()
                .CountAsync();

            var avgOrderValue = distinctCustomers > 0
                ? totalSales / distinctCustomers
                : 0m;

            var summary = new FactSummaryDto
            {
                TotalSales = totalSales,
                TotalQuantity = totalQuantity,
                DistinctStores = distinctStores,
                DistinctProducts = distinctProducts,
                DistinctCustomers = distinctCustomers,
                AvgOrderValue = avgOrderValue
            };

            // --------- SALES TREND (chart) ----------
            var salesTrend = await query
                .GroupBy(f => f.Date.Date)
                .Select(g => new SalesTrendPointDto
                {
                    Date = g.Key,
                    Amount = g.Sum(x => x.TotalAmount)
                })
                .OrderBy(x => x.Date)
                .ToListAsync();

            // --------- TOP STORES / PRODUCTS / CUSTOMERS ----------
            var topStores = await query
                .GroupBy(f => f.Store.StoreName)
                .Select(g => new TopEntityDto
                {
                    Name = g.Key,
                    TotalAmount = g.Sum(x => x.TotalAmount)
                })
                .OrderByDescending(x => x.TotalAmount)
                .Take(5)
                .ToListAsync();

            var topProducts = await query
                .GroupBy(f => f.Product.ProductName)
                .Select(g => new TopEntityDto
                {
                    Name = g.Key,
                    TotalAmount = g.Sum(x => x.TotalAmount)
                })
                .OrderByDescending(x => x.TotalAmount)
                .Take(5)
                .ToListAsync();

            var topCustomers = await query
                .Where(f => f.Customer != null)
                .GroupBy(f => f.Customer!.FullName)
                .Select(g => new TopEntityDto
                {
                    Name = g.Key,
                    TotalAmount = g.Sum(x => x.TotalAmount)
                })
                .OrderByDescending(x => x.TotalAmount)
                .Take(5)
                .ToListAsync();

            // --------- FACT RECORDS TABLE ----------
            var records = await query
                .OrderByDescending(f => f.Date.Date)
                .ThenByDescending(f => f.TotalAmount)
                .Take(200)
                .Select(f => new FactRecordDto
                {
                    Date = f.Date.Date,
                    Store = f.Store.StoreName,
                    Product = f.Product.ProductName,
                    Customer = f.Customer != null ? f.Customer.FullName : "(Unknown)",
                    Quantity = f.Quantity,
                    TotalAmount = f.TotalAmount
                })
                .ToListAsync();

            // --------- PAGE DTO ----------
            return new FactExplorerPageDto
            {
                FromDate = fromDate,
                ToDate = toDate,
                StoreSearch = storeSearch,
                ProductSearch = productSearch,
                CustomerSearch = customerSearch,
                Summary = summary,
                SalesTrend = salesTrend,
                TopStores = topStores,
                TopProducts = topProducts,
                TopCustomers = topCustomers,
                Records = records
            };
        }
    }
}
