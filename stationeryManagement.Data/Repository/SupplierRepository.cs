using stationeryManagement.Data.Common.BaseRepository;
using stationeryManagement.Data.Common.DbContext;
using stationeryManagement.Data.Repository.Interface;

namespace stationeryManagement.Data.Repository
{
    public class SupplierRepository: GenericRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(IDbContext context) : base(context)
        {
        }
    }
}
