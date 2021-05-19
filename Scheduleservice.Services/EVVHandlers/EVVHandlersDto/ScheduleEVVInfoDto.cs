using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduleservice.Services.EVVHandlers.EVVHandlersDto
{
    public class ScheduleEVVInfoDto
    {
        public int CGTASK_ID { get; set; }
        public int PAYMENT_SOURCE { get; set; }
        public int SERVICECODE_ID { get; set; }
        public int CLIENT_ID { get; set; }
        public int CAREGIVER { get; set; }
        public DateTime PLANNED_DATE { get; set; }
        public int CdsPlanYearService { get; set; }
        public bool isEvvschedule { get; set; } 
        public bool isEvvScheduleDirty { get; set; }
        public int HHA_BRANCH_ID { get; set; }
        public int EvvAggregatorVendorVersionMasterID { get; set; }
        public DateTime EVVBranchVendorEffectiveDate { get; set; }

        public bool IsSplitForBilling { get; set; }
        public bool IsSplitEVV { get; set; }

        public bool IsRealEVV { get; set; }

        public bool IsRealNonEVV { get; set; }

        public bool IsClientORLOBEVV { get; set; }
        public bool IsPayerEVV { get; set; }
        public bool IsHHABranchEVV { get; set; }
        public bool IsServiceEVV { get; set; }
        public bool IsCdsAuthServiceEVV { get; set; }
        public bool IsClinicianDiscEVV { get; set; }


        public ScheduleEVVInfoDto()
        {
            IsRealEVV = false;
            IsRealNonEVV = true;
            IsHHABranchEVV = false;
            IsSplitEVV = false; 
        }
    }
}
