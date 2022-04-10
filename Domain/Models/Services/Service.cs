using System.ComponentModel.DataAnnotations.Schema;
using HotelAutomationApp.Domain.Common;
using HotelAutomationApp.Domain.Models.ServiceGroups;

namespace HotelAutomationApp.Domain.Models.Services;

public class Service : BaseDictionary
{
    public Service(string name, string code, string description, decimal pricePerHour) : base(name, code, description)
    {
        PricePerHour = pricePerHour;
    }

    public Service(decimal pricePerHour)
    {
        PricePerHour = pricePerHour;
    }

    public Service()
    {
    }

    public decimal PricePerHour { get; set; }
    [ForeignKey(nameof(ServiceGroup))]
    public string ServiceGroupId { get; set; }
    public ServiceGroup ServiceGroup { get; set; }
}