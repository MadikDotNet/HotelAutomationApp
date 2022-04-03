namespace HotelAutomationApp.Application.Exceptions;

public class ApplicationLayerException : Exception
{
    public ApplicationLayerException()
    {
    }

    public ApplicationLayerException(string message)
        : base(message)
    {
    }

    public ApplicationLayerException(string message, Exception inner)
        : base(message, inner)
    {
    }
}