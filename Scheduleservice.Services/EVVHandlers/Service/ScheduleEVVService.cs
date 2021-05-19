using Scheduleservice.Services;
using Scheduleservice.Services.EVVHandlers.EVVHandlersDto;
using Scheduleservice.Services.Services.FacadeManager;
using Scheduleservice.Services.Services.FacadeManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Services.FacadeLayer
{
    public class ScheduleEVVService : IScheduleEVVService
    { 

        private IEVVManager _IEVVManager; 
       

        public ScheduleEVVService(IEVVManager evvManager)
        {
            _IEVVManager = evvManager;
        } 

        public async Task<IEnumerable<ScheduleEVVInfoDto>> GetWronglyMarkedEVVVisits(int HHA , int UserID, string StartDate, string EndDate)
        {
            IEnumerable<ScheduleEVVInfoDto> WrongRecords = null;
            try
            { 
                var WrongEVVRecords = await _IEVVManager.GetWronglyMarkedEVVVisits(HHA, UserID, StartDate, EndDate); 
                WrongRecords = WrongEVVRecords;

                var WrongNonEVVRecords = await _IEVVManager.GetWronglyMarkedNonEVVVisits(HHA, UserID, StartDate, EndDate);

                if (WrongNonEVVRecords != null)
                    WrongRecords = WrongRecords.Concat(WrongNonEVVRecords);

            }
            catch (Exception ex)
            { 
                throw;
            }

            return WrongRecords;

        }

        public async Task<bool> UpdateIsEVVScheduleFlag(int HHA, int UserID, IEnumerable<UpdateIsEVVScheduleFlagDto> ScheduleDetails)
        {
            var ret = false;
            try
            {
                var RealEVVSchedules = ScheduleDetails.Where(x => x.IsRealEVV == true).Select(y => y.CGTASK_ID);
                var NonEVVSchedules = ScheduleDetails.Where(x => x.IsRealEVV == false).Select(y => y.CGTASK_ID);

                if (RealEVVSchedules.Count() > 0)
                    ret = await _IEVVManager.UpdatescheduleasRealEVV(HHA, UserID, RealEVVSchedules);

                if (NonEVVSchedules.Count() > 0)
                    ret = await _IEVVManager.UpdatescheduleasNonEVV(HHA, UserID, NonEVVSchedules);
            }
            catch (Exception ex)
            {
                throw;
            }

            return ret;
        }

         
    }
}
