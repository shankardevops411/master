using Scheduleservice.Core.Entities;
using Scheduleservice.Data.Connections; 
using Scheduleservice.Services.Infrastructure.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Data.Repository
{
    public class CDSPlanYearServicesRepository : ICDSPlanYearServicesRepository
    {

         
        private readonly IConnectionProvider _connectionProvider;

        public CDSPlanYearServicesRepository( IConnectionProvider connectionProvider)
        {
            
            this._connectionProvider = connectionProvider;
        }

        public async Task<IEnumerable<CDSPlanYearServicesEntity>> GetEVVORNonEVVCDSPlanYearServices(int HHA, int UserID, IEnumerable<int> CDSPlanYearServices, bool IsEnableEVV)
        {
            IEnumerable<CDSPlanYearServicesEntity> EVVCDSPlanYearServices = new List<CDSPlanYearServicesEntity>();

            try
            {
                var sqlQuery = "";

                if (CDSPlanYearServices.Count() > 2100)
                {
                    sqlQuery = "Select CDSPlanYearServiceID from CDSPlanYearServices With(Nolock) Where HHA = @HHA " +
                                    " and isnull(IsEnableEVV, 0) = @IsEnableEVV ";
                }
                else
                {
                    sqlQuery = "Select CDSPlanYearServiceID from CDSPlanYearServices With(Nolock) Where HHA = @HHA and isnull(IsEnableEVV, 0) = @IsEnableEVV " +
                                        " and CDSPlanYearServiceID IN @CDSPlanYearServices ";
                }

                object parameter = new { HHA = HHA, CDSPlanYearServices = CDSPlanYearServices, IsEnableEVV = IsEnableEVV };
                

                //using Dapper
                using (var uow = this._connectionProvider.Connect(HHA,UserID))
                {
                    IGenericRepository<CDSPlanYearServicesEntity> _genericRepository = new GenericMasterRepository<CDSPlanYearServicesEntity>(uow);
                    EVVCDSPlanYearServices = await _genericRepository.Select(sqlQuery, parameter).ConfigureAwait(true);
                }
            }
            catch (Exception ex)
            { 
                throw;
            }

            return EVVCDSPlanYearServices;
        }

         

    }
}
