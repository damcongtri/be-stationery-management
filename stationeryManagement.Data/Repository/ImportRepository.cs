﻿using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using stationeryManagement.Data.Common.BaseRepository;
using stationeryManagement.Data.Common.DbContext;
using stationeryManagement.Data.Model;
using stationeryManagement.Data.Repository.Interface;

namespace stationeryManagement.Data.Repository
{
    public class ImportRepository : GenericRepository<Import>, IImportRepository
    {
        public ImportRepository(IDbContext context) : base(context)
        {
        }
        public IQueryable<Import> GetWithUser()
        {
            return this.DbSet.Include(x => x.UserCreate).AsQueryable();

        }

       
    }
}
