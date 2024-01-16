using stationeryManagement.Data.Dto.RequestDto;
using stationeryManagement.Data.Enum;
using stationeryManagement.Data.Model;

namespace stationeryManagement.Service.Interface;

public interface IRequestService
{
    // Create
    Task<Request> CreateRequest(RequestCreateDto request);

    // Read
    Task<Request?> GetRequestById(int requestId);
    Task<IEnumerable<Request>> GetAllRequest();
    Task<IEnumerable<Request>> GetRequestForUser(Guid userId);

    // Update
    Task<bool> UpdateSatus(int RequestId, RequestStatus status);
}