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
    public class CDSAuthServiceNonEVVValidator : ICDSAuthServiceNonEVVValidator
    {
        private ICDSPlanYearServicesRepository _ICDSPlanYearServices;

        public CDSAuthServiceNonEVVValidator(ICDSPlanYearServicesRepository cdsPlanYearServices)
        {
            _ICDSPlanYearServices = cdsPlanYearServices;
        }

        public async Task<List<ScheduleEVVInfoDto>> ValidateCDSClientAuthServiceNonEVVFlag(int HHA, int UserID, List<ScheduleEVVInfoDto> HHASchedulesList)
        {
            List<ScheduleEVVInfoDto> OutHHASchedulesList = new List<ScheduleEVVInfoDto>();
            OutHHASchedulesList = HHASchedulesList;

            try
            {
                var CDSPlanYearServices = OutHHASchedulesList.Select(x => x.CdsPlanYearService).Distinct();
                //Get non EVV Auth services
                var NonEVVCDSPlanYearServices = await _ICDSPlanYearServices.GetEVVORNonEVVCDSPlanYearServices(HHA, UserID, CDSPlanYearServices, false);

                if (NonEVVCDSPlanYearServices.Count() > 0)
                {
                    OutHHASchedulesList.ForEach(x => x.IsRealNonEVV = false);

                    OutHHASchedulesList.Join(NonEVVCDSPlanYearServices, (sch) => sch.CdsPlanYearService, (ser) => ser.CDSPlanYearServiceID,
                                 (sch, ser) =>
                                 {
                                     sch.IsRealNonEVV = true;
                                     return sch;
                                 }
                                 ).ToList();

                    OutHHASchedulesList.Where(x => x.IsRealNonEVV == false).ToList().ForEach(y => y.IsCdsAuthServiceEVV = true);
                    //OutHHASchedulesList.RemoveAll(x => x.IsRealNonEVV == false);
                }
                else
                {
                    OutHHASchedulesList.ForEach(y => y.IsCdsAuthServiceEVV = true);
                   
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
