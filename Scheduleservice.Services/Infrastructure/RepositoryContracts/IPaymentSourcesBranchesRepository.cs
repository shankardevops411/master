using Scheduleservice.Core.Entities; 
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Services.Infrastructure.RepositoryContracts
{
    public interface IPaymentSourcesBranchesRepository
    {
        Task<IEnumerable<PaymentSourcesBranchesEntity>> GetHHAPayerBranches(int HHA, int UserID, IEnumerable<int> Payers);
    }
}
