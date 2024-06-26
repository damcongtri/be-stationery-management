﻿using stationeryManagement.Data.Common.BaseRepository;
using stationeryManagement.Data.Common.DbContext;
using stationeryManagement.Data.Model;
using stationeryManagement.Data.Repository.Interface;

namespace stationeryManagement.Data.Repository;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(IDbContext context) : base(context)
    {
    }
}