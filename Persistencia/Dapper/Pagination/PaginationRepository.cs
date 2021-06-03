using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Linq;

namespace Persistence.Dapper.Pagination
{
    public class PaginationRepository : IPagination
    {
        private readonly IFactoryConnection _connection;

        public PaginationRepository(IFactoryConnection connection)
        {
            _connection = connection;
        }

        public async Task<Pagination> GetPagination(
            string storeProcedure,
            int pagNum,
            int numRecords,
            IDictionary<string, object> filters,
            string columnOrder)
        {
            Pagination pagination = new Pagination();
            int totalRecords = 0;
            int totalPages = 0;

            try
            {
                var connection = _connection.GetConnection();
                DynamicParameters parameters = new DynamicParameters();

                foreach (var param in filters)
                {
                    parameters.Add($"@{param.Key}", param.Value);
                }

                parameters.Add("@PagNum", pagNum);
                parameters.Add("@NumRecords", numRecords);
                parameters.Add("@ColumnOrder", columnOrder);

                parameters.Add("@TotalRecords", totalRecords, DbType.Int32, ParameterDirection.Output);
                parameters.Add("@TotalPages", totalPages, DbType.Int32, ParameterDirection.Output);

                var result = await connection.QueryAsync(storeProcedure, parameters,
                    commandType: CommandType.StoredProcedure);
                pagination.recordsList = result.Select(r => (IDictionary<string, object>) r).ToList();

                pagination.totalRecords = parameters.Get<int>("@TotalRecords");
                pagination.pagNum = parameters.Get<int>("@TotalPages");
            }
            catch (Exception e)
            {

                throw new Exception("No se pudo ejecutar el procedimiento almacenado ", e);
            } 
            finally
            {
                _connection.CloseConnection();
            }

            return pagination;
        }
    }
}
