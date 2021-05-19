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
    public class CdsAuthServiceEVVValidator: ICdsAuthServiceEVVValidator
    {
        private ICDSPlanYearServicesRepository _ICDSPlanYearServices;

        public CdsAuthServiceEVVValidator(ICDSPlanYearServicesRepository cdsPlanYearServices)
        {  
            _ICDSPlanYearServices = cdsPlanYearServices; 
        }
        public async Task<List<ScheduleEVVInfoDto>> ValidateCDSClientAuthServiceEVVFlag(int HHA, int UserID, List<ScheduleEVVInfoDto> HHASchedulesList)
        {
            List<ScheduleEVVInfoDto> OutHHASchedulesList = new List<ScheduleEVVInfoDto>();
            OutHHASchedulesList = HHASchedulesList;

            try
            {
                var CDSPlanYearServices = OutHHASchedulesList.Select(x => x.CdsPlanYearService).Distinct();
                //Get EVV services
                var EVVCDSPlanYearServices = await _ICDSPlanYearServices.GetEVVORNonEVVCDSPlanYearServices(HHA, UserID, CDSPlanYearServices, true);

                if (EVVCDSPlanYearServices.Count() > 0)
                {
                    OutHHASchedulesList.ForEach(x => x.IsRealEVV = false);

                    OutHHASchedulesList.Join(EVVCDSPlanYearServices, (sch) => sch.CdsPlanYearService, (ser) => ser.CDSPlanYearServiceID,
                                 (sch, ser) =>
                                 {
                                     sch.IsRealEVV = true;
                                     return sch;
                                 }
                                 ).ToList();

                    OutHHASchedulesList.RemoveAll(x => x.IsRealEVV == false);
                }
                else
                {
                    OutHHASchedulesList.Clear();
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
