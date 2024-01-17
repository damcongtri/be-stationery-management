using stationeryManagement.Data.Common.BaseRepository;
using stationeryManagement.Data.Common.DbContext;
using stationeryManagement.Data.Model;
using stationeryManagement.Data.Repository.Interface;

namespace stationeryManagement.Data.Repository;

public class ImportDetailRepository : GenericRepository<ImportDetail>, IImportDetailRepository
{
    public ImportDetailRepository(IDbContext context) : base(context)
    {
    }
}