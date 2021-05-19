using AutoMapper;
using Scheduleservice.Services.Common.Request;
using Scheduleservice.Services.Common.Response;
using Scheduleservice.Services.Infrastructure.RepositoryContracts;
using Scheduleservice.Services.Schedules.DTO;
using Scheduleservice.Services.Schedules.Models;
using Scheduleservice.Services.Schedules.Query;
using Scheduleservice.Services.Schedules.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduleservice.Services.Schedules.QueryHandlers
{
    public class ScheduleEVVBatchQueryHandler : IHandlerWrapper<ScheduleEVVBatchQuery, IEnumerable<ScheduleEVVBatchListDto>>
    {
         
        private readonly IEVVSchedule _eVVSchedule;
        private readonly ICaregivertasksRepository _caregivertasksRepository;
        private readonly IMapper _mapper;
        public ScheduleEVVBatchQueryHandler( IEVVSchedule eVVSchedule, IMapper mapper, ICaregivertasksRepository caregivertasksRepository)
        {
            
            this._mapper = mapper;
            this._eVVSchedule = eVVSchedule;
            this._caregivertasksRepository = caregivertasksRepository;

        }
        public async Task<Response<IEnumerable<ScheduleEVVBatchListDto>>> Handle(ScheduleEVVBatchQuery request, CancellationToken cancellationToken)
        {             
            var scheduleFilterDetails = _mapper.Map<ScheduleFilters>(request);
            var schedulelists = await _caregivertasksRepository.GetScheduleBasicList(scheduleFilterDetails); 

            var CGTaskIDs = schedulelists.Select(x => x.CGTASK_ID);
            var scheduleCGTaskIDDto = _mapper.Map<IEnumerable<ScheduleCGTaskIDDto>>(CGTaskIDs);
            var scheduleEVVBatchListDtos = await _eVVSchedule.InsertCaregiverTaskEvvSchedules(request.HHA, request.UserId, scheduleCGTaskIDDto);
            return Response.Ok<IEnumerable<ScheduleEVVBatchListDto>>(scheduleEVVBatchListDtos, "Success");
        }
    }
}
