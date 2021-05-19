using Microsoft.Extensions.Options; 
using Scheduleservice.Data.Models;
using Scheduleservice.Data.UnitofWorks;
using Scheduleservice.Services.Infrastructure.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Scheduleservice.Data.Connections
{
    public class ConnectionProvider : IConnectionProvider
    {
        private readonly IOptions<ConnectionProviderOptions> _connectionProviderOptions;
        private readonly IAgencyMasterListRepository _agencyMasterListRepository;
        private readonly IDatabaselistRepository _databaselistRepository;

        public ConnectionProvider(IOptions<ConnectionProviderOptions> connectionProviderOptions, IAgencyMasterListRepository agencyMasterListRepository, IDatabaselistRepository databaselistRepository)
        {
            _connectionProviderOptions = connectionProviderOptions;
            
            _agencyMasterListRepository = agencyMasterListRepository;
            _databaselistRepository = databaselistRepository;
        }
         

        public IUnitOfWork Connect(int HHA, int UserID)
        {
            var connectionString = _connectionProviderOptions.Value.connctionstring;
            try
            {
                var AgencyListResult =   _agencyMasterListRepository.GetAgency(HHA).Result;
                var DatabaselistResult =   _databaselistRepository.GetAllDataBaseList().Result;                

                var AgencyDatabase = AgencyListResult.DatabaseName;
                var AgencyServerName = DatabaselistResult.Where(x => x.DatabaseName == AgencyDatabase).FirstOrDefault().Server; 

                if (connectionString.Contains("$SERVER_NAME"))
                {
                    connectionString = connectionString.Replace("$SERVER_NAME$", AgencyServerName).Replace("$DATABASE_NAME$", AgencyDatabase);
                }

                return new UnitOfWork(connectionString);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IUnitOfWork ConnectMaster()
        {
            var connectionString = _connectionProviderOptions.Value.connctionMasterstring;
             
            return new UnitOfWork(connectionString);

        }
    }
}
