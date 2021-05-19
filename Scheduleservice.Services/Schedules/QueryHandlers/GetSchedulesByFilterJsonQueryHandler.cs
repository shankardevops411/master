using Scheduleservice.Services.Common.Request;
using Scheduleservice.Services.Common.Response;
using Scheduleservice.Services.Infrastructure.RepositoryContracts;
using Scheduleservice.Services.Schedules.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduleservice.Services.Schedules.QueryHandlers
{
    public class GetSchedulesByFilterJsonQueryHandler : IHandlerWrapper<GetSchedulesByFilterJsonQuery, string>
    {

        private readonly ICaregivertasksRepository  _caregivertasksRepository;

        public GetSchedulesByFilterJsonQueryHandler(ICaregivertasksRepository caregivertasksRepository)
        {
            _caregivertasksRepository = caregivertasksRepository;
        }

 

        public async Task<Response<string>> Handle(GetSchedulesByFilterJsonQuery request, CancellationToken cancellationToken)
        {
            string ScheduleJsonResult = "";
            try
            {
                var ScheduleJson = await _caregivertasksRepository.GetSchedulesByFilterJson(request.HHA, request.UserId, request.FilterJSON);
                  ScheduleJsonResult = ScheduleJson.ToList().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            } 
            return Response.Ok<string>(ScheduleJsonResult, "Success");
        }
    }
}
