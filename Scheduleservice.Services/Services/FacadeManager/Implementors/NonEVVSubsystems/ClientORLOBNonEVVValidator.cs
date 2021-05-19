using Scheduleservice.Services.EVVHandlers.EVVHandlersDto;
using Scheduleservice.Services.Infrastructure.RepositoryContracts;
using Scheduleservice.Services.Services.FacadeManager.Implementors.NonEVVSubsystems.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Services.Services.FacadeManager.Implementors.NonEVVSubsystems
{
    public class ClientORLOBNonEVVValidator: IClientORLOBNonEVVValidator
    {
        private IClientsRepository _clientsRepository;
        private I_C_LineofbusinessRepository _I_C_Lineofbusinness;

        public ClientORLOBNonEVVValidator(IClientsRepository clientsRepository, I_C_LineofbusinessRepository c_Lineofbusinness)
        {
            _clientsRepository = clientsRepository;
            _I_C_Lineofbusinness = c_Lineofbusinness;
        }

        public async Task<List<ScheduleEVVInfoDto>> ValidateClientORLobNonEVVFlag(int HHA, int UserID, List<ScheduleEVVInfoDto> HHASchedulesList)
        {
            List<ScheduleEVVInfoDto> OutHHASchedulesList = new List<ScheduleEVVInfoDto>();
            OutHHASchedulesList = HHASchedulesList;
            try
            {
                var scheduleClients = HHASchedulesList.Select(x => x.CLIENT_ID).Distinct();

                //Get EVV HHA Clients 
                var HHAClients = await _clientsRepository.GetClients(HHA, UserID, scheduleClients);
                //get EVV LOBS
                var NonEVVLOBs = await _I_C_Lineofbusinness.GetHHALOBs(HHA, UserID, false);

                //check  if there is Non EVV Clients and EVV LOBS
                if (HHAClients.Where(x => x.Enable_EVV == false && x.LOB_ID > 0).Count() > 0 && NonEVVLOBs.Count() > 0)
                {
                    HHAClients.Where(x => x.Enable_EVV == false && x.LOB_ID > 0).ToList().ForEach(c => c.Enable_EVV = NonEVVLOBs.Where(l => l.LOB_ID == c.LOB_ID).Count() > 0 ? false : true);
                }

                //Get Non EVV Clients 
                var NonEVVClients = HHAClients.Where(x => x.Enable_EVV == false).ToList();

                //Check Client is evv or not
                if (NonEVVClients.Count() > 0)
                {
                    OutHHASchedulesList.ForEach(x => x.IsRealNonEVV = false);
                    OutHHASchedulesList.Join(NonEVVClients, (sch) => sch.CLIENT_ID, (cli) => cli.CLIENT_ID,
                                       (sch, cli) =>
                                       {
                                           sch.IsRealNonEVV = true;
                                           return sch;
                                       }
                                       ).ToList();

                    OutHHASchedulesList.Where(x => x.IsRealNonEVV == false).ToList().ForEach(y => y.IsClientORLOBEVV = true);

                }
                else
                    OutHHASchedulesList.ForEach(y => y.IsClientORLOBEVV = true);

            }
            catch (Exception ex)
            {
                throw;
            }

            return OutHHASchedulesList;
        }

    }
}
