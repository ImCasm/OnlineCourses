using Aplicacion.ExceptionHandlers;
using Dominio;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Courses
{
    public class QueryId
    {
    
        public class CourseId : IRequest<Course>
        {
             public int Id { get; set; }
        }

        public class Handler : IRequestHandler<CourseId, Course>
        {
            private readonly CoursesContext _context;

            public Handler(CoursesContext context)
            {
                _context = context;
            }

            public async Task<Course> Handle(CourseId request, CancellationToken cancellationToken)
            {
                var course = await _context.Course.FindAsync(request.Id);

                if (course == null)
                {
                    throw new ExceptionHandler(HttpStatusCode.NotFound,
                                                new { mensaje = "No se encontró el curso" });
                }

                return course;
            }
        }
    }
}
