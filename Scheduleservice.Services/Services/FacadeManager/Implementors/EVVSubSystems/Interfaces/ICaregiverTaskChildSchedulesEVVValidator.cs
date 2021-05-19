using Scheduleservice.Services.EVVHandlers.EVVHandlersDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Services.Services.FacadeManager.Implementors.EVVSubSystems.Interfaces
{
    public interface ICaregiverTaskChildSchedulesEVVValidator
    {
        Task<List<ScheduleEVVInfoDto>> ValidateSplitschedulesEVVFlag(int HHA, int UserID, List<ScheduleEVVInfoDto> HHASchedulesList);
    }
}
