using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPICore.Generics
{
   public interface IGenericRepo<TEntity> where TEntity:class
    {
        Task<IEnumerable<TEntity>> AllAsync();
        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> FindAsync(object pk);
        Task<int> ExecuteQuerySync(string sql, CommandType commandType, object parameter = null);
        Task<IEnumerable<TEntity>> QueryAsync(string sql, CommandType commandType, object parameter = null);
    }
}
