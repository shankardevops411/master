using Microsoft.Extensions.Options;
using Scheduleservice.Core.Entities;
using Scheduleservice.Data.Connections;
using Scheduleservice.Data.Models;
using Scheduleservice.Data.UnitofWorks;
using Scheduleservice.Services.Infrastructure.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Data.Repository
{
    public class DatabaselistRepository : IDatabaselistRepository
    {
        private readonly IOptions<ConnectionProviderOptions> _connectionProviderOptions;
        string connectionMasterString = "";
        public DatabaselistRepository(IOptions<ConnectionProviderOptions> connectionProviderOptions)
        {
            _connectionProviderOptions = connectionProviderOptions;
            connectionMasterString = _connectionProviderOptions.Value.connctionMasterstring;

        }

        public async Task<IEnumerable<DatabaselistEntity>> GetAllDataBaseList()
        {
            var sql = "select DatabaseID, DatabaseName, Server from [dbo].[DatabaseList] With(Nolock)";
            object parameter = new { Status = "Active" };
            IEnumerable<DatabaselistEntity> moidfieddataResult = new List<DatabaselistEntity>();
            using (var uow = new UnitOfWork(connectionMasterString))
            {
                IGenericRepository<DatabaselistEntity> _genericRepository = new GenericMasterRepository<DatabaselistEntity>(uow);
                moidfieddataResult = await _genericRepository.SelectAll(sql).ConfigureAwait(true);
            }
            return moidfieddataResult;
        }
    }
}
