namespace HotelAutomationApp.Application.Exceptions;

public class PermissionDeniedException : ApplicationLayerException
{
    public PermissionDeniedException() : base("Permission denied")
    {
        
    }
}