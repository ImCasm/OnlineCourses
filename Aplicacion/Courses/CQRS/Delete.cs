using Aplicacion.ExceptionHandlers;
using MediatR;
using Persistencia;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Courses.CQRS
{
    public class Delete
    {
        public class DeleteCourse : IRequest
        {
            public Guid CourseId { get; set; }
        }

        public class Handler : IRequestHandler<DeleteCourse>
        {
            private readonly CoursesContext _context;

            public Handler(CoursesContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteCourse request, CancellationToken cancellationToken)
            {

                //var priceCourse = _context.Price.Where(prc => prc.CourseId == request.CourseId);

                var course = await _context.Course.FindAsync(request.CourseId);

                if (course == null)
                {
                    throw new ExceptionHandler(HttpStatusCode.NotFound,
                                                new { mensaje = "No se encontró el curso" });
                }

                _context.Course.Remove(course);

                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("Error al eliminar el curso");
            }
        }
    }
}
