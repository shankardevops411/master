using AutoMapper;
using Newtonsoft.Json;
using Scheduleservice.Services.Common.Request;
using Scheduleservice.Services.Common.Response;
using Scheduleservice.Services.Infrastructure.RepositoryContracts;
using Scheduleservice.Services.Schedules.Command;
using Scheduleservice.Services.Schedules.DTO;
using Scheduleservice.Services.Schedules.Models;
using Scheduleservice.Services.Schedules.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks; 
namespace Scheduleservice.Services.Schedules.CommandHandlers
{
    public class ScheduleEVVBatchCommandHandler : IHandlerWrapper<ScheduleEVVBatchCommand, bool>
    {
        IMapper _mapper;
        private readonly ICaregivertasksRepository _caregivertasksRepository;
        private readonly ICaregiverTaskEvvSchedulesRepository _caregiverTaskEvvSchedulesRepository;
       
        public ScheduleEVVBatchCommandHandler(IMapper mapper, ICaregivertasksRepository caregivertasksRepository, ICaregiverTaskEvvSchedulesRepository caregiverTaskEvvSchedulesRepository)
        {
            
            this._mapper = mapper;
            this._caregivertasksRepository = caregivertasksRepository;
            this._caregiverTaskEvvSchedulesRepository = caregiverTaskEvvSchedulesRepository;
        }
        public async Task<Response<bool>> Handle(ScheduleEVVBatchCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //Get  CaregiverTask Evv Schedules batches
                var CaregiverTaskEvvSchedules = await this._caregiverTaskEvvSchedulesRepository.GetCaregiverTaskEvvSchedules(request.HHA, request.UserId, request.ScheduleEVVBatchID);

                //get all  CGTASKID from   CaregiverTask Evv Schedules batches
                var RecalculateEVVSchedules = CaregiverTaskEvvSchedules.Where(y => y.SplitScheduleID == 0).Select(x => x.cgtaskid);
                var CGTaskIDs = _mapper.Map<IEnumerable<ScheduleCGTaskIDDto>>(RecalculateEVVSchedules);

                //convert to Json string
                var CGTaskIDsjson = JsonConvert.SerializeObject(CGTaskIDs);


                var result = await this._caregivertasksRepository.UpdateRecalculateEVVSchedules(request.HHA, request.UserId, CGTaskIDsjson, 1);

                var DeleteCaregiverTaskEvvSchedules = await this._caregiverTaskEvvSchedulesRepository.DeleteCaregiverTaskEvvSchedules(request.HHA, request.UserId, request.ScheduleEVVBatchID);

                return Response.Ok(true, "Successfully updated");
                 
            }
            catch (Exception ex)
            {
                return Response.Fail<bool>("Fail to  update, Please contact kantime support!");
            }
        }
    }
}
