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
    public class ServiceEVVValidator : IServiceEVVValidator
    {
        private IServiceCodesRepository _IServiceCodes;

        public ServiceEVVValidator(IServiceCodesRepository serviceCodes)
        { 
            _IServiceCodes = serviceCodes; 
        }

        public async Task<List<ScheduleEVVInfoDto>> ValidateServiceEVVFlag(int HHA, int UserID, List<ScheduleEVVInfoDto> HHASchedulesList)
        {
            List<ScheduleEVVInfoDto> OutHHASchedulesList = new List<ScheduleEVVInfoDto>();
            OutHHASchedulesList = HHASchedulesList;

            try
            {
                var ScheduleServices = OutHHASchedulesList.Select(x => x.SERVICECODE_ID).Distinct();
                //Get EVV services
                var EVVServices = await _IServiceCodes.GetEVVORNonEVVServices(HHA, UserID, ScheduleServices, true);

                if (EVVServices.Count() > 0)
                {
                    OutHHASchedulesList.ForEach(x => x.IsRealEVV = false);

                    OutHHASchedulesList.Join(EVVServices, (sch) => sch.SERVICECODE_ID, (ser) => ser.SERVICE_CODE_ID,
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
