using stationeryManagement.Data.Common.BaseRepository;
using stationeryManagement.Data.Model;

namespace stationeryManagement.Data.Repository.Interface;

public interface IUserRepository : IGenericRepository<User>
{
    IQueryable<User> GetUserWithRole();
}