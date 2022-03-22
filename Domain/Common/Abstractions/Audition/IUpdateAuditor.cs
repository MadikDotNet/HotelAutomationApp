namespace HotelAutomationApp.Domain.Common.Abstractions.Audition;

public interface IUpdateAuditor
{
    /// <summary>
    /// Modifier identifier
    /// </summary>
    public string LastModifiedBy { get; set; }
        
    /// <summary>
    /// Date of last modify
    /// </summary>
    public DateTime LastModifiedDate { get; set; }
}