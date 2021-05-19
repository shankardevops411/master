using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduleservice.Core.Entities
{
    public class PaymentSourcesAdditionalEntity
    {
        public int PayerSourceId { get; set; }

        public bool IsEnableEVV { get; set; }
        public int EvvAggregatorVendorVersionMasterID { get; set; }
    }
}
