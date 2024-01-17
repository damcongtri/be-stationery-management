namespace stationeryManagement.Service.Exceptions;

public class BadRequestException  : Exception
{
    public BadRequestException (string message) : base(message)
    {
    }
}