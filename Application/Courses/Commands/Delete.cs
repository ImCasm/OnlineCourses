using Application.ExceptionHandlers;
using MediatR;
using Persistence;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Courses.Commands
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

                var comments = _context.Comment.Where(c => c.CourseId == request.CourseId);
                foreach (var comment in comments)
                {
                    _context.Comment.Remove(comment);
                }

                var price = _context.Price.Where(prc => prc.CourseId == request.CourseId).FirstOrDefault();
                if (price != null)
                {
                    _context.Price.Remove(price);
                }

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
