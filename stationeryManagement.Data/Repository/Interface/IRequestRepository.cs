using stationeryManagement.Data.Common.BaseRepository;
using stationeryManagement.Data.Model;

namespace stationeryManagement.Data.Repository.Interface;

public interface IRequestRepository:IGenericRepository<Request>
{
    // Task<Request> AddWithDetailAsync(Request request);
    IQueryable<Request> GetWithDetailAsync();
}