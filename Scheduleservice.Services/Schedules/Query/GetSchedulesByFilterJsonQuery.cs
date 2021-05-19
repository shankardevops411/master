using System;
using System.Collections.Generic; 
using Scheduleservice.Services.Common.Request; 

namespace Scheduleservice.Services.Schedules.Query
{
    public class GetSchedulesByFilterJsonQuery:BaseRequest, IRequestWrapper<string>
    {        
        public string FilterJSON { get; set; }
    }
}
