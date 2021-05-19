
using AutoMapper;
using MediatR; 
using Microsoft.Extensions.DependencyInjection;
using Scheduleservice.Services.Common.Behaviors; 
using Scheduleservice.Services.Schedules.Service;
using Scheduleservice.Services.Schedules.Service.Contracts;
using System.Reflection;
/*
using Scheduleservice.Services.Services.FacadeManager;
using Scheduleservice.Services.Services.FacadeManager.Implementors;
using Scheduleservice.Services.Services.FacadeManager.Implementors.EVVSubSystems;
using Scheduleservice.Services.Services.FacadeManager.Implementors.EVVSubSystems.Interfaces;
using Scheduleservice.Services.Services.FacadeManager.Implementors.Interfaces;
using Scheduleservice.Services.Services.FacadeManager.Implementors.NonEVVSubsystems;
using Scheduleservice.Services.Services.FacadeManager.Implementors.NonEVVSubsystems.Interfaces;
using Scheduleservice.Services.Services.FacadeManager.Interfaces;
using Scheduleservice.Services.FacadeLayer;
 */

namespace Scheduleservice.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServicesServiceCollection(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(HttpContextMiddleware<,>));
            services.AddMediatR(Assembly.GetExecutingAssembly()); 
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
         
            services.AddScoped<IEVVSchedule, EVVSchedule>();
            services.AddScoped<ISchedule, Schedule>();

            /*
            services.AddScoped<IScheduleEVVService, ScheduleEVVService>();
            services.AddScoped<IEVVManager, EVVManager>();
            services.AddScoped<IClientORLOBEVVValidator, ClientORLOBEVVValidator>();
            services.AddScoped<ICaregiverDiscEVVValidator, CaregiverDiscEVVValidator>();
            services.AddScoped<ICdsAuthServiceEVVValidator, CdsAuthServiceEVVValidator>();
            services.AddScoped<IHHABrancheffectivePeriodEVVValidator, HHABrancheffectivePeriodEVVValidator>();
            services.AddScoped<IPayerEVVValidator, PayerEVVValidator>();
            services.AddScoped<IServiceEVVValidator, ServiceEVVValidator>();
            services.AddScoped<ICaregiverTaskChildSchedulesEVVValidator, CaregiverTaskChildSchedulesEVVValidator>();
            services.AddScoped<IClientORLOBNonEVVValidator, ClientORLOBNonEVVValidator>();
            services.AddScoped<ICaregiverDiscNonEVVValidator, CaregiverDiscNonEVVValidator>();
            services.AddScoped<ICDSAuthServiceNonEVVValidator, CDSAuthServiceNonEVVValidator>();
            services.AddScoped<IPayerNonEVVValidator, PayerNonEVVValidator>();
            services.AddScoped<IServiceNonEVVValidator, ServiceNonEVVValidator>();
            services.AddScoped<IGetWrongNonEVVschedulesImplementor, GetWrongNonEVVschedulesImplementor>();
            services.AddScoped<IGetWrongEVVschedulesImplementor, GetWrongEVVschedulesImplementor>();
            */
            return services;
        }
    }
}
