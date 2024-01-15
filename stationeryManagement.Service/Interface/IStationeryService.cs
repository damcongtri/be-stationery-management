using stationeryManagement.Data.Dto;
using stationeryManagement.Data.Model;

namespace stationeryManagement.Service.Interface
{
    public interface IStationeryService
    {
        Task<Stationery> CreateStationery(StationeryDto stationery);

        // Read
        Task<Stationery?> GetStationeryById(int itemnId);
        Task<IEnumerable<Stationery>> GetAllStationery();

        // Update
        Task<bool> UpdateStationery(StationeryDto stationery, int itemid);

        // Delete
        Task<bool> DeleteStationery(int itemId);
    }
}
