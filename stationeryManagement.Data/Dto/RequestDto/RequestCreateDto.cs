using stationeryManagement.Data.Enum;

namespace stationeryManagement.Data.Dto.RequestDto;

public class RequestCreateDto
{
    public DateTime RequestDate { get; set; } = DateTime.Now;
    public RequestStatus ApprovalStatus { get; set; } = RequestStatus.Pending;
    public DateTime? CancellationDate { get; set; }
    public DateTime? WithdrawalDate { get; set; }
    public List<RequestDetailCreateDto> Stationeries { get; set; }
}