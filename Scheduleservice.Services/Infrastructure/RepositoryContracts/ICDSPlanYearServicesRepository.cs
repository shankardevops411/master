using Scheduleservice.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Services.Infrastructure.RepositoryContracts
{
    public interface ICDSPlanYearServicesRepository
    {
        Task<IEnumerable<CDSPlanYearServicesEntity>> GetEVVORNonEVVCDSPlanYearServices(int HHA, int UserID, IEnumerable<int> CDSPlanYearServices, bool IsEnableEVV);
         

    }
}
