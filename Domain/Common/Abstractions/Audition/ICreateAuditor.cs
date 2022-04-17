namespace HotelAutomationApp.Domain.Common.Abstractions.Audition;

public interface ICreateAuditor
{
    /// <summary>
    /// Creator identifier 
    /// </summary>
    public string? CreatedBy { get; set; }
        
    /// <summary>
    /// Date of creation
    /// </summary>
    public DateTime CreationDate { get; set; }
}