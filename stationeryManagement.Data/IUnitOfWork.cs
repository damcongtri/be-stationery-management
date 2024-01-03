using stationeryManagement.Data.Common.BaseUnitOfWork;
using stationeryManagement.Data.Repository;

namespace stationeryManagement.Data;

public interface IUnitOfWork: IUnitOfWorkBase
{
    ICategoryRepository CategoryRepository { get; }
}