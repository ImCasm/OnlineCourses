using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Courses
{
    public class Query
    {
        public class CoursesList : IRequest<List<Course>>{}

        public class Handler : IRequestHandler<CoursesList, List<Course>>
        {
            private readonly CoursesContext _context;

            public Handler(CoursesContext context)
            {
                _context = context;
            }

            public async Task<List<Course>> Handle(CoursesList request, CancellationToken cancellationToken)
            {
                return await _context.Course.ToListAsync();
            }
        }
    }
}