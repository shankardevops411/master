using Scheduleservice.Services.EVVHandlers.EVVHandlersDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Services.Services.FacadeManager.Implementors.NonEVVSubsystems.Interfaces
{
    public interface ICaregiverDiscNonEVVValidator
    {
        Task<List<ScheduleEVVInfoDto>> ValidateClincianDiscNonEVVFlag(int HHA, int UserID, List<ScheduleEVVInfoDto> HHASchedulesList);
    }
}
