using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using stationeryManagement.Data.Common.BaseRepository;
using stationeryManagement.Data.Common.DbContext;
using stationeryManagement.Data.Model;
using stationeryManagement.Data.Repository.Interface;

namespace stationeryManagement.Data.Repository;

public class RequestRepository: GenericRepository<Request>,IRequestRepository
{
    public RequestRepository(IDbContext context) : base(context)
    {
    }

    // public async Task<Request> AddWithDetailAsync(Request request)
    // {
    //     var ent = (await DbSet.Include(r=>r.RequestDetails).AddAsync(request)).Entity;
    //
    //     // Entities.SaveChanges(); DO NOT SAVE CHANGE HERE
    //     // need to call unitOfWork.CommitAsync() to save the changes
    //     return ent;
    // }

    public IQueryable<Request> GetWithDetail(Expression<Func<Request, bool>> predicate, bool tracking = false)
    {
        return tracking ? this.DbSet.Include(r=>r.RequestDetails).AsTracking().Where(predicate) : this.DbSet.Include(r=>r.RequestDetails).AsNoTracking().Where(predicate);
    }
}