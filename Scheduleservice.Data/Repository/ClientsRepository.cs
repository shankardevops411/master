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
    public class ClientsRepository: IClientsRepository
    {
        private readonly IConnectionProvider _connectionProvider;
        public ClientsRepository(IConnectionProvider connectionProvider)
        {
            this._connectionProvider = connectionProvider;
        }

        public async Task<IEnumerable<ClientsEntity>> GetALLClients( ClientsFilters clientsFilters)
        {

            IEnumerable<ClientsEntity> clientsEntities = new List<ClientsEntity>();

            var sqlQuery = "select Clients.CLIENT_ID, Clients.HHA_BRANCH_ID , Clients.LOB_ID, ClientAdditionalDetails.Enable_EVV Enable_EVV  " +
                                      "from Clients With(Nolock), ClientAdditionalDetails With(Nolock), ClientAdditionalDetails2 With(Nolock) " +
                                      "Where Clients.CLIENT_ID = ClientAdditionalDetails.CLIENT " +
                                      " and Clients.CLIENT_ID= ClientAdditionalDetails2.CLIENT " +
                                      " and Clients.HHA = @HHA and ClientAdditionalDetails.HHA = @HHA and ClientAdditionalDetails2.HHA = @HHA ";

            object parameter = new { HHA = clientsFilters.HHA };


            //using Dapper
            using (var uow = this._connectionProvider.Connect(clientsFilters.HHA, clientsFilters.UserId))
            {
                try
                {
                    IGenericRepository<ClientsEntity> _genericRepository = new GenericMasterRepository<ClientsEntity>(uow);
                    clientsEntities = await _genericRepository.Select(sqlQuery, parameter).ConfigureAwait(true);
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


            return clientsEntities;
        }

        public async Task<IEnumerable<ClientsEntity>> GetClients(int HHA, int UserID, IEnumerable<int> clients)
        {

            IEnumerable<ClientsEntity> clientsEntities = new List<ClientsEntity>();

            var sqlQuery = "select Clients.CLIENT_ID, Clients.HHA_BRANCH_ID , Clients.LOB_ID, ClientAdditionalDetails.Enable_EVV Enable_EVV  " +
                                      "from Clients With(Nolock), ClientAdditionalDetails With(Nolock), ClientAdditionalDetails2 With(Nolock) " +
                                      "Where Clients.CLIENT_ID = ClientAdditionalDetails.CLIENT " +
                                      " and Clients.CLIENT_ID= ClientAdditionalDetails2.CLIENT " +
                                      " and Clients.CLIENT_ID IN @clients " +
                                      " and Clients.HHA = @HHA and ClientAdditionalDetails.HHA = @HHA and ClientAdditionalDetails2.HHA = @HHA ";

            object parameter = new { HHA = HHA , clients = clients };


            //using Dapper
            using (var uow = this._connectionProvider.Connect(HHA, UserID   ))
            {
                try
                {
                    IGenericRepository<ClientsEntity> _genericRepository = new GenericMasterRepository<ClientsEntity>(uow);
                    clientsEntities = await _genericRepository.Select(sqlQuery, parameter).ConfigureAwait(true);
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


            return clientsEntities;
        }

        public async Task<IEnumerable<ClientsEntity>> GetClientsAdditional(  ClientsFilters clientsFilters)
        {

            IEnumerable<ClientsEntity> clientsAdditionalEntities = new List<ClientsEntity>();

            var sqlQuery = "select ClientAdditionalDetails.Enable_EVV Enable_EVV  " +
                                      "from ClientAdditionalDetails With(Nolock) " +
                                      "Where ClientAdditionalDetails.HHA = @HHA ";

            object parameter = new { HHA = clientsFilters.HHA };


            //using Dapper
            using (var uow = this._connectionProvider.Connect(clientsFilters.HHA, clientsFilters.UserId))
            {
                try
                {
                    IGenericRepository<ClientsEntity> _genericRepository = new GenericMasterRepository<ClientsEntity>(uow);
                    clientsAdditionalEntities = await _genericRepository.Select(sqlQuery, parameter).ConfigureAwait(true);
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


            return clientsAdditionalEntities;
        }

        public async Task<IEnumerable<ClientsEntity>> GetClientsAdditional2( ClientsFilters clientsFilters)
        {
            IEnumerable<ClientsEntity> clientsAdditional2Entities = new List<ClientsEntity>();

            var sqlQuery = "select ClientAdditionalDetails2.HHA, ClientAdditionalDetails2.CLIENT  " +
                                      "from ClientAdditionalDetails2 With(Nolock) " +
                                      "Where ClientAdditionalDetails2.HHA = @HHA ";

            object parameter = new { HHA = clientsFilters.HHA };


            //using Dapper
            using (var uow = this._connectionProvider.Connect(clientsFilters.HHA, clientsFilters.UserId))
            {
                try
                {
                    IGenericRepository<ClientsEntity> _genericRepository = new GenericMasterRepository<ClientsEntity>(uow);
                    clientsAdditional2Entities = await _genericRepository.Select(sqlQuery, parameter).ConfigureAwait(true);
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


            return clientsAdditional2Entities;
        }

        public async Task<IEnumerable<ClientsEntity>> GetClientsBasic(ClientsFilters clientsFilters)
        {

            IEnumerable<ClientsEntity> clientsEntities = new List<ClientsEntity>();

            var sqlQuery = "select Clients.CLIENT_ID, Clients.HHA_BRANCH_ID , Clients.LOB_ID" +
                                      "from Clients With(Nolock)" +
                                      "Where Clients.HHA = @HHA";

            object parameter = new { HHA = clientsFilters.HHA };


            //using Dapper
            using (var uow = this._connectionProvider.Connect(clientsFilters.HHA, clientsFilters.UserId))
            {
                try
                {
                    IGenericRepository<ClientsEntity> _genericRepository = new GenericMasterRepository<ClientsEntity>(uow);
                    clientsEntities = await _genericRepository.Select(sqlQuery, parameter).ConfigureAwait(true);
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


            return clientsEntities;
        }
    }
}
