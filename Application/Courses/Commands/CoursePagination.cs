using Domain;
using MediatR;
using Persistence.Dapper.Pagination;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Courses.Commands
{
    public class CoursePagination
    {
        public class Execute : IRequest<Pagination>
        {
            public string Title { get; set; }
            public int PagNum { get; set; }
            public int NumRecords { get; set; }
        }

        public class Handler : IRequestHandler<Execute, Pagination>
        {
            private readonly IPagination _pagination;

            public Handler(IPagination pagination)
            {
                _pagination = pagination;
            }

            public async Task<Pagination> Handle(Execute request, CancellationToken cancellationToken)
            {
                string storeProcedure = "sp_Get_Course_Pagination";
                string order = "Title";
                var parameters = new Dictionary<string, object>();
                parameters.Add("CourseTitle", request.Title);

                return await _pagination.GetPagination(storeProcedure, request.PagNum, request.NumRecords,
                    parameters, order);
            }
        }
    }
}
