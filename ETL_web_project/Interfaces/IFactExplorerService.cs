using ETL_web_project.DTOs;

namespace ETL_web_project.Interfaces
{
    public interface IFactExplorerService
    {
        /// <summary>
        /// FactSales + Dim tablolarından Fact Explorer sayfası için
        /// tüm özet, trend, top list ve kayıtları üretir.
        /// </summary>
        Task<FactExplorerPageDto> GetFactExplorerAsync(
            DateTime? fromDate,
            DateTime? toDate,
            string? storeSearch,
            string? productSearch,
            string? customerSearch);
    }
}
