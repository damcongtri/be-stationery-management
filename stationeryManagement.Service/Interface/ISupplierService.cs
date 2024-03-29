using stationeryManagement.Data;
using stationeryManagement.Data.Dto;
using stationeryManagement.Data.Dto.SupplierDto;
using stationeryManagement.Data.Model;

namespace stationeryManagement.Service.Interface
{
    public interface ISupplierService
    {
        // Create
        Task<Supplier> CreateSupplier(SupplierDto supplier);
        // Read
        Task<Supplier?> GetSupplierById(int supplierId);
        Task<IEnumerable<Supplier>> GetAllSupplier();

        // Update
        Task<bool> UpdateSupplier(SupplierDto supplier, int id);

        // Delete
        Task<bool> DeleteSupplier(int supplierId);
    }
}
