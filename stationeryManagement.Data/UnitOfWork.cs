﻿using stationeryManagement.Data.Common.BaseUnitOfWork;
using stationeryManagement.Data.Common.DbContext;
using stationeryManagement.Data.Repository;
using stationeryManagement.Data.Repository.Interface;

namespace stationeryManagement.Data;

public class UnitOfWork:UnitOfWorkBase,IUnitOfWork
{
    private ICategoryRepository _categoryRepository;
    private ISupplierRepository _supplierRepository;
    private IUserRepository _userRepository;
    private IRoleRepository _roleRepository;
    private IStationeryRepository _stationeryRepository;
    private IRequestRepository _requestRepository;
    public UnitOfWork(IDbContext context) : base(context)
    {
        
    }

    public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(DbContext);
    public ISupplierRepository SupplierRepository => _supplierRepository ??= new SupplierRepository(DbContext);
    public IUserRepository UserRepository => _userRepository ??= new UserRepository(DbContext);
    public IRoleRepository RoleRepository => _roleRepository ??= new RoleRepository(DbContext);
    public IStationeryRepository StationeryRepository => _stationeryRepository ??= new StationeryRepository(DbContext);
    public IRequestRepository RequestRepository=> _requestRepository ??= new RequestRepository(DbContext);
}