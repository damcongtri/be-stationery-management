using Microsoft.EntityFrameworkCore;
using stationeryManagement.Data;
using stationeryManagement.Data.Dto.RequestDto;
using stationeryManagement.Data.Enum;
using stationeryManagement.Data.Model;
using stationeryManagement.Service.Common;
using stationeryManagement.Service.Exceptions;
using stationeryManagement.Service.Interface;
using stationeryManagement.Service.SignalRService;


namespace stationeryManagement.Service;

public class RequestService : EntityService<Request>, IRequestService
{
    private readonly IEventHub _eventHub;
    private readonly IUnitOfWork _unitOfWork;
    public RequestService(IUnitOfWork unitOfWork, IEventHub eventHub) : base(unitOfWork, unitOfWork.RequestRepository)
    {
        _unitOfWork = unitOfWork;
        _eventHub = eventHub;
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
        return await _unitOfWork.RequestRepository.GetWithDetail(r => r.UserId == userId).ToListAsync();
    }


    // Create

    public async Task<Request?> CreateRequest(RequestCreateDto request, Guid userId)
    {
        var newRequest = new Request()
        {
            UserId = userId
        };
        var rq1 = await _unitOfWork.RequestRepository.AddAsync(newRequest);

        await _unitOfWork.CommitAsync();
        foreach (var rd in request.Stationeries.Select(item => new RequestDetail
        {
            RequestId = rq1.RequestId,
            StationeryId = item.StationeryId,
            Quantity = item.Quantity
        }))
        {
            var rrd = await _unitOfWork.RequestDetailRepository.AddAsync(rd);
            rq1.RequestDetails.Add(rrd);
        }

        if (await _unitOfWork.CommitAsync() > 0)
        {
            await _eventHub.SendMessageAsync(MessageFactory.RequestResult,MessageFactory.RequestResultEvent(userId, RequestStatus.Pending));
            // await 
            return rq1;
        }
        await _eventHub.SendMessageAsync(MessageFactory.ErrorMessage,MessageFactory.ErrorMessageEvent("Đã xảy ra lỗi, vui lòng thử lại hoặc liên hệ admin để được hỗ trợ"));
        return null;
    }
    //Update
    public async Task<bool> UpdateStatus(int requestId, RequestStatus status, Guid userId)
    {
        var findRequest = await _unitOfWork.RequestRepository.GetWithDetail(r => r.RequestId == requestId).FirstOrDefaultAsync();
        if (findRequest is null)
        {
            throw new NotFoundException("Không tìm thấy yêu cầu");
        }
        if (findRequest.ApprovalStatus != RequestStatus.Pending)
        {
            return false;
        }
        findRequest.ApprovalStatus = status;

        if (status == RequestStatus.Cancel)
        {
            if (findRequest.UserId != userId)
            {
                throw new Exception("Unauthorized");
            }
            findRequest.CancellationDate = DateTime.Now;
        }
        else
        {
            findRequest.ApprovedId = userId;
            findRequest.WithdrawalDate = DateTime.Now;
            if (status == RequestStatus.Approved)
            {
                foreach (var item in findRequest.RequestDetails)
                {
                    var findS = await _unitOfWork.StationeryRepository.GetByIdAsync(item.StationeryId);
                    if (findS == null) continue;
                    findS.Inventory -= item.Quantity;
                    _unitOfWork.StationeryRepository.UpdateAsync(findS);
                }
            }
        }
        await _eventHub
            .SendMessageAsync(MessageFactory.RequestResult
                ,MessageFactory.RequestResultEvent(userId, status),false);
        _unitOfWork.RequestRepository.UpdateAsync(findRequest);

        return await _unitOfWork.CommitAsync() > 0;
    }
}