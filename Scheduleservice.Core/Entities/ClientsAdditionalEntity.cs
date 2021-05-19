using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduleservice.Core.Entities
{
    public class ClientsAdditionalEntity
    {
        public int CLIENT_ID { get; set; }
        public int HHA_BRANCH_ID { get; set; } 
        public int? LOB_ID { get; set; }

        public bool? Enable_EVV { get; set; }
    }
}
