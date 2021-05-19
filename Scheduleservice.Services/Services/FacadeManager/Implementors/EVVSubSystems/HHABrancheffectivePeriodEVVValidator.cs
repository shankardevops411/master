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
    public class HHABrancheffectivePeriodEVVValidator: IHHABrancheffectivePeriodEVVValidator
    {
        private I_C_EVVConfigurationsRepository _I_C_EVVConfigurations;

        public HHABrancheffectivePeriodEVVValidator(I_C_EVVConfigurationsRepository c_EVVConfigurations)
        { 
            _I_C_EVVConfigurations = c_EVVConfigurations; 
        }

        public async Task<List<ScheduleEVVInfoDto>> ValidateHHABranchEffectiveEVVFlag(int HHA, int UserID, List<ScheduleEVVInfoDto> HHASchedulesList)
        {
            List<ScheduleEVVInfoDto> OutHHASchedulesList = new List<ScheduleEVVInfoDto>();
            OutHHASchedulesList = HHASchedulesList;

            try
            {
                var SchedulesBranches = OutHHASchedulesList.Select(c => c.HHA_BRANCH_ID).Distinct();
                //Get HHA Branch EVV Config
                var EVVEnabledBranchDetails = await _I_C_EVVConfigurations.GetEVVBranchVendorDetails(HHA, UserID, SchedulesBranches);
                var DefaultDate = new DateTime();
              

                //get the schedule vendor effective date
                if (EVVEnabledBranchDetails.Count() > 0)
                {
                    OutHHASchedulesList.ForEach(x => x.IsRealEVV = false);
                    OutHHASchedulesList.ForEach(x => x.EVVBranchVendorEffectiveDate = EVVEnabledBranchDetails.Where(y => y.HHABranchID == x.HHA_BRANCH_ID && y.EvvVendorVersionMasterID == x.EvvAggregatorVendorVersionMasterID).Count() > 0 ?
                                                EVVEnabledBranchDetails.Where(y => y.HHABranchID == x.HHA_BRANCH_ID && y.EvvVendorVersionMasterID == x.EvvAggregatorVendorVersionMasterID).ToList()[0].EffectiveDate : x.PLANNED_DATE
                                            );
                    OutHHASchedulesList.ForEach(x => x.EVVBranchVendorEffectiveDate = x.EVVBranchVendorEffectiveDate == DefaultDate ? x.PLANNED_DATE : x.EVVBranchVendorEffectiveDate);
                }

                //if schedule date befor the vendor effective date , consider those as non evv 
                if (OutHHASchedulesList.Where(x => x.PLANNED_DATE >= x.EVVBranchVendorEffectiveDate).Count() > 0)
                {
                    OutHHASchedulesList.ForEach(x => x.IsRealEVV = x.PLANNED_DATE >= x.EVVBranchVendorEffectiveDate ? true : false);
                    // OutHHASchedulesList.RemoveAll(x => x.IsRealEVV == false);
                    OutHHASchedulesList.ForEach(x => x.IsHHABranchEVV = true);
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
