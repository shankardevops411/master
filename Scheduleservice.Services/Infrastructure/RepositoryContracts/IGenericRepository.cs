using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduleservice.Services.Infrastructure.RepositoryContracts
{
    public interface IGenericRepository<T>
    {         
        Task<T> Get(string sql, object paramenters);
        Task<IEnumerable<T>> Select(string sql, object paramenters);
        Task<int> Execute(string sql, object paramenters);
        Task<IEnumerable<T>> SelectProcedure(string procedurename, object paramenters);
        Task<T> SelectSingle(string sql, object paramenters);
        Task<IEnumerable<T>> SelectAll(string sql);
        Task<int> ExecuteProcedure(string procedurename, object paramenters);
    }
}
