using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduleservice.Core.Entities
{
    public class EvvExportDiscardedClientsEntity
    {
        public int EvvExportDiscardedClientID { get; set; }
        public int HHA { get; set; }
        public int PAYMENT_SOURCE { get; set; }
        public int CLIENT_ID { get; set; }
        public int EvvVendorVersionMasterID { get; set; }
        
    }
}
