using Scheduleservice.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Services.Infrastructure.RepositoryContracts
{
    public interface I_C_EVVConfigurationsRepository
    {
        Task<IEnumerable<_C_EVVConfigurationsEntity>> GetEVVBranchVendorDetails(int HHA, int UserID, IEnumerable<int> Branches);

        Task<IEnumerable<_C_EVVConfigurationsEntity>> GetEVVBranchDetails(int HHA, int UserID);
    }
}
