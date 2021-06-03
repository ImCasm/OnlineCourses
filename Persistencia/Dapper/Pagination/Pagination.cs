using System.Collections.Generic;

namespace Persistence.Dapper.Pagination
{
    public class Pagination
    {
        public IList<IDictionary<string, object>> recordsList { get; set; }
        public int totalRecords { get; set; }
        public int pagNum { get; set; }
    }
}
