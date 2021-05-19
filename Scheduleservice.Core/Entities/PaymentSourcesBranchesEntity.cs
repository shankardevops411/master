using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduleservice.Core.Entities
{
    public class PaymentSourcesBranchesEntity
    {
        public int PaymentSource_ID { get; set; }
        public int HHA { get; set; }
        public int Branch_ID { get; set; }
        public int EvvAggregatorVendorVersionMasterID { get; set; }
        public bool EnableEvv { get; set; }
    }
}
