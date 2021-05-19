using Dapper;
using Scheduleservice.Core.Entities;
using Scheduleservice.Data.Connections;
using Scheduleservice.Services.Infrastructure.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Data.Repository
{
    public class CaregiverTaskEvvSchedulesRepository : ICaregiverTaskEvvSchedulesRepository
    {
        private readonly IConnectionProvider _connectionProvider;

        public CaregiverTaskEvvSchedulesRepository(IConnectionProvider connectionProvider)
        {
            this._connectionProvider = connectionProvider;
        }

        public async Task<int> DeleteCaregiverTaskEvvSchedules(int HHA, int UserId, string CaregiverTaskEvvSchedulesBachID)
        {
            int result = 0;
            string procedurename = "_S_DeleteCaregiverTaskBatchEvvSchedules";
            var parameter = new DynamicParameters();
            parameter.Add("@hha", HHA);
            parameter.Add("@UserID", UserId);
            parameter.Add("@BatchID", CaregiverTaskEvvSchedulesBachID);


            //using Dapper
            using (var uow = this._connectionProvider.Connect(HHA, UserId))
            {
                try
                {
                    IGenericRepository<int> _genericRepository = new GenericMasterRepository<int>(uow);
                    result = await _genericRepository.ExecuteProcedure(procedurename, parameter).ConfigureAwait(true);
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

            return result;
        }

        public async Task<IEnumerable<CaregiverTaskEvvSchedulesEntity>> GetCaregiverTaskEvvSchedules(int HHA, int UserId, string CaregiverTaskEvvSchedulesBachID)
        {
            IEnumerable<CaregiverTaskEvvSchedulesEntity> CaregiverTaskBatchEvvSchedules = new List<CaregiverTaskEvvSchedulesEntity>();
            string procedurename = "_S_GetCaregiverTaskBatchEvvSchedules";
            var parameter = new DynamicParameters();
            parameter.Add("@hha", HHA);
            parameter.Add("@user", UserId);           
            parameter.Add("@BatchID", CaregiverTaskEvvSchedulesBachID);

           
            //using Dapper
            using (var uow = this._connectionProvider.Connect(HHA, UserId))
            {
                try
                {
                    IGenericRepository<CaregiverTaskEvvSchedulesEntity> _genericRepository = new GenericMasterRepository<CaregiverTaskEvvSchedulesEntity>(uow);
                      CaregiverTaskBatchEvvSchedules = await _genericRepository.SelectProcedure(procedurename, parameter).ConfigureAwait(true);
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

            return CaregiverTaskBatchEvvSchedules;
        }

        public async Task<IEnumerable<Guid>> InsertCaregiverTaskEvvSchedules(int HHA, int UserId, string CaregiverTaskEvvSchedules)
        {
            IEnumerable<Guid> CaregiverTaskEvvScheduleBatches =new  List<Guid>();
            string procedurename = "_S_CreateCaregiverTaskEvvSchedulesBatches";
            var parameter = new DynamicParameters();
            parameter.Add("@hha", HHA);
            parameter.Add("@UserID", UserId);
            parameter.Add("@CgtaskIDJSON", CaregiverTaskEvvSchedules);
            parameter.Add("@batch", 2000);
 

            //using Dapper
            using (var uow = this._connectionProvider.Connect(HHA, UserId))
            {
                try
                {
                    IGenericRepository<Guid> _genericRepository = new GenericMasterRepository<Guid>(uow);
                    CaregiverTaskEvvScheduleBatches = await _genericRepository.SelectProcedure(procedurename, parameter).ConfigureAwait(true);
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

            return CaregiverTaskEvvScheduleBatches;
        }
    }
}
