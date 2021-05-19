using Scheduleservice.Core.Entities;
using Scheduleservice.Data.Connections; 
using Scheduleservice.Services.Infrastructure.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Data.Repository
{
    public class CaregiverTaskChildSchedulesRepository : ICaregiverTaskChildSchedulesRepository
    {
      
        private readonly IConnectionProvider _connectionProvider;

        public CaregiverTaskChildSchedulesRepository(IConnectionProvider connectionProvider)
        { 
            this._connectionProvider = connectionProvider;
        }

        public async Task<IEnumerable<CaregiverTaskChildSchedulesEntity>> GetSplitscheduleDetails(int HHA, int UserID, IEnumerable<int> scheduleList)
        {
            IEnumerable<CaregiverTaskChildSchedulesEntity> ChildscheduleList = new List<CaregiverTaskChildSchedulesEntity>();

            try
            { 
                var sqlQuery = " Select Child_Schedule_Id, PARENT_CGTASK_ID, PAYMENT_SOURCE, SERVICECODE_ID, ISNULL(isEvvschedule, 0) isEvvschedule, " + 
		                        "ISNULL(isEvvScheduleDirty, 0) isEvvScheduleDirty "+
                                " from CaregiverTaskChildSchedules With(Nolock) " +
                                " Where CaregiverTaskChildSchedules.HHA = @HHA " +
                                "   and ISNULL(CaregiverTaskChildSchedules.isDeleted, 0) = 0 " +
                                "   and PARENT_CGTASK_ID IN @scheduleList ";

                object parameter = new { HHA = HHA, scheduleList = scheduleList };
                

                //using Dapper
                using (var uow = this._connectionProvider.Connect(HHA,UserID))
                {
                    IGenericRepository<CaregiverTaskChildSchedulesEntity> _genericRepository = new GenericMasterRepository<CaregiverTaskChildSchedulesEntity>(uow);
                    ChildscheduleList = await _genericRepository.Select(sqlQuery, parameter).ConfigureAwait(true);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ChildscheduleList;
        }

    }
}
