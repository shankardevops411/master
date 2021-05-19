using Scheduleservice.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Services.Infrastructure.RepositoryContracts
{
    public interface IServiceCodesRepository
    {
        Task<IEnumerable<ServiceCodesEntity>> GetEVVORNonEVVServices(int HHA, int UserID, IEnumerable<int> Services, bool isEvvEnabled);
    }
}
