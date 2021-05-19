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
    public class PayerNonEVVValidator : IPayerNonEVVValidator
    {
        private IPaymentsourcesAdditionalRepository _IPaymentsourcesAdditional;
        private IPaymentSourcesBranchesRepository _IPaymentSourcesBranches;

        public PayerNonEVVValidator(IPaymentsourcesAdditionalRepository payers, IPaymentSourcesBranchesRepository paymentSourcesBranches)
        {
            _IPaymentsourcesAdditional = payers;
            _IPaymentSourcesBranches = paymentSourcesBranches;
        }


        public async Task<List<ScheduleEVVInfoDto>> ValidatePayerNonEVVFlag(int HHA, int UserID, List<ScheduleEVVInfoDto> HHASchedulesList)
        {
            List<ScheduleEVVInfoDto> OutHHASchedulesList = new List<ScheduleEVVInfoDto>();
            OutHHASchedulesList = HHASchedulesList;

            try
            {
                var SchedulePayers = OutHHASchedulesList.Select(x => x.PAYMENT_SOURCE).Distinct();

                //Get schedule EVV Payer Branches
                var ScheduleNonEVVPayerBranchList = await _IPaymentSourcesBranches.GetHHAPayerBranches(HHA, UserID, SchedulePayers);
                //Get EVV Payers
                var NonEVVPayers = await _IPaymentsourcesAdditional.GetHHAPayers(HHA, UserID, SchedulePayers);

                OutHHASchedulesList.Where(y => y.IsRealNonEVV == false).ToList().ForEach(x => x.IsRealNonEVV = true);

                //Get schedule Payer Branch matching Vendor
                if (ScheduleNonEVVPayerBranchList.Where(x => x.EvvAggregatorVendorVersionMasterID > 0).Count() > 0 && NonEVVPayers.Count() > 0)
                {
                    OutHHASchedulesList.ForEach(x => x.EvvAggregatorVendorVersionMasterID = ScheduleNonEVVPayerBranchList.Where(p => p.PaymentSource_ID == x.PAYMENT_SOURCE && p.Branch_ID == x.HHA_BRANCH_ID).Count() > 0 ?
                                                    ScheduleNonEVVPayerBranchList.Where(p => p.PaymentSource_ID == x.PAYMENT_SOURCE && p.Branch_ID == x.HHA_BRANCH_ID).ToList()[0].EvvAggregatorVendorVersionMasterID : 0
                                              );
                }

                if (NonEVVPayers.Count() > 0)
                {
                    //Get schedule Payer profile matching Vendor

                    //OutHHASchedulesList.ForEach(x => x.EvvAggregatorVendorVersionMasterID = NonEVVPayers.Where(p => p.PayerSourceId == x.PAYMENT_SOURCE).Count() > 0 ?
                    //                            NonEVVPayers.Where(p => p.PayerSourceId == x.PAYMENT_SOURCE).ToList()[0].EvvAggregatorVendorVersionMasterID : 0
                    //                      );

                    OutHHASchedulesList.Join(NonEVVPayers, (sch) => sch.PAYMENT_SOURCE, (cli) => cli.PayerSourceId,
                                     (sch, cli) =>
                                     {
                                         sch.EvvAggregatorVendorVersionMasterID = cli.EvvAggregatorVendorVersionMasterID;
                                         return sch;
                                     }
                                     ).ToList();

                }
                else 
                    OutHHASchedulesList.ForEach(y => y.IsPayerEVV = true); 

                //if vendor version id is available then mark scchedue as evv 
                if (OutHHASchedulesList.Where(x => x.EvvAggregatorVendorVersionMasterID > 0).Count() > 0)
                {
                    OutHHASchedulesList.Where(x => x.EvvAggregatorVendorVersionMasterID > 0).ToList().ForEach(y => y.IsRealNonEVV = false);
                    OutHHASchedulesList.Where(x => x.IsRealNonEVV == false).ToList().ForEach(y => y.IsPayerEVV = true); 
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
