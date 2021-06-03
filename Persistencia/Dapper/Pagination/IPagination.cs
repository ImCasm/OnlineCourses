using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence.Dapper.Pagination
{
    public interface IPagination
    {
        Task<Pagination> GetPagination(
            string storeProcedure,
            int pagNum,
            int totalRecords,
            IDictionary<string, object> filter,
            string columnOrder);
    }
}
