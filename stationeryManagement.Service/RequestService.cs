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

    

    // Create
    
    public async Task<Request?> CreateRequest(RequestCreateDto request, Guid userId)
    {
        var newRequset =  new Request()
        {
           UserId = userId
        };
        foreach (var item in request.Stationeries)
        {
            var rd = new RequestDetail
            {
                StationeryId = item.StationeryId,
                Quantity = item.Quantity
            };
            newRequset.RequestDetails.Add(rd);
        }

        var r = await _unitOfWork.RequestRepository.AddAsync(newRequset);
        return await _unitOfWork.CommitAsync() >0 ?r:null;
    }
    //Update
    public async Task<bool> UpdateSatus(int requestId, RequestStatus status)
    {
        var findRequest = await _unitOfWork.RequestRepository.GetByIdAsync(requestId);
        findRequest.ApprovalStatus = status;
        if (status == RequestStatus.Cancel)
        {
            findRequest.CancellationDate = DateTime.Now;
        }
        if (status == RequestStatus.Approved)
        {
            findRequest.WithdrawalDate = DateTime.Now;
        }
        return await _unitOfWork.CommitAsync() >0;
    }
}