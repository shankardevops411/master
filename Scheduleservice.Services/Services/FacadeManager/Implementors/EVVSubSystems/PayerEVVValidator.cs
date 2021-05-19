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
    public class PayerEVVValidator: IPayerEVVValidator
    {
        private IPaymentsourcesAdditionalRepository _IPaymentsourcesAdditional;
        private IPaymentSourcesBranchesRepository _IPaymentSourcesBranches;

        public PayerEVVValidator(IPaymentsourcesAdditionalRepository payers, IPaymentSourcesBranchesRepository paymentSourcesBranches)
        {
            _IPaymentsourcesAdditional = payers;
            _IPaymentSourcesBranches = paymentSourcesBranches;
        }


        public async Task<List<ScheduleEVVInfoDto>> ValidatePayerEVVFlag(int HHA, int UserID, List<ScheduleEVVInfoDto> HHASchedulesList)
        {
            List<ScheduleEVVInfoDto> OutHHASchedulesList = new List<ScheduleEVVInfoDto>();
            OutHHASchedulesList = HHASchedulesList;

            try
            {
                var SchedulePayers = OutHHASchedulesList.Select(x => x.PAYMENT_SOURCE).Distinct();

                //Get schedule EVV Payer Branches
                var ScheduleEVVPayerBranchList = await _IPaymentSourcesBranches.GetHHAPayerBranches(HHA, UserID, SchedulePayers);
                //Get EVV Payers
                var EVVPayers = await _IPaymentsourcesAdditional.GetHHAPayers(HHA, UserID, SchedulePayers);

                //OutHHASchedulesList.ForEach(x => x.IsRealEVV = true);

                //Get schedule Payer Branch matching Vendor
                if (ScheduleEVVPayerBranchList.Count() > 0)
                {
                    OutHHASchedulesList.ForEach(x => x.EvvAggregatorVendorVersionMasterID = ScheduleEVVPayerBranchList.Where(p => p.PaymentSource_ID == x.PAYMENT_SOURCE && p.Branch_ID == x.HHA_BRANCH_ID).Count() > 0 ?
                                                    ScheduleEVVPayerBranchList.Where(p => p.PaymentSource_ID == x.PAYMENT_SOURCE && p.Branch_ID == x.HHA_BRANCH_ID).ToList()[0].EvvAggregatorVendorVersionMasterID : 0
                                              );
                }

                if (EVVPayers.Count() > 0)
                {
                    //Get schedule Payer profile matching Vendor
                    if (OutHHASchedulesList.Where(x => x.EvvAggregatorVendorVersionMasterID == 0).Count() > 0)
                    {
                        OutHHASchedulesList.Where(x => x.EvvAggregatorVendorVersionMasterID == 0).ToList().ForEach(x => x.EvvAggregatorVendorVersionMasterID = EVVPayers.Where(p => p.PayerSourceId == x.PAYMENT_SOURCE).Count() > 0 ?
                                                    EVVPayers.Where(p => p.PayerSourceId == x.PAYMENT_SOURCE).ToList()[0].EvvAggregatorVendorVersionMasterID : 0
                                              );
                    }
                }

                //if vendor version id is not available then mark scchedue as non evv 
                if (OutHHASchedulesList.Where(x => x.EvvAggregatorVendorVersionMasterID == 0).Count() > 0)
                {
                    // OutHHASchedulesList.ForEach(x => x.IsRealEVV = x.EvvAggregatorVendorVersionMasterID > 0 ? true : false);
                    OutHHASchedulesList.RemoveAll(x => x.EvvAggregatorVendorVersionMasterID == 0); 
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
