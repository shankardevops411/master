using AutoMapper;
using Scheduleservice.Core.Entities;
using Scheduleservice.Services.Infrastructure.RepositoryContracts;
using Scheduleservice.Services.Schedules.DTO;
using Scheduleservice.Services.Schedules.Models;
using Scheduleservice.Services.Schedules.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Services.Schedules.Service
{
    public class Schedule : ISchedule
    {
        private readonly ICaregivertasksRepository _caregivertasksRepository;
        private readonly IMapper _mapper;
        public Schedule(ICaregivertasksRepository caregivertasksRepository, IMapper mapper)
        {
            _caregivertasksRepository = caregivertasksRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ScheduleListDto>> GetSchedulesList(ScheduleFilters scheduleFilters)
        {
            var schedules = await _caregivertasksRepository.GetScheduleBasicList(scheduleFilters);
            var scheduleFilterDetails = _mapper.Map<IEnumerable<ScheduleListDto>>(schedules);
            return scheduleFilterDetails;
        }

        public async Task<bool> UpdateScheduleProperties(ScheduleUpdateFilters  scheduleUpdateFilters)
        {
            string caregivertasksEntity = "";
            if (scheduleUpdateFilters.isEVVAggrigatorExported != null)
            {
                if (scheduleUpdateFilters.isEVVAggrigatorExported ?? false)
                    caregivertasksEntity = " isEVVAggrigatorExported=1";
                else
                    caregivertasksEntity = " isEVVAggrigatorExported=0";
            }


            var result = await _caregivertasksRepository.UpdateScheduleAdditional2(scheduleUpdateFilters.HHA, scheduleUpdateFilters.UserId, scheduleUpdateFilters.CGTASK_ID, caregivertasksEntity);
            return result;
        }
    }
}
