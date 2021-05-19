using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduleservice.Core.Entities
{
    public class CaregiverTaskChildSchedulesEntity
    {
        public int Child_Schedule_Id { get; set; }
        public int PARENT_CGTASK_ID { get; set; }
        public int SERVICECODE_ID { get; set; }

        public int PAYMENT_SOURCE { get; set; }
        public bool isEvvschedule { get; set; }
        public bool isEvvScheduleDirty { get; set; }

        public int EvvAggregatorVendorVersionMasterID { get; set; }
        public int HHA_BRANCH_ID { get; set; }
        public int CDSAuthServiceID { get; set; }

        public bool IsRealEVV { get; set; }

        public CaregiverTaskChildSchedulesEntity()
        {
            IsRealEVV = true;
        }

    }
}
