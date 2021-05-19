
using Scheduleservice.Services.EVVHandlers.EVVHandlersDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Services.Services.FacadeManager.Interfaces
{
    public interface IEVVManager
    {
        Task<IEnumerable<ScheduleEVVInfoDto>> GetWronglyMarkedEVVVisits(int HHA, int UserID, string StartDate, string EndDate); 
        Task<IEnumerable<ScheduleEVVInfoDto>> GetWronglyMarkedNonEVVVisits(int HHA, int UserID, string StartDate, string EndDate);

        Task<bool> UpdatescheduleasRealEVV(int HHA, int UserID, IEnumerable<int> CgtaskIds);

        Task<bool> UpdatescheduleasNonEVV(int HHA, int UserID, IEnumerable<int> CgtaskIds);
         
    }
}
