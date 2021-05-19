 
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Scheduleservice.Services.Services.FacadeManager.Interfaces;
using AutoMapper; 
using Scheduleservice.Services.Services.FacadeManager.Implementors.Interfaces;
using Scheduleservice.Services.EVVHandlers.EVVHandlersDto;
using Scheduleservice.Services.Infrastructure.RepositoryContracts;

namespace Scheduleservice.Services.Services.FacadeManager
{
    public class EVVManager : IEVVManager
    {
        private ICaregivertasksRepository _ICaregivertasks;
        private I_C_EVVConfigurationsRepository _I_C_EVVConfigurations;
        private readonly IMapper _mapper;
        private IGetWrongEVVschedulesImplementor _IGetWrongEVVschedulesImplementor;
        private IGetWrongNonEVVschedulesImplementor _IGetWrongNonEVVschedulesImplementor;


        public EVVManager(I_C_EVVConfigurationsRepository c_EVVConfigurations, ICaregivertasksRepository caregivertasks, IMapper mapper, 
                            IGetWrongEVVschedulesImplementor getWrongEVVschedulesImplementor, IGetWrongNonEVVschedulesImplementor getWrongNonEVVschedulesImplementor)
        {
            _ICaregivertasks = caregivertasks;
            _I_C_EVVConfigurations = c_EVVConfigurations;
            _mapper = mapper;
            _IGetWrongEVVschedulesImplementor = getWrongEVVschedulesImplementor;
            _IGetWrongNonEVVschedulesImplementor = getWrongNonEVVschedulesImplementor;
        }


        public async Task<IEnumerable<ScheduleEVVInfoDto>> GetWronglyMarkedEVVVisits(int HHA, int UserID, string StartDate, string EndDate)
        {
            IEnumerable<ScheduleEVVInfoDto> WrongSchedules = null;
            try
            {
                var EVVEnabledBranchDetails = await _I_C_EVVConfigurations.GetEVVBranchDetails(HHA, UserID);
                if (EVVEnabledBranchDetails.Count() > 0)
                {
                    //get HHA EVV schedules
                    var HHASchedules = await _ICaregivertasks.GetHHAEVVSchedules(HHA, UserID, StartDate, EndDate); 
                    var HHASchedulesList = _mapper.Map<IEnumerable<ScheduleEVVInfoDto>>(HHASchedules).ToList(); 

                    WrongSchedules = await _IGetWrongEVVschedulesImplementor.GetWrongMarkedEVVschedules(HHA, UserID, HHASchedulesList);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return WrongSchedules;
        }

        public async Task<IEnumerable<ScheduleEVVInfoDto>> GetWronglyMarkedNonEVVVisits(int HHA, int UserID, string StartDate, string EndDate)
        {
            IEnumerable<ScheduleEVVInfoDto> WrongNonEVVSchedules = null;
            try
            {
                var EVVEnabledBranchDetails = await _I_C_EVVConfigurations.GetEVVBranchDetails(HHA, UserID);
                if (EVVEnabledBranchDetails.Count() > 0)
                {
                    //get HHA Non EVV schedules
                    var HHASchedules = await _ICaregivertasks.GetHHANonEVVSchedules(HHA, UserID, StartDate, EndDate);
                    var HHASchedulesList = _mapper.Map<IEnumerable<ScheduleEVVInfoDto>>(HHASchedules).ToList();

                    WrongNonEVVSchedules = await _IGetWrongNonEVVschedulesImplementor.GetWronglyMarkedNonEVVschedules(HHA, UserID, HHASchedulesList);
                }
            } 
            catch (Exception ex)
            {
                throw;
            }

            return WrongNonEVVSchedules;
        }

        public async Task<bool> UpdatescheduleasRealEVV(int HHA, int UserID, IEnumerable<int> CgtaskIds)
        {
            var ret = false;
            try
            {
                 ret = await _ICaregivertasks.UpdatescheduleasRealEVV(HHA, UserID, CgtaskIds);
            }
            catch (Exception ex)
            {
                throw;
            }
            return ret;
        }

        public async Task<bool> UpdatescheduleasNonEVV(int HHA, int UserID, IEnumerable<int> CgtaskIds)
        {
            var ret = false;
            try
            {
                ret = await _ICaregivertasks.UpdatescheduleasNonEVV(HHA, UserID, CgtaskIds);
            }
            catch (Exception ex)
            {
                throw;
            }
            return ret;
        }

         
    }
}
