using Scheduleservice.Core.Entities;
using Scheduleservice.Data.Connections; 
using Scheduleservice.Services.Infrastructure.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Data.Repository
{
    public class PaymentSourcesAdditionalRepository : IPaymentsourcesAdditionalRepository
    {
        private readonly IConnectionProvider _connectionProvider;

        public PaymentSourcesAdditionalRepository(IConnectionProvider connectionProvider)
        {

            this._connectionProvider = connectionProvider;
        }

        public async Task<IEnumerable<PaymentSourcesAdditionalEntity>> GetHHAPayers(int HHA, int UserID, IEnumerable<int> Payers)
        {
            IEnumerable<PaymentSourcesAdditionalEntity> EVVPayers = new List<PaymentSourcesAdditionalEntity>();

            var sqlQuery = "";
            if (Payers.Count() > 2100)
            {
                sqlQuery = " select PaymentsourcesAdditional.PayerSourceId, EvvAggregatorVendorVersionMasterID, isnull(PaymentsourcesAdditional.IsEnableEVV, 0) IsEnableEVV " +
                                    "from PaymentsourcesAdditional With(Nolock), PaymentSourcesAdditional2 With(Nolock)" +
                                    "Where PaymentSourcesAdditional2.HHA = @HHA " +
                                       " and PaymentsourcesAdditional.HHA = @HHA " +
                                       " and PaymentsourcesAdditional.PayerSourceId = PaymentSourcesAdditional2.Paymentsourceid ";
            }
            else
            {
                sqlQuery = " select PaymentsourcesAdditional.PayerSourceId, EvvAggregatorVendorVersionMasterID, isnull(PaymentsourcesAdditional.IsEnableEVV, 0) IsEnableEVV " +
                                    "from PaymentsourcesAdditional With(Nolock), PaymentSourcesAdditional2 With(Nolock)" +
                                    "Where PaymentSourcesAdditional2.HHA = @HHA " +
                                       " and PaymentsourcesAdditional.HHA = @HHA " +
                                       " and PaymentsourcesAdditional.PayerSourceId = PaymentSourcesAdditional2.Paymentsourceid " +
                                       " and PaymentsourcesAdditional.PayerSourceId IN  @Payers ";
            }

            object parameter = new { HHA = HHA, Payers = Payers };


            //using Dapper
            using (var uow = this._connectionProvider.Connect(HHA, UserID))
            {
                try
                {
                    IGenericRepository<PaymentSourcesAdditionalEntity> _genericRepository = new GenericMasterRepository<PaymentSourcesAdditionalEntity>(uow);
                    EVVPayers = await _genericRepository.Select(sqlQuery, parameter).ConfigureAwait(true);
                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    uow.Dispose();
                }
            }


            return EVVPayers;
        }

    }
}
