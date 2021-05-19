using Scheduleservice.Services.Common.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduleservice.Services.Schedules.Models
{
    public class ScheduleFilters: BaseRequest
    {
        
        public string startDate { get; set; }
        public string EndDate { get; set; }
        public int? PayerId { get; set; }
        public int? ClientID { get; set; }
        public int[] ClientIDList { get; set; }
    }

    public class ScheduleUpdateFilters : BaseRequest
    {

        public int CGTASK_ID { get; set; }
        public bool? isEVVAggrigatorExported { get; set; }
    }
}
