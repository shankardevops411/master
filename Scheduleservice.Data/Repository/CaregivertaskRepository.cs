using Dapper;
using Scheduleservice.Core.Entities;
using Scheduleservice.Data.Connections; 
using Scheduleservice.Services.Infrastructure.RepositoryContracts;
using Scheduleservice.Services.Schedules.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Data;
using System.Linq;

namespace Scheduleservice.Data.Repository
{
    public class CaregivertaskRepository : ICaregivertasksRepository
    {

        private readonly IConnectionProvider _connectionProvider;

        public CaregivertaskRepository(IConnectionProvider connectionProvider)
        {
            this._connectionProvider = connectionProvider;
        }

        public async Task<IEnumerable<CaregivertasksEntity>> GetHHAEVVSchedules(int HHA, int UserID, string StartDate, string EndDate)
        {
            IEnumerable<CaregivertasksEntity> EVVSchedules = new List<CaregivertasksEntity>();


            var sqlQuery = "Select CGTASK_ID, CaregiverTasks.CLIENT_ID, SERVICECODE_ID, PAYMENT_SOURCE,CAREGIVER,isEvvschedule, isEvvScheduleDirty, PLANNED_DATE, " +
                                    "Clients.HHA_BRANCH_ID,CaregiverTaskAdditional2.CdsPlanYearService, isnull(CaregiverTaskAdditional.IsSplitForBilling, 0) IsSplitForBilling   " +
                           "from CaregiverTasks With(nolock), CaregiverTaskAdditional With(Nolock), Clients With(Nolock), CaregiverTaskAdditional2 With(Nolock) " +
                               "Where CaregiverTasks.CGTASK_ID = CaregiverTaskAdditional.CGTaskID " +
                               "and Clients.CLIENT_ID = CaregiverTasks.CLIENT_ID " +
                               "and CaregiverTaskAdditional.CGTaskID =  CaregiverTaskAdditional2.CgTaskID " +
                               "and Clients.HHA = @HHA " +
                               "and CaregiverTasks.HHA = @HHA " +
                               "and CaregiverTaskAdditional.HHA = @HHA " +
                               "and CaregiverTaskAdditional2.HHA = @HHA " +
                               // " and CGTASK_ID = 8817399 " + 
                               "and CaregiverTasks.PLANNED_DATE between @StartDate and @EndDate " +
                               "and CaregiverTaskAdditional.isEvvschedule = 1 ";

            object parameter = new { HHA = HHA, StartDate = StartDate, EndDate = EndDate };


            //using Dapper
            using (var uow = this._connectionProvider.Connect(HHA, UserID))
            {
                try
                {
                    IGenericRepository<CaregivertasksEntity> _genericRepository = new GenericMasterRepository<CaregivertasksEntity>(uow);
                    EVVSchedules = await _genericRepository.Select(sqlQuery, parameter).ConfigureAwait(true);
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


            return EVVSchedules;
        }

        public async Task<IEnumerable<CaregivertasksEntity>> GetHHANonEVVSchedules(int HHA, int UserID, string StartDate, string EndDate)
        {
            IEnumerable<CaregivertasksEntity> EVVSchedules = new List<CaregivertasksEntity>();

            var sqlQuery = "Select CGTASK_ID, CaregiverTasks.CLIENT_ID, SERVICECODE_ID, PAYMENT_SOURCE,CAREGIVER,isEvvschedule, isEvvScheduleDirty, PLANNED_DATE, " +
                                    "Clients.HHA_BRANCH_ID,CaregiverTaskAdditional2.CdsPlanYearService , isnull(CaregiverTaskAdditional.IsSplitForBilling, 0) IsSplitForBilling  " +
                           "from CaregiverTasks With(nolock), CaregiverTaskAdditional With(Nolock), Clients With(Nolock), CaregiverTaskAdditional2 With(Nolock) " +
                               "Where CaregiverTasks.CGTASK_ID = CaregiverTaskAdditional.CGTaskID " +
                               "and Clients.CLIENT_ID = CaregiverTasks.CLIENT_ID " +
                               "and CaregiverTaskAdditional.CGTaskID =  CaregiverTaskAdditional2.CgTaskID " +
                               "and Clients.HHA = @HHA " +
                               "and CaregiverTasks.HHA = @HHA " +
                               "and CaregiverTaskAdditional.HHA = @HHA " +
                               "and CaregiverTaskAdditional2.HHA = @HHA " +
                               "and CaregiverTasks.PLANNED_DATE between @StartDate and @EndDate " +
                               "and isnull(CaregiverTaskAdditional.isEvvschedule,0) = 0 " +
                               "and isnull(CaregiverTaskAdditional.isEvvScheduleDirty,0) = 0 ";

            object parameter = new { HHA = HHA, StartDate = StartDate, EndDate = EndDate };


            //using Dapper
            using (var uow = this._connectionProvider.Connect(HHA, UserID))
            {
                try
                {
                    IGenericRepository<CaregivertasksEntity> _genericRepository = new GenericMasterRepository<CaregivertasksEntity>(uow);
                    EVVSchedules = await _genericRepository.Select(sqlQuery, parameter).ConfigureAwait(true);
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


            return EVVSchedules;
        }

        public async Task<bool> UpdatescheduleasRealEVV(int HHA, int UserID, IEnumerable<int> CgtaskIds)
        {
            var ret = true;

            var sqlQuery = "Update CaregiverTaskAdditional set isEvvschedule = 1, isEvvScheduleDirty= 0 " +
                           "    Where HHA = @HHA and CGTaskID in @CgtaskIds ";

            object parameter = new { HHA = HHA, CgtaskIds = CgtaskIds };


            //using Dapper
            using (var uow = this._connectionProvider.Connect(HHA, UserID))
            {
                try
                {
                    IGenericRepository<CaregivertasksEntity> _genericRepository = new GenericMasterRepository<CaregivertasksEntity>(uow);
                    await _genericRepository.Execute(sqlQuery, parameter).ConfigureAwait(false);
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

            return ret;
        }

        public async Task<bool> UpdatescheduleasNonEVV(int HHA, int UserID, IEnumerable<int> CgtaskIds)
        {
            var ret = true;

            var sqlQuery = "Update CaregiverTaskAdditional set isEvvschedule = 0, isEvvScheduleDirty= 0 " +
                           "    Where HHA = @HHA and CGTaskID in @CgtaskIds ";

            object parameter = new { HHA = HHA, CgtaskIds = CgtaskIds };


            // var connection = new SqlConnection(conn);
            //using Dapper
            using (var uow = this._connectionProvider.Connect(HHA, UserID))
            {
                try
                {
                    IGenericRepository<CaregivertasksEntity> _genericRepository = new GenericMasterRepository<CaregivertasksEntity>(uow);
                    var test = await _genericRepository.Execute(sqlQuery, parameter).ConfigureAwait(false);
                    // var test = await connection.ExecuteAsync(sqlQuery, parameter).ConfigureAwait(false);
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

            return ret;
        }

        public async Task<IEnumerable<CaregivertasksEntity>> GetSchedules(ScheduleFilters scheduleFilters)
        {
            IEnumerable<CaregivertasksEntity> Schedules = new List<CaregivertasksEntity>();

            var sqlQuery = "Select CGTASK_ID, CaregiverTasks.CLIENT_ID, SERVICECODE_ID, PAYMENT_SOURCE,CAREGIVER,isEvvschedule, isEvvScheduleDirty, PLANNED_DATE, " +
                                    " CaregiverTaskAdditional2.CdsPlanYearService ,  CaregiverTaskAdditional.IsSplitForBilling, isEvvAggregatorExportRequired, CaregiverTaskAdditional2.EvvExport_ExportEffectiveFrom, CaregiverTaskAdditional2.EvvExport_DiscardPayer  " +
                           "from CaregiverTasks With(nolock), CaregiverTaskAdditional With(Nolock),  CaregiverTaskAdditional2 With(Nolock) " +
                               "Where CaregiverTasks.CGTASK_ID = CaregiverTaskAdditional.CGTaskID " +
                               "and CaregiverTaskAdditional.CGTaskID =  CaregiverTaskAdditional2.CgTaskID " +
                               "and CaregiverTasks.HHA = @HHA " +
                               "and CaregiverTaskAdditional.HHA = @HHA " +
                               "and CaregiverTaskAdditional2.HHA = @HHA ";




            if (!String.IsNullOrEmpty(scheduleFilters.startDate) && !String.IsNullOrEmpty(scheduleFilters.EndDate))
            {

                sqlQuery += "and CaregiverTasks.PLANNED_DATE between @StartDate and @EndDate ";
            }

            if (scheduleFilters.PayerId > 0)
            {

                sqlQuery += "and CaregiverTasks.PAYMENT_SOURCE = @PayerId ";
            }
            /*
            if (scheduleFilters.ClientID.Length > 0)
            {
                //var Clientids = JsonConvert.SerializeObject(scheduleFilters.ClientID);


                sqlQuery += "and CaregiverTasks.CLIENT_ID IN @Clientids ";
            }*/
            dynamic parameter = new { HHA = scheduleFilters.HHA, StartDate = scheduleFilters.startDate, EndDate = scheduleFilters.EndDate, PayerId = scheduleFilters.PayerId, Clientids = scheduleFilters.ClientID };




            //using Dapper
            using (var uow = this._connectionProvider.Connect(scheduleFilters.HHA, scheduleFilters.UserId))
            {
                try
                {
                    IGenericRepository<CaregivertasksEntity> _genericRepository = new GenericMasterRepository<CaregivertasksEntity>(uow);
                    Schedules = await _genericRepository.Select(sqlQuery, parameter);
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


            return Schedules;
        }

        public async Task<IEnumerable<CaregivertasksEntity>> GetScheduleBasicList(ScheduleFilters scheduleFilters)
        {
            IEnumerable<CaregivertasksEntity> Schedules = new List<CaregivertasksEntity>();

            var sqlQuery = "";

            if (!String.IsNullOrEmpty(scheduleFilters.startDate) && !String.IsNullOrEmpty(scheduleFilters.EndDate))
            {
                sqlQuery += "and CaregiverTasks.PLANNED_DATE between '" + scheduleFilters.startDate + "' and '" + scheduleFilters.EndDate + "' ";
            }
            else if (!String.IsNullOrEmpty(scheduleFilters.startDate))
            {
                sqlQuery += "and CaregiverTasks.PLANNED_DATE >= '" + scheduleFilters.startDate + "' ";
            }
            else if (!String.IsNullOrEmpty(scheduleFilters.EndDate))
            {
                sqlQuery += "and CaregiverTasks.PLANNED_DATE <= '" + scheduleFilters.EndDate + "' ";
            }

            if (scheduleFilters.PayerId > 0)
            {
                sqlQuery += "and CaregiverTasks.PAYMENT_SOURCE = " + scheduleFilters.PayerId.ToString();
            }
            if (scheduleFilters.ClientID > 0)
            {
                sqlQuery += "and CaregiverTasks.CLIENT_ID = " + scheduleFilters.ClientID.ToString();
            }
            else if (scheduleFilters.ClientIDList.Any())
            {
                var ClientId_CommaSepated = String.Join(",", scheduleFilters.ClientIDList);

                sqlQuery += "and CaregiverTasks.CLIENT_ID in (" + ClientId_CommaSepated + ")";
            }


            var procedurename = "_S_API_GetScheduleBasicList";

            var parameter = new DynamicParameters();
            parameter.Add("@hha", scheduleFilters.HHA);
            parameter.Add("@UserID", scheduleFilters.UserId);
            parameter.Add("@FilterData", sqlQuery);


            //using Dapper
            using (var uow = this._connectionProvider.Connect(scheduleFilters.HHA, scheduleFilters.UserId))
            {
                try
                {
                    IGenericRepository<CaregivertasksEntity> _genericRepository = new GenericMasterRepository<CaregivertasksEntity>(uow);
                    Schedules = await _genericRepository.SelectProcedure(procedurename, parameter).ConfigureAwait(false);

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

            return Schedules;
        }

        public async Task<IEnumerable<CaregivertasksEntity>> GetScheduleAdditional(ScheduleFilters scheduleFilters)
        {
            IEnumerable<CaregivertasksEntity> Schedules = new List<CaregivertasksEntity>();

            var sqlQuery = "Select CGTASK_ID, CaregiverTasks.CLIENT_ID, SERVICECODE_ID, PAYMENT_SOURCE,CAREGIVER,isEvvschedule, isEvvScheduleDirty, PLANNED_DATE, " +
                                    " CaregiverTaskAdditional2.CdsPlanYearService ,  CaregiverTaskAdditional.IsSplitForBilling  " +
                           "from CaregiverTasks With(nolock), CaregiverTaskAdditional With(Nolock),  CaregiverTaskAdditional2 With(Nolock) " +
                               "Where CaregiverTasks.CGTASK_ID = CaregiverTaskAdditional.CGTaskID " +
                               "and CaregiverTaskAdditional.CGTaskID =  CaregiverTaskAdditional2.CgTaskID " +
                               "and CaregiverTasks.HHA = @HHA " +
                               "and CaregiverTaskAdditional.HHA = @HHA " +
                               "and CaregiverTaskAdditional2.HHA = @HHA ";


            dynamic parameter = new { HHA = scheduleFilters.HHA };

            if (!String.IsNullOrEmpty(scheduleFilters.startDate) && !String.IsNullOrEmpty(scheduleFilters.EndDate))
            {
                parameter.StartDate = scheduleFilters.startDate;
                parameter.EndDate = scheduleFilters.EndDate;
                sqlQuery += "and CaregiverTasks.PLANNED_DATE between @StartDate and @EndDate ";
            }

            if (scheduleFilters.PayerId > 0)
            {
                parameter.PayerId = scheduleFilters.PayerId;
                sqlQuery += "and CaregiverTasks.PAYMENT_SOURCE = @PayerId ";
            }



            //using Dapper
            using (var uow = this._connectionProvider.Connect(scheduleFilters.HHA, scheduleFilters.UserId))
            {
                try
                {
                    IGenericRepository<CaregivertasksEntity> _genericRepository = new GenericMasterRepository<CaregivertasksEntity>(uow);
                    Schedules = await _genericRepository.Select(sqlQuery, parameter);
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


            return Schedules;
        }

        public async Task<IEnumerable<CaregivertasksEntity>> GetScheduleAdditional2(ScheduleFilters scheduleFilters)
        {
            IEnumerable<CaregivertasksEntity> Schedules = new List<CaregivertasksEntity>();

            var sqlQuery = "Select CGTASK_ID, CaregiverTasks.CLIENT_ID, SERVICECODE_ID, PAYMENT_SOURCE,CAREGIVER,isEvvschedule, isEvvScheduleDirty, PLANNED_DATE, " +
                                    " CaregiverTaskAdditional2.CdsPlanYearService ,  CaregiverTaskAdditional.IsSplitForBilling  " +
                           "from CaregiverTasks With(nolock), CaregiverTaskAdditional With(Nolock),  CaregiverTaskAdditional2 With(Nolock) " +
                               "Where CaregiverTasks.CGTASK_ID = CaregiverTaskAdditional.CGTaskID " +
                               "and CaregiverTaskAdditional.CGTaskID =  CaregiverTaskAdditional2.CgTaskID " +
                               "and CaregiverTasks.HHA = @HHA " +
                               "and CaregiverTaskAdditional.HHA = @HHA " +
                               "and CaregiverTaskAdditional2.HHA = @HHA ";


            dynamic parameter = new { HHA = scheduleFilters.HHA };

            if (!String.IsNullOrEmpty(scheduleFilters.startDate) && !String.IsNullOrEmpty(scheduleFilters.EndDate))
            {
                parameter.StartDate = scheduleFilters.startDate;
                parameter.EndDate = scheduleFilters.EndDate;
                sqlQuery += "and CaregiverTasks.PLANNED_DATE between @StartDate and @EndDate ";
            }

            if (scheduleFilters.PayerId > 0)
            {
                parameter.PayerId = scheduleFilters.PayerId;
                sqlQuery += "and CaregiverTasks.PAYMENT_SOURCE = @PayerId ";
            }



            //using Dapper
            using (var uow = this._connectionProvider.Connect(scheduleFilters.HHA, scheduleFilters.UserId))
            {
                try
                {
                    IGenericRepository<CaregivertasksEntity> _genericRepository = new GenericMasterRepository<CaregivertasksEntity>(uow);
                    Schedules = await _genericRepository.Select(sqlQuery, parameter);
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


            return Schedules;
        }

        public async Task<bool> UpdateScheduleBasic(int HHA, int UserId, int CGTASK_ID, string caregivertasksEntity)
        {
            bool ret = false;
            var sqlQuery = "Update CaregiverTasks set  " + caregivertasksEntity +
                               " Where CaregiverTasks.CGTASK_ID = @CGTASK_ID " +
                               " and CaregiverTasks.HHA = @HHA ";


            dynamic parameter = new { HHA = HHA, CGTASK_ID = CGTASK_ID };

            //using Dapper
            using (var uow = this._connectionProvider.Connect(HHA, UserId))
            {
                try
                {
                    IGenericRepository<CaregivertasksEntity> _genericRepository = new GenericMasterRepository<CaregivertasksEntity>(uow);
                    var result = await _genericRepository.Execute(sqlQuery, parameter).ConfigureAwait(false);
                    if (result == 1)
                        ret = true;
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
            return ret;

        }

        public async Task<bool> UpdateScheduleAdditional(int HHA, int UserId, int CGTASK_ID, string caregivertasksAdditionalEntity)
        {
            bool ret = false;
            var sqlQuery = "Update CaregiverTaskAdditional set  " + caregivertasksAdditionalEntity +
                               " Where CaregiverTasks.CGTASK_ID = @CGTASK_ID " +
                               " and CaregiverTasks.HHA = @HHA ";


            dynamic parameter = new { HHA = HHA, CGTASK_ID = CGTASK_ID };

            //using Dapper
            using (var uow = this._connectionProvider.Connect(HHA, UserId))
            {
                try
                {
                    IGenericRepository<CaregivertasksEntity> _genericRepository = new GenericMasterRepository<CaregivertasksEntity>(uow);
                    var result = await _genericRepository.Execute(sqlQuery, parameter).ConfigureAwait(false);
                    if (result == 1)
                        ret = true;
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
            return ret;
        }

        public async Task<bool> UpdateScheduleAdditional2(int HHA, int UserId, int CGTASK_ID, string caregivertasksAdditional2Entity)
        {
            bool ret = false;
            var sqlQuery = "Update CaregiverTaskAdditional2 set  " + caregivertasksAdditional2Entity +
                               " Where CaregiverTaskAdditional2.CGTASKID = @CGTASK_ID " +
                               " and CaregiverTasks.HHA = @HHA ";


            dynamic parameter = new { HHA = HHA, CGTASK_ID = CGTASK_ID };

            //using Dapper
            using (var uow = this._connectionProvider.Connect(HHA, UserId))
            {
                try
                {
                    IGenericRepository<CaregivertasksEntity> _genericRepository = new GenericMasterRepository<CaregivertasksEntity>(uow);
                    var result = await _genericRepository.Execute(sqlQuery, parameter).ConfigureAwait(false);
                    if (result == 1)
                        ret = true;
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
            return ret;
        }

        public async Task<int> UpdateRecalculateEVVSchedules(int HHA, int UserId, string CGTaskIDsjson, int context)
        {
            int result = 0;
            var procedurename = "_S_RecalcualteEvvScheduleFlag"; 
             
            var parameter = new DynamicParameters();
            parameter.Add("@HHA", HHA);
            parameter.Add("@user", UserId);
            parameter.Add("@Cgtaskids", CGTaskIDsjson);
            parameter.Add("@Context", context);
          
            //using Dapper
            using (var uow = this._connectionProvider.Connect(HHA, UserId))
            {
                try
                {
                    IGenericRepository<int> _genericRepository = new GenericMasterRepository<int>(uow);
                    result = await _genericRepository.ExecuteProcedure(procedurename, parameter).ConfigureAwait(false);
                     
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

        public async Task<IEnumerable<string>> GetSchedulesByFilterJson(int HHA, int UserID, string FilterJson)
        {
            IEnumerable<string> result = new List<string>();
            var procedurename = "_R_GetSchedulesByFilterJson";

            var parameter = new DynamicParameters();
            parameter.Add("@hha", HHA);
            parameter.Add("@User", UserID);
            parameter.Add("@filterJSON", FilterJson);
           

            //using Dapper
            using (var uow = this._connectionProvider.Connect(HHA, UserID))
            {
                try
                {
                    IGenericRepository<string> _genericRepository = new GenericMasterRepository<string>(uow);
                    result = await _genericRepository.SelectProcedure(procedurename, parameter).ConfigureAwait(false);

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
    }
}
