using Scheduleservice.Core.Entities;
using Scheduleservice.Services.Clients.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Services.Infrastructure.RepositoryContracts
{
   public interface IEvvExportDiscardedClientsRepository
    {
        Task<IEnumerable<EvvExportDiscardedClientsEntity>> GetEvvExportDiscardedClients(EvvExportDiscardedClientsFilters evvExportDiscardedClientsFilters);
      
    }
}
