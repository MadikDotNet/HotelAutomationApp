using HotelAutomationApp.Application.ApplicationServices.Dictionary;
using HotelAutomationApp.Application.RoomGroups.Models;
using HotelAutomationApp.Domain.Models.RoomGroups;
using HotelAutomationApp.Domain.Models.Rooms;
using HotelAutomationApp.WebApi.Controllers.Common;

namespace HotelAutomationApp.WebApi.Controllers.RoomGroups
{
    public class RoomGroupController : DictionaryController
        <RoomGroup, RoomGroupDto, DictionaryCrudService<RoomGroup, RoomGroupDto>>
    {
        public RoomGroupController(DictionaryCrudService<RoomGroup, RoomGroupDto> dictionaryService) 
            : base(dictionaryService)
        {
        }
    }
}