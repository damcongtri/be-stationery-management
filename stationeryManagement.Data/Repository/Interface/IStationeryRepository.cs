﻿using stationeryManagement.Data.Common.BaseRepository;
using stationeryManagement.Data.Model;

namespace stationeryManagement.Data.Repository.Interface
{
    public interface IStationeryRepository : IGenericRepository<Stationery>
    {
        IQueryable<Stationery> GetWithCategoryAndSupplier();
    }
}
