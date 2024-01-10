using stationeryManagement.Data.Common.BaseUnitOfWork;
using stationeryManagement.Data.Repository.Interface;

namespace stationeryManagement.Data;
public interface IUnitOfWork: IUnitOfWorkBase
{
    ICategoryRepository CategoryRepository { get; }
    ISupplierRepository SupplierRepository { get; }
}