using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduleservice.Core.Entities
{
    public class _C_EVVConfigurationsEntity
    {
        public int EvvConfigurationID { get; set; }
        public int HHABranchID { get; set; }
        public int EvvVendorVersionMasterID { get; set;}
        public DateTime EffectiveDate { get; set; }

    }
}
