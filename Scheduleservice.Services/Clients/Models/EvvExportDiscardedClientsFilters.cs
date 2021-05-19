using Scheduleservice.Services.Common.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduleservice.Services.Clients.Models
{
    public class EvvExportDiscardedClientsFilters : BaseRequest
    {
        public int PAYMENT_SOURCE { get; set; }
        public int CLIENT_ID { get; set; }
       
    }
}
