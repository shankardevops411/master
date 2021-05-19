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
    public class ServiceNonEVVValidator : IServiceNonEVVValidator
    {
        private IServiceCodesRepository _IServiceCodes;

        public ServiceNonEVVValidator(IServiceCodesRepository serviceCodes)
        {
            _IServiceCodes = serviceCodes;
        }

        public async Task<List<ScheduleEVVInfoDto>> ValidateServiceNonEVVFlag(int HHA, int UserID, List<ScheduleEVVInfoDto> HHASchedulesList)
        {
            List<ScheduleEVVInfoDto> OutHHASchedulesList = new List<ScheduleEVVInfoDto>();
            OutHHASchedulesList = HHASchedulesList;

            try
            {
                var ScheduleServices = OutHHASchedulesList.Select(x => x.SERVICECODE_ID).Distinct();
                //Get EVV services
                var NonEVVServices = await _IServiceCodes.GetEVVORNonEVVServices(HHA, UserID, ScheduleServices, false);

                if (NonEVVServices.Count() > 0)
                {
                    OutHHASchedulesList.ForEach(x => x.IsRealNonEVV = false);

                    OutHHASchedulesList.Join(NonEVVServices, (sch) => sch.SERVICECODE_ID, (ser) => ser.SERVICE_CODE_ID,
                                 (sch, ser) =>
                                 {
                                     sch.IsRealNonEVV = true;
                                     return sch;
                                 }
                                 ).ToList();

                    OutHHASchedulesList.Where(x => x.IsRealNonEVV == false).ToList().ForEach(y => y.IsServiceEVV = true); 
                }
                else
                {
                    OutHHASchedulesList.ForEach(y => y.IsServiceEVV = true);
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
