using Scheduleservice.Core.Entities;
using Scheduleservice.Services.Schedules.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Services.Infrastructure.RepositoryContracts
{
    public interface ICaregivertasksRepository
    {

       // Task<IEnumerable<CaregivertasksEntity>> GetSchedules(ScheduleFilters scheduleFilters);
        Task<IEnumerable<CaregivertasksEntity>> GetScheduleBasicList(ScheduleFilters scheduleFilters);
       // Task<IEnumerable<CaregivertasksEntity>> GetScheduleAdditional(ScheduleFilters scheduleFilters);
       // Task<IEnumerable<CaregivertasksEntity>> GetScheduleAdditional2(ScheduleFilters scheduleFilters);



        //Task<bool> UpdateScheduleBasic(int HHA, int UserId, int CGTASK_ID, string caregivertasksEntity);
        //Task<bool> UpdateScheduleAdditional(int HHA, int UserId, int CGTASK_ID, string caregivertasksAdditionalEntity);
        Task<bool> UpdateScheduleAdditional2(int HHA, int UserId, int CGTASK_ID, string caregivertasksAdditional2Entity);

        Task<int> UpdateRecalculateEVVSchedules(int HHA, int UserId, string CGTaskIDsjson, int context);



        //Task<IEnumerable<CaregivertasksEntity>> GetHHAEVVSchedules(int HHA, int UserID, string StartDate, string EndDate);
        //Task<IEnumerable<CaregivertasksEntity>> GetHHANonEVVSchedules(int HHA, int UserID, string StartDate, string EndDate);
        //Task<bool> UpdatescheduleasRealEVV(int HHA, int UserID, IEnumerable<int> CgtaskIds);
        //Task<bool> UpdatescheduleasNonEVV(int HHA, int UserID, IEnumerable<int> CgtaskIds);

        Task<IEnumerable<string>> GetSchedulesByFilterJson(int HHA, int UserID, string FilterJson);
    }
}
