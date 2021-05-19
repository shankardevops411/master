using Scheduleservice.Core.Entities;
using Scheduleservice.Services.Clients.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Services.Infrastructure.RepositoryContracts
{
    public interface IClientsRepository
    {
        Task<IEnumerable<ClientsEntity>> GetClients(int HHA, int UserID, IEnumerable<int> clients);
        Task<IEnumerable<ClientsEntity>> GetALLClients( ClientsFilters clientsFilters);
        Task<IEnumerable<ClientsEntity>> GetClientsBasic(ClientsFilters clientsFilters);
        Task<IEnumerable<ClientsEntity>> GetClientsAdditional(ClientsFilters clientsFilters);
        Task<IEnumerable<ClientsEntity>> GetClientsAdditional2(ClientsFilters clientsFilters);
    }
}
