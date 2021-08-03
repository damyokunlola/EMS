using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPICore.Generics
{
    public class GenericRepo<TEntity> : IGenericRepo<TEntity> where TEntity : class
    {

        private readonly IConfiguration _configuration;

        public GenericRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task<IEnumerable<TEntity>> AllAsync()
        {
            throw new NotImplementedException();

        }
        public async Task<IEnumerable<TEntity>> QueryAsync(string sql, CommandType commandType, object parameter = null)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("EmployeeAppCon")))
            {
                return con.Query<TEntity>(sql, parameter, commandType: commandType).ToList();
            };
        }
        public async Task<int> ExecuteQuerySync(string sql, CommandType commandType, object parameter = null)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("EmployeeAppCon")))
            {
                return await con.ExecuteAsync(sql: sql, param: parameter, transaction: null, commandType:commandType);
            };
        }

        public Task<TEntity> FindAsync(object pk)
        {
            throw new NotImplementedException();

        }

        public Task<IEnumerable<TEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

       
    }
}
