using AutoMapper;
using Scheduleservice.Core.Entities;
using Scheduleservice.Services.Schedules.Command;
using Scheduleservice.Services.Schedules.DTO;
using Scheduleservice.Services.Schedules.Models;
using Scheduleservice.Services.Schedules.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduleservice.Services.Schedules.MappingProfiles
{
    public class ScheduleMappingProfiles:Profile
    {
        public ScheduleMappingProfiles()
        {
            CreateMap<ScheduleEVVBatchQuery, ScheduleFilters>();            
            CreateMap<ScheduleUpdateFilters, CaregivertasksEntity>();
            CreateMap<CaregivertasksEntity, ScheduleListDto>();
            CreateMap<int, ScheduleCGTaskIDDto>().ForMember(d => d.CGTaskID, opt => opt.MapFrom(src => src));
            CreateMap <Guid,ScheduleEVVBatchListDto>().ForMember(d => d.BatchID, opt => opt.MapFrom(src => src));
        }
    }
}
