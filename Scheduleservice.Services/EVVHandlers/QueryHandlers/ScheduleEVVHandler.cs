using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Scheduleservice.Services.Common.Request;
using Scheduleservice.Services.Common.Response;
using Scheduleservice.Services.EVVHandlers.EVVHandlersDto;

namespace Scheduleservice.Services.EVVHandlers
{
    public class ScheduleEVVHandler : IHandlerWrapper<ScheduleEVVQuery, IEnumerable<ScheduleEVVInfoDto>>
    {

        public IScheduleEVVService _IScheduleEVV;

        public ScheduleEVVHandler(IScheduleEVVService scheduleEVVService)
        {
            _IScheduleEVV = scheduleEVVService;
        }   

        public async Task<Response<IEnumerable<ScheduleEVVInfoDto>>> Handle(ScheduleEVVQuery request, CancellationToken cancellationToken)
        {

            var scheduleEVVInfoDtoList =await _IScheduleEVV.GetWronglyMarkedEVVVisits(request.HHA, request.UserId, request.StartDate, request.EndDate);
            return Response.Ok<IEnumerable<ScheduleEVVInfoDto>>(scheduleEVVInfoDtoList,"");
        }

        
    }
}
