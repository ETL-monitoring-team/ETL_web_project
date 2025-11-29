using ETL_web_project.DTOs.FactExplorer;

namespace ETL_web_project.Interfaces
{
    public interface IFactExplorerService
    {
        Task<FactExplorerPageDto> GetFactExplorerAsync(DateTime? fromDate, DateTime? toDate, string? storeSearch, string? productSearch, string? customerSearch);
    }
}
