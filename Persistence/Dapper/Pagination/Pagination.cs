using System.Collections.Generic;

namespace Persistence.Dapper.Pagination
{
    public class Pagination
    {
        public IList<IDictionary<string, object>> RecordsList { get; set; }
        public int TotalRecords { get; set; }
        public int PagNum { get; set; }
    }
}
