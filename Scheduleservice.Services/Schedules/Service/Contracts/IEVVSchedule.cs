using Scheduleservice.Services.Schedules.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Services.Schedules.Service.Contracts
{
    public interface IEVVSchedule
    { 
        Task<IEnumerable<ScheduleEVVBatchListDto>> InsertCaregiverTaskEvvSchedules(int HHA, int UserID, IEnumerable<ScheduleCGTaskIDDto> CGTaskIDs );
        Task<bool>RecalculateCaregiverTaskEvvSchedules(int HHA, int UserID, String ScheduleEVVBatchID);
    }
}
