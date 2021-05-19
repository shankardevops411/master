
using Scheduleservice.Services.EVVHandlers.EVVHandlersDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Services
{
    public interface IScheduleEVVService
    {
        //get EVV Enabled Payers
        Task<IEnumerable<ScheduleEVVInfoDto>> GetWronglyMarkedEVVVisits(int HHA, int userID, string StartDate, string EndDate);
        Task<bool> UpdateIsEVVScheduleFlag(int HHA, int UserID, IEnumerable<UpdateIsEVVScheduleFlagDto> ScheduleDetails);
      
        //Task<int> GetEVVEnabledPayers(int HHA);
        //Task<int> GetEVVEnabledServices(int HHA);
        //Task<int> GetEVVEnabledClincianDisc(int HHA);
    }
}
