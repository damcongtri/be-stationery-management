using stationeryManagement.Data.Common.BaseUnitOfWork;
using stationeryManagement.Data.Common.DbContext;
using stationeryManagement.Data.Repository;
using stationeryManagement.Data.Repository.Interface;

namespace stationeryManagement.Data;

public class UnitOfWork:UnitOfWorkBase,IUnitOfWork
{
    private ICategoryRepository _categoryRepository;
    private ISupplierRepository _supplierRepository;
    private IRoleRepository _roleRepository;
    public UnitOfWork(IDbContext context) : base(context)
    {
        
    }

    public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(DbContext);
    public ISupplierRepository SupplierRepository => _supplierRepository ??= new SupplierRepository(DbContext);
    public IRoleRepository RoleRepository => _roleRepository ??= new RoleRepository(DbContext);
}