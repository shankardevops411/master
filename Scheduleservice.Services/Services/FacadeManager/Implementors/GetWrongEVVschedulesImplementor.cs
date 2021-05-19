using Scheduleservice.Services.EVVHandlers.EVVHandlersDto;
using Scheduleservice.Services.Services.FacadeManager.Implementors.EVVSubSystems.Interfaces;
using Scheduleservice.Services.Services.FacadeManager.Implementors.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Services.Services.FacadeManager.Implementors
{
    public class GetWrongEVVschedulesImplementor : IGetWrongEVVschedulesImplementor 
    { 
        private IServiceEVVValidator _IServiceEVVValidator;
        private IClientORLOBEVVValidator _IClientORLOBEVVValidator;
        private ICaregiverDiscEVVValidator _ICaregiverDiscEVVValidator;
        private ICdsAuthServiceEVVValidator _ICdsAuthServiceEVVValidator;
        private IHHABrancheffectivePeriodEVVValidator _IHHABrancheffectivePeriodEVVValidator;
        private IPayerEVVValidator _IPayerEVVValidator;
        private ICaregiverTaskChildSchedulesEVVValidator _ICaregiverTaskChildSchedulesEVVValidator;

        public GetWrongEVVschedulesImplementor( IClientORLOBEVVValidator clientORLOBEVVValidator, ICaregiverDiscEVVValidator caregiverDiscEVVValidator,
                                ICdsAuthServiceEVVValidator cdsAuthServiceEVVValidator, IHHABrancheffectivePeriodEVVValidator hhaBrancheffectivePeriodEVVValidator,
                                IPayerEVVValidator payerEVVValidator, IServiceEVVValidator serviceEVVValidator,
                                ICaregiverTaskChildSchedulesEVVValidator caregiverTaskChildSchedulesEVVValidator
                                )
        {
              
            _IClientORLOBEVVValidator = clientORLOBEVVValidator;
            _ICaregiverDiscEVVValidator = caregiverDiscEVVValidator;
            _ICdsAuthServiceEVVValidator = cdsAuthServiceEVVValidator;
            _IHHABrancheffectivePeriodEVVValidator = hhaBrancheffectivePeriodEVVValidator;
            _IPayerEVVValidator = payerEVVValidator;
            _IServiceEVVValidator = serviceEVVValidator;
            _ICaregiverTaskChildSchedulesEVVValidator = caregiverTaskChildSchedulesEVVValidator;
        } 

        public async Task<List<ScheduleEVVInfoDto>> GetWrongMarkedEVVschedules(int HHA, int UserID, List<ScheduleEVVInfoDto> HHASchedulesList)
        {
            List<ScheduleEVVInfoDto> NonEVVschedules = null;
            List<ScheduleEVVInfoDto> CDSEVVschedules = new List<ScheduleEVVInfoDto>();
            List<ScheduleEVVInfoDto> NonCDSEVVschedules = new List<ScheduleEVVInfoDto>();

            var OriginalHHAEVVSchedulesList = HHASchedulesList.ToList() ;
            try
            {

               // HHASchedulesList.ForEach(x => x.IsRealEVV = true); 

                //Validate Client/LOB EVV FLag
                HHASchedulesList = await _IClientORLOBEVVValidator.ValidateClientORLobEVVFlag(HHA, UserID, HHASchedulesList); 

                if (HHASchedulesList.Count() > 0)
                {
                    //Validate HHA Branch Vendor Effective Date
                    HHASchedulesList = await _IHHABrancheffectivePeriodEVVValidator.ValidateHHABranchEffectiveEVVFlag(HHA, UserID, HHASchedulesList);
                }

                if (HHASchedulesList.Count() > 0)
                {

                    if (HHASchedulesList.Where(y => y.CAREGIVER > 0).Count() > 0)
                    {
                        //Validate Caregiver Disc EVV flag
                        var NoCaregiverSchedules = HHASchedulesList.Where(y => y.CAREGIVER == 0).ToList();
                        HHASchedulesList = await _ICaregiverDiscEVVValidator.ValidateClincianDiscEVVFlag(HHA, UserID, HHASchedulesList.Where(y => y.CAREGIVER > 0).ToList());
                       
                        HHASchedulesList.AddRange(NoCaregiverSchedules); 
                    }

                    //HHASchedulesList.ForEach(x => x.IsRealEVV = false);

                    if (HHASchedulesList.Where(x => x.CdsPlanYearService > 0).Count() > 0)
                    {
                        //Validate for Client CDS Auth service 
                        CDSEVVschedules = await _ICdsAuthServiceEVVValidator.ValidateCDSClientAuthServiceEVVFlag(HHA, UserID, HHASchedulesList.Where(x => x.CdsPlanYearService > 0).ToList());
                    }

                    if (HHASchedulesList.Where(x => x.CdsPlanYearService == 0).Count() > 0)
                    {
                        //Validate Payer EVV Flag
                        NonCDSEVVschedules = await _IPayerEVVValidator.ValidatePayerEVVFlag(HHA, UserID, HHASchedulesList.Where(x => x.CdsPlanYearService == 0).ToList());
                       
                    }  

                    if (HHASchedulesList.Where(x => x.CdsPlanYearService == 0).Count() > 0)
                    {
                        //ValidateServiceEVV Flag 
                        NonCDSEVVschedules = await _IServiceEVVValidator.ValidateServiceEVVFlag(HHA, UserID, HHASchedulesList.Where(x => x.CdsPlanYearService == 0).ToList());
                       
                    }

                    //  HHASchedulesList.Clear();

                    if (CDSEVVschedules.Count() > 0)
                    {
                        HHASchedulesList.Join(CDSEVVschedules, (sch) => sch.CGTASK_ID, (cli) => cli.CGTASK_ID,
                                            (sch, cli) =>
                                            {
                                                sch.IsRealEVV = true;
                                                sch.IsRealNonEVV = false; 
                                                return sch;
                                            }
                                            ).ToList();

                       // HHASchedulesList = CDSEVVschedules;
                    }

                    if (NonCDSEVVschedules.Count() > 0)
                    {
                        HHASchedulesList.Join(NonCDSEVVschedules, (sch) => sch.CGTASK_ID, (cli) => cli.CGTASK_ID,
                                            (sch, cli) =>
                                            {
                                                sch.IsRealEVV = true;
                                                sch.IsRealNonEVV = false;
                                                return sch;
                                            }
                                            ).ToList();
                         

                    }

                    //if (HHASchedulesList.Where(x => x.IsRealEVV == false).Count() > 0)
                    //    HHASchedulesList.Where(x => x.IsRealEVV == false).ToList().ForEach(x => x.IsRealEVV = true);

                    if (HHASchedulesList.Where(x => x.IsSplitForBilling == true && x.IsRealEVV == false).Count() > 0)
                    {
                        //Validate split schedule EVV Flag 
                        var SplitEVVSChedules = await _ICaregiverTaskChildSchedulesEVVValidator.ValidateSplitschedulesEVVFlag(HHA, UserID, HHASchedulesList.Where(y => y.IsSplitForBilling == true && y.IsRealEVV == false).ToList());
                        SplitEVVSChedules = SplitEVVSChedules.Where(x => x.IsSplitEVV == true).ToList();
                        HHASchedulesList.Join(SplitEVVSChedules, (sch) => sch.CGTASK_ID, (cli) => cli.CGTASK_ID,
                                           (sch, cli) =>
                                           {
                                               sch.IsRealEVV = true;
                                               sch.IsRealNonEVV = false;
                                               return sch;
                                           }
                                           ).ToList(); 
                    }

                    HHASchedulesList = HHASchedulesList.Where(x => x.IsRealEVV == true).ToList();

                }

                OriginalHHAEVVSchedulesList.ForEach(x => x.IsRealNonEVV = false);
                NonEVVschedules = FilterNonEVVscheduleList(OriginalHHAEVVSchedulesList, HHASchedulesList);
                NonEVVschedules.ForEach(x => x.IsRealNonEVV = true);
            }
            catch (Exception ex)
            {
                throw;
            }

            return NonEVVschedules;
        }

        public List<ScheduleEVVInfoDto> FilterNonEVVscheduleList(List<ScheduleEVVInfoDto> OriginalHHASchedulesList, List<ScheduleEVVInfoDto> HHASchedulesList)
        {
            List<ScheduleEVVInfoDto> NonEVVschedules = new List<ScheduleEVVInfoDto>();
            NonEVVschedules = OriginalHHASchedulesList.Except(HHASchedulesList).ToList();
            NonEVVschedules.ForEach(x => x.IsRealEVV = false);
            return NonEVVschedules;
        } 
    }
}
 
