using stationeryManagement.Data.Common.BaseUnitOfWork;
using stationeryManagement.Data.Common.DbContext;
using stationeryManagement.Data.Repository;

namespace stationeryManagement.Data;

public class UnitOfWork:UnitOfWorkBase,IUnitOfWork
{
    private ICategoryRepository _categoryRepository;
    public UnitOfWork(IDbContext context) : base(context)
    {
        
    }

    public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(DbContext);
}