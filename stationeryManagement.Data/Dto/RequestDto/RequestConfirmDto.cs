using stationeryManagement.Data.Enum;

namespace stationeryManagement.Data.Dto.RequestDto;

public class RequestConfirmDto
{
    public int RequestId { get; set; }
    public RequestStatus Status { get; set; }
}