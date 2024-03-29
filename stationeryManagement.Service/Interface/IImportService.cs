using stationeryManagement.Data.Dto;
using stationeryManagement.Data;
using stationeryManagement.Data.Model;

namespace stationeryManagement.Service.Interface
{
    public interface IImportService
    {
        Task<Import?> CreateImport(ImportDto import,Guid id);
        // Read
        Task<Import?> GetImportId(int ImportId);
        Task<IEnumerable<Import>> GetAllImport();

        // Update
        Task<bool> UpdateImport(ImportDto import, Guid id,int ImportId);

        // Delete
        Task<bool> DeleteImport(int ImportId);
    }
}
