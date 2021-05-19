using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection; 
using Scheduleservice.Data.Connections; 
using Scheduleservice.Data.Models;
using Scheduleservice.Data.Repository;
using Scheduleservice.Data.UnitofWorks;
using Scheduleservice.Services.Infrastructure.RepositoryContracts; 

namespace Scheduleservice.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureConnectionProvider<T>(this IServiceCollection services, IConfiguration Configuration)
             where T : class, IConnectionProvider
        {
            var ConnectionString = Configuration["ConnectionStrings:MsSqlPrivateConnection"];
            var ConnectionMsSqlMasterString = Configuration["ConnectionStrings:MsSqlMasterConnection"];

            services.Configure<ConnectionProviderOptions>(x =>
            {
                x.connctionstring = ConnectionString;
                x.connctionMasterstring = ConnectionMsSqlMasterString;
            });

            services.AddScoped<IConnectionProvider, T>();
            return services;
        }
        public static IServiceCollection AddDataServiceCollection(this IServiceCollection services)
        {           
           
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericMasterRepository<>));           
          //master Database 
            services.AddScoped<IAgencyMasterListRepository, AgencyMasterListRepository>();
            services.AddScoped<IDatabaselistRepository, DatabaselistRepository>();    
            
            
            // Agency level
            services.AddScoped<I_C_EVVConfigurationsRepository, _C_EVVConfigurationsRepository>();
            services.AddScoped<I_C_LineofbusinessRepository, _C_LineofbusinnessRepository>();

            // Schedules
            services.AddScoped<ICaregivertasksRepository, CaregivertaskRepository>();
            services.AddScoped<ICaregiverTaskChildSchedulesRepository, CaregiverTaskChildSchedulesRepository>();
            services.AddScoped<ICaregiverTaskEvvSchedulesRepository, CaregiverTaskEvvSchedulesRepository>();

            // Clients
            services.AddScoped<IClientsRepository, ClientsRepository>();           
            services.AddScoped<IEvvExportDiscardedClientsRepository, EvvExportDiscardedClientsRepository>();
            // Services
            services.AddScoped<IServiceCodesRepository, ServiceCodesRepository>();

            //payers
            services.AddScoped<IPaymentsourcesRepository, PaymentsourcesRepository>();
            services.AddScoped<IPaymentsourcesAdditionalRepository, PaymentSourcesAdditionalRepository>();
            services.AddScoped<IPaymentSourcesBranchesRepository, PaymentSourcesBranchesRepository>();

            //Clinicians
            services.AddScoped<ICaregiversRepository, CaregiversRepository>();

            //CDS tables
            services.AddScoped<ICDSPlanYearServicesRepository, CDSPlanYearServicesRepository>();
         
            

            return services;
        }
    }
}
