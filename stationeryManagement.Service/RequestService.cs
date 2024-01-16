using Microsoft.EntityFrameworkCore;
using stationeryManagement.Data;
using stationeryManagement.Data.Common.BaseRepository;
using stationeryManagement.Data.Dto.RequestDto;
using stationeryManagement.Data.Enum;
using stationeryManagement.Data.Model;
using stationeryManagement.Service.Common;
using stationeryManagement.Service.Interface;

namespace stationeryManagement.Service;

public class RequestService:EntityService<Request>,IRequestService
{
    private readonly IUnitOfWork _unitOfWork;
    public RequestService(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.RequestRepository)
    {
        _unitOfWork = unitOfWork;
    }
    //get 
    public async Task<Request?> GetRequestById(int requestId)
    {
        return await _unitOfWork.RequestRepository.GetByIdAsync(requestId);
    }

    public async Task<IEnumerable<Request>> GetAllRequest()
    {
        return await _unitOfWork.RequestRepository.GetAll().ToListAsync();
    }

    public async Task<IEnumerable<Request>> GetRequestForUser(Guid userId)
    {
        return await _unitOfWork.RequestRepository.FindBy(r => r.UserId == userId).ToListAsync();
    }

    

    //
    
    public async Task<Request> CreateRequest(RequestCreateDto request)
    {
        var newRequset =  new Request() { };
        return await _unitOfWork.RequestRepository.AddAsync(newRequset);
    }
    //Update
    public async Task<bool> UpdateSatus(int requestId, RequestStatus status)
    {
        var findRequest = await _unitOfWork.RequestRepository.GetByIdAsync(requestId);
        findRequest.ApprovalStatus = status;
        return await _unitOfWork.CommitAsync() >0;
    }
}