using Scheduleservice.Core.Entities; 
using Scheduleservice.Services.PaymentSources.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Services.Infrastructure.RepositoryContracts
{
    public interface IPaymentsourcesRepository
    {
        Task<IEnumerable<PaymentsourcesEntity>> GetPaymentsources(PayerFilters payerFilters);      
        Task<IEnumerable<PaymentsourcesEntity>> GetPaymentsourceBasic(PayerFilters payerFilters);
        Task<IEnumerable<PaymentsourcesEntity>> GetPaymentsourceAdditional(PayerFilters payerFilters);
        Task<IEnumerable<PaymentsourcesEntity>> GetPaymentsourceAdditional2(PayerFilters payerFilters);

    }
}
