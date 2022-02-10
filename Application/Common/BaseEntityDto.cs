using Newtonsoft.Json;

namespace HotelAutomationApp.Application.Common;

public record BaseEntityDto
{
    public BaseEntityDto()
    {
    }

    public BaseEntityDto(string id) =>
        Id = id;

    public string? Id { get; set; }

    [JsonIgnore]
    public bool HasId => !string.IsNullOrEmpty(Id);
}