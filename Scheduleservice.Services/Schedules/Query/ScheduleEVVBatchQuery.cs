using Scheduleservice.Services.Common.Request;
using Scheduleservice.Services.Schedules.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduleservice.Services.Schedules.Query
{
    public class ScheduleEVVBatchQuery : BaseRequest, IRequestWrapper<IEnumerable<ScheduleEVVBatchListDto>>
    {
        public int ClientID { get; set; }
        public int PayerID { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int HHABranchID { get; set; }
        public string Context { get; set; }
        public int[] ClientIDList { get; set; }
    }
}
