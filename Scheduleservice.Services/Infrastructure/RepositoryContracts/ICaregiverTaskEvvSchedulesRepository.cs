using Scheduleservice.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Services.Infrastructure.RepositoryContracts
{
    public interface ICaregiverTaskEvvSchedulesRepository
    {
        Task<IEnumerable<Guid>> InsertCaregiverTaskEvvSchedules(int HHA, int UserId,  string CaregiverTaskEvvSchedules);
        Task<int> DeleteCaregiverTaskEvvSchedules(int HHA, int UserId, string CaregiverTaskEvvSchedulesBachID);
        Task<IEnumerable<CaregiverTaskEvvSchedulesEntity>> GetCaregiverTaskEvvSchedules(int HHA, int UserId, string CaregiverTaskEvvSchedulesBachID);
    }
}
