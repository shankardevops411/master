using AutoMapper;
using Scheduleservice.Core.Entities;
using Scheduleservice.Services.EVVHandlers.EVVHandlersDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduleservice.Services.EVVHandlers.EVVMappingProfiles
{
    public class EVVMappingProfile : Profile
    {
        public EVVMappingProfile()
        {
            CreateMap<CaregivertasksEntity, ScheduleEVVInfoDto>();
        }
    }
}
