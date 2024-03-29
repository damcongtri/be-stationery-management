using stationeryManagement.Data.Enum;

namespace stationeryManagement.Service.SignalRService;

public static class MessageFactory
{
    public const string RequestResult = "RequestResult";
    public const string ErrorMessage = "ErrorMessage";
    
    public static SignalRMessage RequestResultEvent(Guid userId, RequestStatus status)
    {
        return new SignalRMessage
        {
            Name = RequestResult,
            Title = "Request Result",
            Body = new
            {
                UserId = userId,
                Result = status
            }
        };
    }
    public static SignalRMessage ErrorMessageEvent(string error)
    {
        return new SignalRMessage
        {
            Name = ErrorMessage,
            Title = "Error Message",
            Body = new
            {
                ErrorMessage= error,
            }
        };
    }
}