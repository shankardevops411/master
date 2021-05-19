using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduleservice.Core.Entities
{
   public class CaregiverTaskEvvSchedulesEntity
    {
        public int cgtaskid { get; set; }
        public bool IsEvvSchedule { get; set; }
        public string batchid { get; set; }
        public int SplitScheduleID { get; set; }

        
    }
}
