using Microsoft.Extensions.Options;
using Scheduleservice.Core.Entities;
using Scheduleservice.Data.Connections;
using Scheduleservice.Data.Models;
using Scheduleservice.Data.UnitofWorks;
using Scheduleservice.Services.Infrastructure.RepositoryContracts;
using System;
using System.Threading.Tasks;

namespace Scheduleservice.Data.Repository
{
    public class AgencyMasterListRepository : IAgencyMasterListRepository
    {
        private readonly IOptions<ConnectionProviderOptions> _connectionProviderOptions;
        string connectionMasterString = "";

        public AgencyMasterListRepository(IOptions<ConnectionProviderOptions> connectionProviderOptions)
        {
            _connectionProviderOptions = connectionProviderOptions;
            connectionMasterString = _connectionProviderOptions.Value.connctionMasterstring;
        }
    

        public async Task<AgencyListEntity> GetAgency(int HHAID)
        {
            AgencyListEntity moidfieddataResult = new AgencyListEntity();
            var sql = "select HHA_ID, DatabaseName from [dbo].[AgencyList] With(Nolock) where HHA_ID=@HHAID";
            object parameter = new { HHAID = HHAID };

            using (var uow = new UnitOfWork(connectionMasterString))
            {
                try
                {
                    IGenericRepository<AgencyListEntity> _genericRepository = new GenericMasterRepository<AgencyListEntity>(uow);
                    moidfieddataResult = await _genericRepository.SelectSingle(sql, parameter).ConfigureAwait(true);
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

            return moidfieddataResult;
        }
    }
}
