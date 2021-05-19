using Scheduleservice.Services.EVVHandlers.EVVHandlersDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Services.Services.FacadeManager.Implementors.Interfaces
{
    public interface IGetWrongEVVschedulesImplementor
    {
        Task<List<ScheduleEVVInfoDto>> GetWrongMarkedEVVschedules(int HHA, int UserID,List<ScheduleEVVInfoDto> HHASchedulesList);
    }
}
