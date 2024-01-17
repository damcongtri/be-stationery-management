namespace stationeryManagement.Service.Exceptions;

public class InternalServerErrorException   : Exception
{
    public InternalServerErrorException  (string message) : base(message)
    {
    }
}