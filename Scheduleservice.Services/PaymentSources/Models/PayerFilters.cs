using Scheduleservice.Services.Common.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduleservice.Services.PaymentSources.Models
{
    public class PayerFilters : BaseRequest
    {
        public int PAYMENT_SOURCE_ID { get; set; }
        public bool IsEnableEVV { get; set; }
        public int[] PAYMENT_SOURCE_IDList { get; set; }
    }
}
