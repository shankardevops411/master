using MediatR;
using Newtonsoft.Json;
using Scheduleservice.Services.Common.Request;
using Scheduleservice.Services.Common.Response;
using Scheduleservice.Services.EVVHandlers.Commands;
using Scheduleservice.Services.EVVHandlers.EVVHandlersDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduleservice.Services.EVVHandlers.CommandHandlers
{
    public class UpdateIsEVVScheduleHandler: IHandlerWrapper<UpdateIsEVVscheduleCommand, bool>
    {
        public IScheduleEVVService _IScheduleEVV;

        public UpdateIsEVVScheduleHandler(IScheduleEVVService scheduleEVVService)
        {
            _IScheduleEVV = scheduleEVVService;
        }
        public async Task<Response<bool>> Handle(UpdateIsEVVscheduleCommand request, CancellationToken cancellationToken)
        {
            
            bool ret = false;
            try
            {
                 IEnumerable<UpdateIsEVVScheduleFlagDto> ScheduleDetails = null;
                if (request.ScheduleDetails_JSON != "" && request.ScheduleDetails_JSON != null)
                {
                    ScheduleDetails = JsonConvert.DeserializeObject<List<UpdateIsEVVScheduleFlagDto>>(request.ScheduleDetails_JSON);

                    if (ScheduleDetails.Count() > 0)
                        ret = await _IScheduleEVV.UpdateIsEVVScheduleFlag(request.HHA, request.UserId, ScheduleDetails);
                }

                if (ret)
                    return Response.Ok<bool>(ret, "Successfully Update EVV Schedule");
                else
                    return Response.Fail<bool>("Failed to Update EVV Schedule");
            }
            catch (Exception ex)
            {
                throw ex;
            }

           
        }

       
    }
}
