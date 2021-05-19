using Scheduleservice.Services.EVVHandlers.EVVHandlersDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Services.Services.FacadeManager.Implementors.NonEVVSubsystems.Interfaces
{
    public interface IClientORLOBNonEVVValidator
    {
        Task<List<ScheduleEVVInfoDto>> ValidateClientORLobNonEVVFlag(int HHA, int UserID, List<ScheduleEVVInfoDto> HHASchedulesList);
    }
}
