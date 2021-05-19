

using Scheduleservice.Services.Interfaces;
using Scheduleservice.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Services.Implementations
{
    public class Paymentsources_Details : IPaymentSources
    {

        IPaymentSources _IPaymentSources;

        public Paymentsources_Details(IPaymentSources paymentSources)
        {
            _IPaymentSources = paymentSources;
        }

        public async  Task<IEnumerable<Paymentsources>> GetEVVEnabledPayers(int HHA, int userID)
        {
            return await _IPaymentSources.GetEVVEnabledPayers(HHA, userID);
        }
    }
}
