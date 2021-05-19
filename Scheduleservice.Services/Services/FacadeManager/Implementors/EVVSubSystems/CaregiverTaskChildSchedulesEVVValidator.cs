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
    public class CaregiverTaskChildSchedulesEVVValidator : ICaregiverTaskChildSchedulesEVVValidator
    {
        private ICaregiverTaskChildSchedulesRepository _ICaregiverTaskChildSchedules;
        private IPaymentsourcesAdditionalRepository _IPaymentsourcesAdditional;
        private IServiceCodesRepository _IServiceCodes;

        public CaregiverTaskChildSchedulesEVVValidator(ICaregiverTaskChildSchedulesRepository caregiverTaskChildSchedules, IPaymentsourcesAdditionalRepository payers,
                                                    IServiceCodesRepository serviceCodes)
        {
            _ICaregiverTaskChildSchedules = caregiverTaskChildSchedules;
            _IPaymentsourcesAdditional = payers;
            _IServiceCodes = serviceCodes;
        }

        public async Task<List<ScheduleEVVInfoDto>> ValidateSplitschedulesEVVFlag(int HHA, int UserID, List<ScheduleEVVInfoDto> HHASchedulesList)
        {
           // List<ScheduleEVVInfoDto> OutHHASchedulesList = new List<ScheduleEVVInfoDto>();

            try
            {
                var ParentSchedules = HHASchedulesList.Where(x => x.IsSplitForBilling == true).Select(x=>x.CGTASK_ID);

                //get SPlit schcedules 
                var Splitschedules = await _ICaregiverTaskChildSchedules.GetSplitscheduleDetails(HHA, UserID, ParentSchedules);

                var SchedulePayers = Splitschedules.Select(x => x.PAYMENT_SOURCE).Distinct();
              
                //Get EVV Payers
                var EVVPayers = await _IPaymentsourcesAdditional.GetHHAPayers(HHA, UserID, SchedulePayers);

                if (HHASchedulesList.Where(x => x.CdsPlanYearService > 0).Count() > 0)
                {
                    //Map the Parent CDS Auth service ID to child
                    Splitschedules.Join(HHASchedulesList, (sch) => sch.PARENT_CGTASK_ID, (cli) => cli.CGTASK_ID,
                                    (sch, cli) =>
                                    {
                                        sch.CDSAuthServiceID = cli.CdsPlanYearService;
                                        return sch;
                                    }
                                    ).ToList();
                }

                if (EVVPayers.Count() > 0)
                {
                    //check child Payer EVV Flag
                    Splitschedules.Join(EVVPayers, (sch) => sch.PAYMENT_SOURCE, (cli) => cli.PayerSourceId,
                                    (sch, cli) =>
                                    {
                                        sch.IsRealEVV = cli.IsEnableEVV;
                                        return sch;
                                    }
                                    ).ToList();
                } 

                Splitschedules = Splitschedules.Where(x => x.IsRealEVV == true).ToList();

                if (Splitschedules.Where(x => x.CDSAuthServiceID == 0).Count() > 0)
                {
                    var ScheduleServices = Splitschedules.Where(x => x.CDSAuthServiceID == 0).Select(x => x.SERVICECODE_ID).Distinct();
                    //Get EVV services
                    var EVVServices = await _IServiceCodes.GetEVVORNonEVVServices(HHA, UserID, ScheduleServices, true);

                    if (EVVServices.Count() > 0)
                    {
                        Splitschedules.Where(x => x.CDSAuthServiceID == 0).ToList().ForEach(x => x.IsRealEVV = false);

                        Splitschedules.Where(x => x.CDSAuthServiceID == 0).Join(EVVServices, (sch) => sch.SERVICECODE_ID, (cli) => cli.SERVICE_CODE_ID,
                                        (sch, cli) =>
                                        {
                                            sch.IsRealEVV = true;
                                            return sch;
                                        }
                                        ).ToList();
                    }
                }
                HHASchedulesList.Where(x => x.IsRealEVV = true).ToList().ForEach(y => y.IsRealEVV = false);

                if (Splitschedules.Count() > 0)
                {
                    HHASchedulesList.Join(Splitschedules, (sch) => sch.CGTASK_ID, (cli) => cli.PARENT_CGTASK_ID,
                                             (sch, cli) =>
                                             {
                                                 sch.IsRealEVV = true;
                                                 sch.IsRealNonEVV = false;
                                                 sch.IsSplitEVV = true;
                                                 sch.IsPayerEVV = true;
                                                 sch.IsServiceEVV = true; 
                                                 return sch;
                                             }
                                             ).ToList();
                }               

            }
            catch (Exception ex)
            {
                throw;
            }

            return HHASchedulesList;
        }
    }
}
