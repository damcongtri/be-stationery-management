using stationeryManagement.Data.Common.BaseUnitOfWork;
using stationeryManagement.Data.Common.DbContext;
using stationeryManagement.Data.Repository;
using stationeryManagement.Data.Repository.Interface;
using System.Security.AccessControl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace stationeryManagement.Data;

public class UnitOfWork:UnitOfWorkBase,IUnitOfWork
{
    private DbContext _dbContext;
    private ICategoryRepository _categoryRepository;
    private ISupplierRepository _supplierRepository;
    private IUserRepository _userRepository;
    private IRoleRepository _roleRepository;
    private IStationeryRepository _stationeryRepository;
    private IRequestRepository _requestRepository;
    private IImportRepository _importRepository;
    private IRequestDetailRepository _requestDetailRepository;
    private IImportDetailRepository _importDetailRepository;
    
    public UnitOfWork(IDbContext context) : base(context)
    {
        
    }
    

    public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(DbContext);
    public ISupplierRepository SupplierRepository => _supplierRepository ??= new SupplierRepository(DbContext);
    public IUserRepository UserRepository => _userRepository ??= new UserRepository(DbContext);
    public IRoleRepository RoleRepository => _roleRepository ??= new RoleRepository(DbContext);
    public IRequestRepository RequestRepository=> _requestRepository ??= new RequestRepository(DbContext);
    public IImportRepository ImportRepository => _importRepository ??= new ImportRepository(DbContext);
    public IStationeryRepository StationeryRepository=> _stationeryRepository ??= new StationeryRepository(DbContext);
    public IRequestDetailRepository RequestDetailRepository => _requestDetailRepository ??= new RequestDetailRepository(DbContext);

    public IImportDetailRepository ImportDetailRepository =>
        _importDetailRepository ??= new ImportDetailRepository(DbContext);
    
    
    
    public async Task BeginTransactionAsync()
    {
        await _dbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await _dbContext.Database.CommitTransactionAsync();
    }

    /// <summary>
    /// Rollback transaction
    /// </summary>
    /// <returns></returns>
    public async Task<bool> RollbackAsync()
    {
        try
        {
            await _dbContext.Database.RollbackTransactionAsync();
        }
        catch (Exception)
        {
            // Swallow exception (this might be used in places where a transaction isn't setup)
        }

        return true;
    }
}