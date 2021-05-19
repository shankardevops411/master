using Scheduleservice.Core.Entities;
using Scheduleservice.Data.Connections;  
using Scheduleservice.Services.Infrastructure.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Data.Repository
{
    public class CaregiversRepository : ICaregiversRepository
    {
      
        private readonly IConnectionProvider _connectionProvider;

        public CaregiversRepository(  IConnectionProvider connectionProvider)
        {
            
            this._connectionProvider = connectionProvider;
        }

        public async Task<IEnumerable<CaregiversEntity>> GetHHAEVVORNonEVVCaregivers(int HHA, int UserID, IEnumerable<int> Cargivers, bool IsEVVEnabled)
        {
            IEnumerable<CaregiversEntity> EVVCaregivers = new List<CaregiversEntity>();
            var sqlQuery = "";
            if (Cargivers.Count() > 2100)
            {
                sqlQuery = "Select CAREGIVER_ID, CAREGIVER_DISC_ID from Caregivers With(Nolock), CaregiverDisciplines With(Nolock)" +
                            "  Where Caregivers.HHA = @hha" +
                             "   and Caregivers.DISCIPLINE = CaregiverDisciplines.CAREGIVER_DISC_ID " +
                             "   and CaregiverDisciplines.HHA = @hha " +
                             "   and isnull(CaregiverDisciplines.IsEnableEVV, 0) = IsEnableEVV";
            }
            else
            {
                sqlQuery = "Select CAREGIVER_ID, CAREGIVER_DISC_ID from Caregivers With(Nolock), CaregiverDisciplines With(Nolock)" +
                                "  Where Caregivers.HHA = @hha" +
                                 "   and Caregivers.DISCIPLINE = CaregiverDisciplines.CAREGIVER_DISC_ID " +
                                 "   and CaregiverDisciplines.HHA = @hha " +
                                 "   and isnull(CaregiverDisciplines.IsEnableEVV, 0) = IsEnableEVV" +
                                 "   and Caregivers.CAREGIVER_ID  in @Cargivers ";
            }

            object parameter = new { HHA = HHA, Cargivers = Cargivers, IsEnableEVV = IsEVVEnabled }; 

            using (var uow = this._connectionProvider.Connect(HHA,UserID))
            {
                try
                {
                    IGenericRepository<CaregiversEntity> _genericRepository = new GenericMasterRepository<CaregiversEntity>(uow);
                    EVVCaregivers = await _genericRepository.Select(sqlQuery, parameter).ConfigureAwait(true);
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    uow.Dispose();
                }
            }

            return EVVCaregivers;
        }

        public async Task<IEnumerable<CaregiversEntity>> GetNonEVVCaregivers(int HHA, int UserID, IEnumerable<int> Cargivers)
        {
            IEnumerable<CaregiversEntity> EVVCaregivers = new List<CaregiversEntity>();

            try
            {
                var sqlQuery = "Select CAREGIVER_ID, CAREGIVER_DISC_ID from Caregivers With(Nolock), CaregiverDisciplines With(Nolock)" +
                                "  Where Caregivers.HHA = @hha" +
                                 "   and Caregivers.DISCIPLINE = CaregiverDisciplines.CAREGIVER_DISC_ID " +
                                 "   and CaregiverDisciplines.HHA = @hha " +
                                 "   and isnull(CaregiverDisciplines.IsEnableEVV, 0) = 0" +
                                 "   and Caregivers.CAREGIVER_ID  in @Cargivers ";

                object parameter = new { HHA = HHA, Cargivers = Cargivers };
                

                 
                using (var uow = this._connectionProvider.Connect(HHA,UserID))
                {
                    IGenericRepository<CaregiversEntity> _genericRepository = new GenericMasterRepository<CaregiversEntity>(uow);
                    EVVCaregivers = await _genericRepository.Select(sqlQuery, parameter).ConfigureAwait(true);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return EVVCaregivers;
        }

    }
}
