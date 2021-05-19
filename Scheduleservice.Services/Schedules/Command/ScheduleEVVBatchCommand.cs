using Scheduleservice.Services.Common.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduleservice.Services.Schedules.Command
{
    public class ScheduleEVVBatchCommand : BaseRequest, IRequestWrapper<bool>
    {
        public string ScheduleEVVBatchID { get; set; }
    }
}
