using System;
using System.Collections.Generic; 
using Scheduleservice.Services.Common.Request;
using Scheduleservice.Services.EVVHandlers.EVVHandlersDto;

namespace Scheduleservice.Services.EVVHandlers
{
    public class ScheduleEVVQuery: BaseRequest, IRequestWrapper<IEnumerable<ScheduleEVVInfoDto>>
    {        
        public string StartDate { get; set; }        
        public string EndDate { get; set; }
    }
}
