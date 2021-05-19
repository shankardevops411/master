using Dapper;
using Scheduleservice.Data.UnitofWorks;
using Scheduleservice.Services.Infrastructure.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleservice.Data.Repository
{
    public class GenericMasterRepository<T> : IGenericRepository<T> 
    {      

        private IUnitOfWork _uow;
        IDbConnection Connection = null; 

        public GenericMasterRepository(IUnitOfWork uow)
        {
            this._uow = uow;
            Connection = _uow.Connection;            
        }


        public async Task<int> Execute(string sql, object paramenters)
        {
            return await Connection.ExecuteAsync(sql, paramenters).ConfigureAwait(false);
        }

        public async Task<T> Get(string sql, object paramenters)
        {
            var result = await Connection.QuerySingleAsync<T>(sql, paramenters).ConfigureAwait(false);
            return result;
        }
        public async Task<T> SelectSingle(string sql, object paramenters)
        {
            try
            {
                var result = await Connection.QueryFirstAsync<T>(sql, paramenters).ConfigureAwait(false);
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<IEnumerable<T>> Select(string sql, object paramenters)
        {
            try
            {
                var result = await Connection.QueryAsync<T>(sql, paramenters).ConfigureAwait(false);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<T>> SelectAll(string sql)
        {
            try
            {
                var result = await Connection.QueryAsync<T>(sql).ConfigureAwait(false);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<T>> SelectProcedure(string procedurename, object paramenters)
        {
            try
            {
                var result = await Connection.QueryAsync<T>(procedurename, paramenters, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                return result;

            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<int> ExecuteProcedure(string procedurename, object paramenters)
        {
            try
            {
                var result = await Connection.ExecuteAsync(procedurename, paramenters, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                return result;
                
            }
            catch (Exception e)
            {

                throw;
            }
        }


    }
}
