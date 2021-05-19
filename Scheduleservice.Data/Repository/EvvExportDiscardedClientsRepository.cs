using Scheduleservice.Core.Entities;
using Scheduleservice.Data.Connections;
using Scheduleservice.Services.Clients.Models;
using Scheduleservice.Services.Infrastructure.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Data.Repository
{
    public class EvvExportDiscardedClientsRepository : IEvvExportDiscardedClientsRepository
    {
        private readonly IConnectionProvider _connectionProvider;
        public EvvExportDiscardedClientsRepository(IConnectionProvider connectionProvider)
        {
            this._connectionProvider = connectionProvider;
        }
        public async Task<IEnumerable<EvvExportDiscardedClientsEntity>> GetEvvExportDiscardedClients(EvvExportDiscardedClientsFilters evvExportDiscardedClientsFilters)
        {
            IEnumerable<EvvExportDiscardedClientsEntity> EvvExportDiscardedClients = new List<EvvExportDiscardedClientsEntity>();
            
            var sqlQuery = "Select EvvExportDiscardedClientID, HHA, PAYMENT_SOURCE, CLIENT_ID, EvvVendorVersionMasterID  " +
                          "from EvvExportDiscardedClients With(Nolock)  Where EvvExportDiscardedClients.HHA = @HHA "; 

            if (evvExportDiscardedClientsFilters.PAYMENT_SOURCE > 0)
            {
                sqlQuery += " and EvvExportDiscardedClients.PAYMENT_SOURCE = @PayerId ";
            }

            if (evvExportDiscardedClientsFilters.CLIENT_ID>0)
            {
                sqlQuery += " and EvvExportDiscardedClients.CLIENT_ID =@Clientid ";
            }

            

            dynamic parameter = new { HHA = evvExportDiscardedClientsFilters.HHA, PayerId = evvExportDiscardedClientsFilters.PAYMENT_SOURCE, Clientid = evvExportDiscardedClientsFilters.CLIENT_ID };


            //using Dapper
            using (var uow = this._connectionProvider.Connect(evvExportDiscardedClientsFilters.HHA, evvExportDiscardedClientsFilters.UserId))
            {
                try
                {
                    IGenericRepository<EvvExportDiscardedClientsEntity> _genericRepository = new GenericMasterRepository<EvvExportDiscardedClientsEntity>(uow);
                    EvvExportDiscardedClients = await _genericRepository.Select(sqlQuery, parameter);
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


            return EvvExportDiscardedClients;
        }

      
    }
}
