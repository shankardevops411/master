using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduleservice.Core.Entities
{
    public class HHADBDetailsEntity
    {
        public int HHA_ID { get; set; }
        public string DatabaseName { get; set; }
        public string Server { get; set; }
        public int DatabaseID { get; set; }

        public int UserID { get; set; }
    }
}
