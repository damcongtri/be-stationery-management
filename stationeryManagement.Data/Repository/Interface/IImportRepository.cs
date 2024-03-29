using System.Linq.Expressions;
using stationeryManagement.Data.Common.BaseRepository;
using stationeryManagement.Data.Model;

namespace stationeryManagement.Data.Repository.Interface
{
    public interface IImportRepository : IGenericRepository<Import>
    {
        IQueryable<Import> GetWithUser();
    }
}
