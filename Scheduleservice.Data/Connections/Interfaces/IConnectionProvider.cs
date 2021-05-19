using Scheduleservice.Data.UnitofWorks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduleservice.Data.Connections
{
    public interface IConnectionProvider
    {
        IUnitOfWork ConnectMaster();

        IUnitOfWork Connect(int HHA, int UserID);
    }
}
