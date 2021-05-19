using Scheduleservice.Core.Entities;
using Scheduleservice.Data.Connections; 
using Scheduleservice.Data.Repository;
using Scheduleservice.Services.Infrastructure.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Data.Repository
{
    public class PaymentSourcesBranchesRepository : IPaymentSourcesBranchesRepository
    {
         private readonly IConnectionProvider _connectionProvider;

        public PaymentSourcesBranchesRepository(IConnectionProvider connectionProvider)
        { 
            this._connectionProvider = connectionProvider;
        }

        public async Task<IEnumerable<PaymentSourcesBranchesEntity>> GetHHAPayerBranches(int HHA, int UserID, IEnumerable<int> Payers)
        {
            IEnumerable<PaymentSourcesBranchesEntity> EVVPayerBranches = new List<PaymentSourcesBranchesEntity>();

            try
            {
                var sqlQuery = "";

                if (Payers.Count() > 2100)
                {
                    sqlQuery = "select HHA, Branch_ID, PaymentSource_ID,EvvAggregatorVendorVersionMasterID, isnull(EnableEvv, 0) EnableEvv  from PaymentSourcesBranches With(Nolock) " +
                                        "Where PaymentSourcesBranches.HHA = @HHA ";
                }
                else
                {
                    sqlQuery = "select HHA, Branch_ID, PaymentSource_ID,EvvAggregatorVendorVersionMasterID, isnull(EnableEvv, 0) EnableEvv  from PaymentSourcesBranches With(Nolock) " +
                                        "Where PaymentSourcesBranches.HHA = @HHA and PaymentSource_ID IN @Payers ";
                }

                object parameter = new { HHA = HHA, Payers = Payers};
                

                //using Dapper
                using (var uow = this._connectionProvider.Connect(HHA,UserID))
                {
                    IGenericRepository<PaymentSourcesBranchesEntity> _genericRepository = new GenericMasterRepository<PaymentSourcesBranchesEntity>(uow);
                    EVVPayerBranches = await _genericRepository.Select(sqlQuery, parameter).ConfigureAwait(true);
                } 
            }
            catch (Exception ex)
            { 
                throw;
            }

            return EVVPayerBranches;
        }
    }
}
