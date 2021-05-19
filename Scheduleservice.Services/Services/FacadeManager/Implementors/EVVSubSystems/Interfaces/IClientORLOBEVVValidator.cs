
using Scheduleservice.Services.EVVHandlers.EVVHandlersDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Services.Services.FacadeManager.Implementors.EVVSubSystems.Interfaces
{
    public interface IClientORLOBEVVValidator
    {

        Task<List<ScheduleEVVInfoDto>> ValidateClientORLobEVVFlag(int HHA, int UserID, List<ScheduleEVVInfoDto> HHASchedulesList);

    }
}
