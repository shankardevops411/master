using AutoMapper;
using Newtonsoft.Json;
using Scheduleservice.Services.Infrastructure.RepositoryContracts;
using Scheduleservice.Services.Schedules.DTO;
using Scheduleservice.Services.Schedules.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Services.Schedules.Service
{
    public class EVVSchedule : IEVVSchedule
    {
        IMapper _mapper;
        ICaregiverTaskEvvSchedulesRepository _caregiverTaskEvvSchedulesRepository;
        ICaregivertasksRepository _caregivertasksRepository;
        public EVVSchedule(IMapper mapper, ICaregiverTaskEvvSchedulesRepository caregiverTaskEvvSchedulesRepository, ICaregivertasksRepository caregivertasksRepository)
        {
            this._mapper = mapper;
            this._caregiverTaskEvvSchedulesRepository = caregiverTaskEvvSchedulesRepository;
            this._caregivertasksRepository = caregivertasksRepository;
        }
        public async Task<bool> RecalculateCaregiverTaskEvvSchedules(int HHA, int UserID, string ScheduleEVVBatchID)
        {   //Get  CaregiverTask Evv Schedules batches
            var CaregiverTaskEvvSchedules = await this._caregiverTaskEvvSchedulesRepository.GetCaregiverTaskEvvSchedules(HHA, UserID, ScheduleEVVBatchID);
            
            //get all  CGTASKID from   CaregiverTask Evv Schedules batches
            var RecalculateEVVSchedules = CaregiverTaskEvvSchedules.Where(y => y.SplitScheduleID == 0).Select(x => x.cgtaskid);
            var CGTaskIDs = _mapper.Map<IEnumerable<ScheduleCGTaskIDDto>>(RecalculateEVVSchedules);

            //convert to Json string
            var CGTaskIDsjson = JsonConvert.SerializeObject(CGTaskIDs);

            //Recalculate evv flag and update
            var EvvSchedules = await this._caregivertasksRepository.UpdateRecalculateEVVSchedules(HHA, UserID, CGTaskIDsjson, 1);

            //Delete  CaregiverTask Evv Schedules batches
            var DeleteCaregiverTaskEvvSchedules =  await this._caregiverTaskEvvSchedulesRepository.DeleteCaregiverTaskEvvSchedules(HHA, UserID, ScheduleEVVBatchID);
            return true;

        }

        public async Task<IEnumerable<ScheduleEVVBatchListDto>> InsertCaregiverTaskEvvSchedules(int HHA, int UserID, IEnumerable<ScheduleCGTaskIDDto> CGTaskIDs)
        {
            var CGTaskIDsjson = JsonConvert.SerializeObject(CGTaskIDs);
            var result = await this._caregiverTaskEvvSchedulesRepository.InsertCaregiverTaskEvvSchedules(HHA, UserID, CGTaskIDsjson);
            var  ScheduleEVVBatchList= _mapper.Map<IEnumerable<ScheduleEVVBatchListDto>>(result);
            return ScheduleEVVBatchList;
        }

        
    }
}
