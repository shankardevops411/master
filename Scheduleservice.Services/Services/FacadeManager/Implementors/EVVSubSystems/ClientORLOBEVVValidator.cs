using Scheduleservice.Services.EVVHandlers.EVVHandlersDto;
using Scheduleservice.Services.Infrastructure.RepositoryContracts;
using Scheduleservice.Services.Services.FacadeManager.Implementors.EVVSubSystems.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Services.Services.FacadeManager.Implementors.EVVSubSystems
{
    public class ClientORLOBEVVValidator: IClientORLOBEVVValidator
    {

        private IClientsRepository _clientsRepository;
        private I_C_LineofbusinessRepository _I_C_Lineofbusinness;

        public ClientORLOBEVVValidator(IClientsRepository clientsRepository, I_C_LineofbusinessRepository c_Lineofbusinness)
        {
            _clientsRepository = clientsRepository;
            _I_C_Lineofbusinness = c_Lineofbusinness;
        }

        public async Task<List<ScheduleEVVInfoDto>> ValidateClientORLobEVVFlag(int HHA, int UserID, List<ScheduleEVVInfoDto> HHASchedulesList)
        {
            List<ScheduleEVVInfoDto> OutHHASchedulesList = new List<ScheduleEVVInfoDto>();
            OutHHASchedulesList = HHASchedulesList;
            try
            {
                var scheduleClients = HHASchedulesList.Select(x => x.CLIENT_ID).Distinct();
                //Get EVV HHA Clients 
                var HHAClients = await _clientsRepository.GetClients(HHA, UserID, scheduleClients);
                //get EVV LOBS
                var EVVLOBs = await _I_C_Lineofbusinness.GetHHALOBs(HHA, UserID, true);

                //check  if there is Non EVV Clients and EVV LOBS
                if (HHAClients.Where(x => x.Enable_EVV == false && x.LOB_ID > 0).Count() > 0 && EVVLOBs.Count() > 0)
                {
                    HHAClients.Where(x => x.Enable_EVV == false && x.LOB_ID > 0).ToList().ForEach(c => c.Enable_EVV = EVVLOBs.Where(l => l.LOB_ID == c.LOB_ID).Count() > 0 ? true : false);
                }

                //Get EVV Enabled Clients
                var EVVClients = HHAClients.Where(x => x.Enable_EVV == true).ToList();

                //Check Client is evv or not
                if (EVVClients.Count() > 0)
                {
                    OutHHASchedulesList.ForEach(x => x.IsRealEVV = false);
                    OutHHASchedulesList.Join(EVVClients, (sch) => sch.CLIENT_ID, (cli) => cli.CLIENT_ID,
                                       (sch, cli) =>
                                       {
                                           sch.IsRealEVV = true;
                                           return sch;
                                       }
                                       ).ToList();

                    OutHHASchedulesList.RemoveAll(x => x.IsRealEVV == false);

                }

            }
            catch (Exception ex)
            {
                throw;
            }

            return OutHHASchedulesList;
        }
    }
}
