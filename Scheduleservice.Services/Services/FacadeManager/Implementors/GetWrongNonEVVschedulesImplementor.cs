using Scheduleservice.Services.EVVHandlers.EVVHandlersDto;
using Scheduleservice.Services.Services.FacadeManager.Implementors.EVVSubSystems.Interfaces;
using Scheduleservice.Services.Services.FacadeManager.Implementors.Interfaces;
using Scheduleservice.Services.Services.FacadeManager.Implementors.NonEVVSubsystems.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Services.Services.FacadeManager.Implementors
{
    public class GetWrongNonEVVschedulesImplementor: IGetWrongNonEVVschedulesImplementor
    { 
       
        private IClientORLOBNonEVVValidator _IClientORLOBNonEVVValidator;
        private ICaregiverDiscNonEVVValidator _ICaregiverDiscNonEVVValidator;
        private ICDSAuthServiceNonEVVValidator _ICDSAuthServiceNonEVVValidator;
        private IHHABrancheffectivePeriodEVVValidator _IHHABrancheffectivePeriodEVVValidator;
        private IPayerNonEVVValidator _IPayerNonEVVValidator;
        private IServiceNonEVVValidator _IServiceNonEVVValidator;
        private ICaregiverTaskChildSchedulesEVVValidator _ICaregiverTaskChildSchedulesEVVValidator;

        public GetWrongNonEVVschedulesImplementor(IClientORLOBNonEVVValidator clientORLOBEVVValidator, ICaregiverDiscNonEVVValidator caregiverDiscEVVValidator,
                                ICDSAuthServiceNonEVVValidator cdsAuthServiceEVVValidator, IHHABrancheffectivePeriodEVVValidator hhaBrancheffectivePeriodEVVValidator,
                                IPayerNonEVVValidator payerEVVValidator, IServiceNonEVVValidator serviceEVVValidator,
                                ICaregiverTaskChildSchedulesEVVValidator caregiverTaskChildSchedulesEVVValidator
                                )
        {

            _IClientORLOBNonEVVValidator = clientORLOBEVVValidator;
            _ICaregiverDiscNonEVVValidator = caregiverDiscEVVValidator;
            _ICDSAuthServiceNonEVVValidator = cdsAuthServiceEVVValidator;
            _IHHABrancheffectivePeriodEVVValidator = hhaBrancheffectivePeriodEVVValidator;
            _IPayerNonEVVValidator = payerEVVValidator;
            _IServiceNonEVVValidator = serviceEVVValidator;
            _ICaregiverTaskChildSchedulesEVVValidator = caregiverTaskChildSchedulesEVVValidator;
        }


        public async Task<List<ScheduleEVVInfoDto>> GetWronglyMarkedNonEVVschedules(int HHA, int UserID, List<ScheduleEVVInfoDto> HHASchedulesList)
        { 
            try
            {
                HHASchedulesList = await _IClientORLOBNonEVVValidator.ValidateClientORLobNonEVVFlag(HHA, UserID, HHASchedulesList);

                if (HHASchedulesList.Count() > 0)
                {
                    //Validate HHA Branch Vendor Effective Date
                    HHASchedulesList = await _IHHABrancheffectivePeriodEVVValidator.ValidateHHABranchEffectiveEVVFlag(HHA, UserID, HHASchedulesList);
                }

                if (HHASchedulesList.Count() > 0)
                {
                    if (HHASchedulesList.Where(x => x.CdsPlanYearService > 0).Count() > 0)
                    {
                        //Validate for Client CDS Auth service 
                        HHASchedulesList = await _ICDSAuthServiceNonEVVValidator.ValidateCDSClientAuthServiceNonEVVFlag(HHA, UserID, HHASchedulesList.Where(x => x.CdsPlanYearService > 0).ToList());
                    }

                    if (HHASchedulesList.Where(x => x.CdsPlanYearService == 0).Count() > 0)
                    {
                        //Validate Payer EVV Flag
                        HHASchedulesList = await _IPayerNonEVVValidator.ValidatePayerNonEVVFlag(HHA, UserID, HHASchedulesList.Where(x => x.CdsPlanYearService == 0).ToList());
                     
                        //Validate Service Non EVV Flag 
                        HHASchedulesList = await _IServiceNonEVVValidator.ValidateServiceNonEVVFlag(HHA, UserID, HHASchedulesList.Where(x => x.CdsPlanYearService == 0).ToList());
                    }

                    if (HHASchedulesList.Where(y => y.CAREGIVER > 0).Count() > 0)
                    {
                        //Validate Caregiver Disc EVV flag
                        HHASchedulesList = await _ICaregiverDiscNonEVVValidator.ValidateClincianDiscNonEVVFlag(HHA, UserID, HHASchedulesList.Where(y => y.CAREGIVER > 0).ToList());
                    }
                    
                    if (HHASchedulesList.Where(y => y.CAREGIVER == 0).Count() > 0) 
                        HHASchedulesList.Where(y => y.CAREGIVER == 0).ToList().ForEach(x => x.IsClinicianDiscEVV = true);

                    var SplitEVVschedules_Caluculate = HHASchedulesList.Where(x => (x.IsSplitForBilling == true && x.IsHHABranchEVV == true && x.IsClientORLOBEVV == true && x.IsClinicianDiscEVV == true && (x.IsPayerEVV == false || x.IsServiceEVV == false) )).ToList();

                    if (SplitEVVschedules_Caluculate.Count() > 0)
                    {
                        var SplitEVVSChedules = await _ICaregiverTaskChildSchedulesEVVValidator.ValidateSplitschedulesEVVFlag(HHA, UserID, SplitEVVschedules_Caluculate);
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
                }

                HHASchedulesList = FilterEVVscheduleList(HHASchedulesList); 
            }
            catch (Exception ex)
            {
                throw;
            }
             
            return HHASchedulesList;
        }

        public List<ScheduleEVVInfoDto> FilterEVVscheduleList(List<ScheduleEVVInfoDto> HHASchedulesList)
        {
            List<ScheduleEVVInfoDto> EVVschedules = new List<ScheduleEVVInfoDto>();
            HHASchedulesList.ForEach(x => x.IsRealEVV = false);
            var RealEVVschedules = HHASchedulesList.Where(x => (((x.IsPayerEVV == true && x.IsServiceEVV == true) || x.IsCdsAuthServiceEVV == true)
                                          && x.IsHHABranchEVV == true && x.IsClientORLOBEVV == true && x.IsClinicianDiscEVV == true)).ToList();
              
            RealEVVschedules.ForEach(x => x.IsRealNonEVV = false);
            RealEVVschedules.ForEach(x => x.IsRealEVV = true);
            return RealEVVschedules;
        }
    }
}
