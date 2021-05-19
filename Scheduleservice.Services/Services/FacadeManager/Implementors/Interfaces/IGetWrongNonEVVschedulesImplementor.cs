using Scheduleservice.Services.EVVHandlers.EVVHandlersDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Services.Services.FacadeManager.Implementors.Interfaces
{
    public interface IGetWrongNonEVVschedulesImplementor
    {
        Task<List<ScheduleEVVInfoDto>> GetWronglyMarkedNonEVVschedules(int HHA, int UserID, List<ScheduleEVVInfoDto> HHASchedulesList);
    }
}
