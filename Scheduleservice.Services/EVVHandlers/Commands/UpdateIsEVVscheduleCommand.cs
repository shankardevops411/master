using Scheduleservice.Services.Common.Request;
namespace Scheduleservice.Services.EVVHandlers.Commands
{
    public class UpdateIsEVVscheduleCommand : BaseRequest, IRequestWrapper<bool>
    {      
        public string ScheduleDetails_JSON { get; set; } 
    }
}
