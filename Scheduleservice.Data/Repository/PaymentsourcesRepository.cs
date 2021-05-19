using Scheduleservice.Core.Entities;
using Scheduleservice.Data.Connections;
using Scheduleservice.Services.Infrastructure.RepositoryContracts;
using Scheduleservice.Services.PaymentSources.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Data.Repository
{
    public class PaymentsourcesRepository : IPaymentsourcesRepository
    {
        private readonly IConnectionProvider _connectionProvider;
        public PaymentsourcesRepository(IConnectionProvider connectionProvider)
        {

            this._connectionProvider = connectionProvider;
        }

        public async Task<IEnumerable<PaymentsourcesEntity>> GetPaymentsourceAdditional(PayerFilters payerFilters)
        {
            IEnumerable<PaymentsourcesEntity> PaymentsourceAdditional = new List<PaymentsourcesEntity>();
            var sqlQuery = " select   PaymentsourcesAdditional.IsEnableEVV " +
                                        "from PaymentsourcesAdditional With(Nolock)" +
                                        "Where PaymentsourcesAdditional.HHA = @HHA ";


            object parameter = new { HHA = payerFilters.HHA, PAYMENT_SOURCE_ID =payerFilters.PAYMENT_SOURCE_ID, PAYMENT_SOURCE_IDList= payerFilters.PAYMENT_SOURCE_IDList };

         

            //using Dapper
            using (var uow = this._connectionProvider.Connect(payerFilters.HHA, payerFilters.UserId))
            {
                try
                {
                    IGenericRepository<PaymentsourcesEntity> _genericRepository = new GenericMasterRepository<PaymentsourcesEntity>(uow);
                    PaymentsourceAdditional = await _genericRepository.Select(sqlQuery, parameter).ConfigureAwait(true);
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

            return PaymentsourceAdditional;
        }

        public async Task<IEnumerable<PaymentsourcesEntity>> GetPaymentsourceAdditional2(PayerFilters payerFilters)
        {
            IEnumerable<PaymentsourcesEntity> EVVPayers = new List<PaymentsourcesEntity>();
            var sqlQuery = " select Paymentsources.PAYMENT_SOURCE_ID, PaymentSourcesAdditional2.EvvAggregatorVendorVersionMasterID, PaymentsourcesAdditional.IsEnableEVV " +
                                        "from Paymentsources  With(Nolock), PaymentsourcesAdditional With(Nolock), PaymentSourcesAdditional2 With(Nolock)" +
                                        "Where PaymentSourcesAdditional2.HHA = @HHA " +
                                           " and PaymentsourcesAdditional.HHA = @HHA " +
                                           " and PaymentsourcesAdditional.PayerSourceId = PaymentSourcesAdditional2.Paymentsourceid ";


              object parameter = new { HHA = payerFilters.HHA, PAYMENT_SOURCE_ID =payerFilters.PAYMENT_SOURCE_ID, PAYMENT_SOURCE_IDList= payerFilters.PAYMENT_SOURCE_IDList };



            //using Dapper
            using (var uow = this._connectionProvider.Connect(payerFilters.HHA, payerFilters.UserId))
            {
                try
                {
                    IGenericRepository<PaymentsourcesEntity> _genericRepository = new GenericMasterRepository<PaymentsourcesEntity>(uow);
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

        public async Task<IEnumerable<PaymentsourcesEntity>> GetPaymentsourceBasic(PayerFilters payerFilters)
        {
            IEnumerable<PaymentsourcesEntity> EVVPayers = new List<PaymentsourcesEntity>();
            var sqlQuery = " select Paymentsources.PAYMENT_SOURCE_ID, PaymentSourcesAdditional2.EvvAggregatorVendorVersionMasterID, PaymentsourcesAdditional.IsEnableEVV " +
                                        "from Paymentsources  With(Nolock), PaymentsourcesAdditional With(Nolock), PaymentSourcesAdditional2 With(Nolock)" +
                                        "Where PaymentSourcesAdditional2.HHA = @HHA " +
                                           " and PaymentsourcesAdditional.HHA = @HHA " +
                                           " and PaymentsourcesAdditional.PayerSourceId = PaymentSourcesAdditional2.Paymentsourceid ";


            object parameter = new { HHA = payerFilters.HHA, PAYMENT_SOURCE_ID = payerFilters.PAYMENT_SOURCE_ID, PAYMENT_SOURCE_IDList = payerFilters.PAYMENT_SOURCE_IDList };



            //using Dapper
            using (var uow = this._connectionProvider.Connect(payerFilters.HHA, payerFilters.UserId))
            {
                try
                {
                    IGenericRepository<PaymentsourcesEntity> _genericRepository = new GenericMasterRepository<PaymentsourcesEntity>(uow);
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

        public async Task<IEnumerable<PaymentsourcesEntity>> GetPaymentsources(PayerFilters payerFilters)
        {
            IEnumerable<PaymentsourcesEntity> EVVPayers = new List<PaymentsourcesEntity>();
            var  sqlQuery = " select Paymentsources.PAYMENT_SOURCE_ID, PaymentSourcesAdditional2.EvvAggregatorVendorVersionMasterID, PaymentsourcesAdditional.IsEnableEVV " +
                                        "from Paymentsources  With(Nolock), PaymentsourcesAdditional With(Nolock), PaymentSourcesAdditional2 With(Nolock)" +
                                        "Where PaymentSourcesAdditional2.HHA = @HHA " +
                                           " and PaymentsourcesAdditional.HHA = @HHA " +
                                           " and PaymentsourcesAdditional.PayerSourceId = PaymentSourcesAdditional2.Paymentsourceid ";


            object parameter = new { HHA = payerFilters.HHA, PAYMENT_SOURCE_ID = payerFilters.PAYMENT_SOURCE_ID, PAYMENT_SOURCE_IDList = payerFilters.PAYMENT_SOURCE_IDList };



            //using Dapper
            using (var uow = this._connectionProvider.Connect(payerFilters.HHA, payerFilters.UserId))
            {
                try
                {
                    IGenericRepository<PaymentsourcesEntity> _genericRepository = new GenericMasterRepository<PaymentsourcesEntity>(uow);
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
