using Scheduleservice.Services.EVVHandlers.EVVHandlersDto;
using Scheduleservice.Services.Infrastructure.RepositoryContracts;
using Scheduleservice.Services.Services.FacadeManager.Implementors.EVVSubSystems.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Services.Services.FacadeManager.Implementors.EVVSubSystems
{
    public class CaregiverDiscEVVValidator: ICaregiverDiscEVVValidator
    {
        private ICaregiversRepository _ICaregivers;

        public CaregiverDiscEVVValidator(ICaregiversRepository caregivers)
        {
            _ICaregivers = caregivers;
        }

        public async Task<List<ScheduleEVVInfoDto>> ValidateClincianDiscEVVFlag(int HHA, int UserID, List<ScheduleEVVInfoDto> HHASchedulesList)
        {
            List<ScheduleEVVInfoDto> OutHHASchedulesList = new List<ScheduleEVVInfoDto>();
            OutHHASchedulesList = HHASchedulesList;

            try
            {
                var ScheduleCaregivers = OutHHASchedulesList.Where(y => y.CAREGIVER > 0).Select(x => x.CAREGIVER).Distinct().ToList();

                if (ScheduleCaregivers.Count() > 0)
                {
                    //Get EVV Caregivers
                    var EVVCaregivers = await _ICaregivers.GetHHAEVVORNonEVVCaregivers(HHA, UserID, ScheduleCaregivers, true);

                    if (EVVCaregivers.Count() > 0)
                    {
                        OutHHASchedulesList.Where(y => y.CAREGIVER > 0).ToList().ForEach(x => x.IsRealEVV = false);

                        OutHHASchedulesList.Where(y => y.CAREGIVER > 0).Join(EVVCaregivers, (sch) => sch.CAREGIVER, (car) => car.CaregiverID,
                                     (sch, car) =>
                                     {
                                         sch.IsRealEVV = true;
                                         return sch;
                                     }
                                     ).ToList();

                        OutHHASchedulesList.Where(y => y.CAREGIVER > 0).ToList().RemoveAll(x => x.IsRealEVV == false);
                    }
                    else
                    {
                        OutHHASchedulesList.Clear();
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
