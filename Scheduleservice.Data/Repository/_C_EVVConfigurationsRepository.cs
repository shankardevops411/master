using Scheduleservice.Core.Entities;
using Scheduleservice.Data.Connections; 
using Scheduleservice.Services.Infrastructure.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Data.Repository
{
    public class _C_EVVConfigurationsRepository : I_C_EVVConfigurationsRepository
    {
         
        private readonly IConnectionProvider _connectionProvider;

        public _C_EVVConfigurationsRepository(IConnectionProvider connectionProvider)
        {
             
            this._connectionProvider = connectionProvider;
        }

        public async Task<IEnumerable<_C_EVVConfigurationsEntity>> GetEVVBranchVendorDetails(int HHA, int UserID, IEnumerable<int> Branches)
        {
            IEnumerable<_C_EVVConfigurationsEntity> EVVBranchVendorDetails = new List<_C_EVVConfigurationsEntity>();
            var sqlQuery = "select EvvConfigurationID, HHABranchID,EvvVendorVersionMasterID, EffectiveDate from _C_EvvConfigurations Where HHA = @HHA ";
            object parameter = new { HHA = HHA, HHABranchID = Branches };         
                    
            using (var uow = this._connectionProvider.Connect(HHA, UserID))
            {
                try
                {
                    IGenericRepository<_C_EVVConfigurationsEntity> _genericRepository = new GenericMasterRepository<_C_EVVConfigurationsEntity>(uow);
                    EVVBranchVendorDetails = await _genericRepository.Select(sqlQuery, parameter).ConfigureAwait(true);
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
           

            return EVVBranchVendorDetails;
        }

        public async Task<IEnumerable<_C_EVVConfigurationsEntity>> GetEVVBranchDetails(int HHA, int UserID)
        {
            IEnumerable<_C_EVVConfigurationsEntity> EVVBranchVendorDetails = new List<_C_EVVConfigurationsEntity>();
            var sqlQuery = "select EvvConfigurationID, HHABranchID,EvvVendorVersionMasterID, EffectiveDate from _C_EvvConfigurations Where HHA = @HHA ";

            object parameter = new { HHA = HHA };

            using (var uow = this._connectionProvider.Connect(HHA, UserID))
            {
                try
                {
                    IGenericRepository<_C_EVVConfigurationsEntity> _genericRepository = new GenericMasterRepository<_C_EVVConfigurationsEntity>(uow);
                    EVVBranchVendorDetails = await _genericRepository.Select(sqlQuery, parameter).ConfigureAwait(true);
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

            return EVVBranchVendorDetails;
        }

    }
}
