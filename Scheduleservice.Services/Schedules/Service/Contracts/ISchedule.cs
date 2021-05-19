using Scheduleservice.Services.Schedules.DTO;
using Scheduleservice.Services.Schedules.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Services.Schedules.Service.Contracts
{
   public interface ISchedule
    {
        Task<IEnumerable<ScheduleListDto>> GetSchedulesList(ScheduleFilters scheduleFilters);
        Task<bool> UpdateScheduleProperties(ScheduleUpdateFilters  scheduleUpdateFilters);
    }
}
