
using Scheduleservice.Services.EVVHandlers.EVVHandlersDto;
using Scheduleservice.Services.Infrastructure.RepositoryContracts;
using Scheduleservice.Services.Services.FacadeManager.Implementors.NonEVVSubsystems.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Services.Services.FacadeManager.Implementors.NonEVVSubsystems
{
    public class CaregiverDiscNonEVVValidator : ICaregiverDiscNonEVVValidator
    {
        private ICaregiversRepository _ICaregivers;

        public CaregiverDiscNonEVVValidator(ICaregiversRepository caregivers)
        {
            _ICaregivers = caregivers;
        }

        public async Task<List<ScheduleEVVInfoDto>> ValidateClincianDiscNonEVVFlag(int HHA, int UserID, List<ScheduleEVVInfoDto> HHASchedulesList)
        {
            List<ScheduleEVVInfoDto> OutHHASchedulesList = new List<ScheduleEVVInfoDto>();
            OutHHASchedulesList = HHASchedulesList;

            try
            {
                var ScheduleCaregivers = OutHHASchedulesList.Where(y => y.CAREGIVER > 0).Select(x => x.CAREGIVER).Distinct().ToList();

                if (ScheduleCaregivers.Count() > 0)
                {
                    //Get EVV Caregivers
                    var EVVCaregivers = await _ICaregivers.GetHHAEVVORNonEVVCaregivers(HHA, UserID, ScheduleCaregivers, false);

                    if (EVVCaregivers.Count() > 0)
                    {
                        OutHHASchedulesList.Where(y => y.CAREGIVER > 0).ToList().ForEach(x => x.IsRealNonEVV = false);

                        OutHHASchedulesList.Where(y => y.CAREGIVER > 0).Join(EVVCaregivers, (sch) => sch.CAREGIVER, (ser) => ser.CaregiverID,
                                     (sch, ser) =>
                                     {
                                         sch.IsRealNonEVV = true;
                                         return sch;
                                     }
                                     ).ToList();

                       // OutHHASchedulesList.Where(y => y.CAREGIVER > 0).ToList().RemoveAll(x => x.IsRealNonEVV == false);
                        OutHHASchedulesList.Where(y => y.CAREGIVER > 0).ToList().ForEach(y => y.IsClinicianDiscEVV = true);

                    }
                    else
                    {
                        OutHHASchedulesList.ForEach(y => y.IsClinicianDiscEVV = true);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return OutHHASchedulesList;
        }

    }
}
