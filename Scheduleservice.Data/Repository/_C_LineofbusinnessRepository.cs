using Scheduleservice.Core.Entities;
using Scheduleservice.Data.Connections; 
using Scheduleservice.Services.Infrastructure.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Data.Repository
{
    public class _C_LineofbusinnessRepository : I_C_LineofbusinessRepository
    {
        
        private readonly IConnectionProvider _connectionProvider;

        public _C_LineofbusinnessRepository(IConnectionProvider connectionProvider)
        {
            
            this._connectionProvider = connectionProvider;
        }

        public async Task<IEnumerable<LineofbusinessEntity>> GetHHALOBs(int HHA, int UserID, bool ISEVVEnabled)
        {

            IEnumerable<LineofbusinessEntity> EVVLOBS = new List<LineofbusinessEntity>(); var sqlQuery = "select LOB_ID from _C_LineOfBusiness Where isnull(Enable_EVV, 0) = @Enable_EVV and _C_LineOfBusiness.HHA = @HHA ";
            object parameter = new { HHA = HHA, Enable_EVV = ISEVVEnabled };
            using (var uow = this._connectionProvider.Connect(HHA, UserID))
            {
                try
                {

                    IGenericRepository<LineofbusinessEntity> _genericRepository = new GenericMasterRepository<LineofbusinessEntity>(uow);
                    EVVLOBS = await _genericRepository.Select(sqlQuery, parameter).ConfigureAwait(true);

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

            return EVVLOBS;
        }
    }
}
