
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Scheduleservice.Services.Models;

namespace Scheduleservice.Services.Interfaces
{
    public interface IPaymentSources
    {
        Task<IEnumerable<Paymentsources>> GetEVVEnabledPayers(int HHA, int userID);
    }
}
