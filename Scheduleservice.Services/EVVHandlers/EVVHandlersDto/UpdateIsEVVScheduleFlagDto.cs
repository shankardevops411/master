using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduleservice.Services.EVVHandlers.EVVHandlersDto
{
    public class UpdateIsEVVScheduleFlagDto
    {
        public int CGTASK_ID { get; set; }
        public bool IsRealEVV { get; set; }
    }
}
