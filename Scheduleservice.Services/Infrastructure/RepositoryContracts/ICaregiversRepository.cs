using Scheduleservice.Core.Entities; 
using System.Collections.Generic; 
using System.Threading.Tasks;

namespace Scheduleservice.Services.Infrastructure.RepositoryContracts
{
    public interface ICaregiversRepository
    {
        Task<IEnumerable<CaregiversEntity>> GetHHAEVVORNonEVVCaregivers(int HHA, int UserID, IEnumerable<int> Caregivers, bool IsEVVEnabled);
         
    }
}
