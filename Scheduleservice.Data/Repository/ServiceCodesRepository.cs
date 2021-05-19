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
    public class ServiceCodesRepository: IServiceCodesRepository
    {
   
        private readonly IConnectionProvider _connectionProvider;

        public ServiceCodesRepository(IConnectionProvider connectionProvider)
        {
            this._connectionProvider = connectionProvider;
        }

        public async Task<IEnumerable<ServiceCodesEntity>> GetEVVORNonEVVServices(int HHA, int UserID, IEnumerable<int> Services, bool isEvvEnabled)
        {
            IEnumerable<ServiceCodesEntity> EVVServices = new List<ServiceCodesEntity>();
            try
            {
                var sqlQuery = "";
                if (Services.Count() > 2100)
                {
                    sqlQuery = "Select SERVICE_CODE_ID from ServiceCodes With(Nolock) Where HHA = @HHA and isnull(isEvvEnabled, 0) = @isEvvEnabled ";
                }
                else
                {
                    sqlQuery = "Select SERVICE_CODE_ID from ServiceCodes With(Nolock) Where HHA = @HHA and isnull(isEvvEnabled, 0) = @isEvvEnabled " +
                                  "and SERVICE_CODE_ID in @Services ";
                } 

                object parameter = new { HHA = HHA, Services = Services, isEvvEnabled = isEvvEnabled };
                
                //using Dapper
                using (var uow = this._connectionProvider.Connect(HHA,UserID))
                {
                    IGenericRepository<ServiceCodesEntity> _genericRepository = new GenericMasterRepository<ServiceCodesEntity>(uow);
                    EVVServices = await _genericRepository.Select(sqlQuery, parameter).ConfigureAwait(true);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return EVVServices;
        }
    }
}
