using MediatR;
using Persistencia;
using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Aplicacion.ExceptionHandlers;
using System.Net;

namespace Aplicacion.Courses
{
    public class Edit
    {

        public class EditCourse : IRequest
        {
            public int CourseId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime? PublicationDate { get; set; }
        }

        public class Validation : AbstractValidator<EditCourse>
        {
            public Validation()
            {
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
                RuleFor(x => x.PublicationDate).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<EditCourse>
        {

            private readonly CoursesContext _context;

            public Handler(CoursesContext context)
            {
                this._context = context;
            }

            public async Task<Unit> Handle(EditCourse request, CancellationToken cancellationToken)
            {
                var course = await this._context.Course.FindAsync(request.CourseId);

                if (course == null)
                {
                    throw new ExceptionHandler(HttpStatusCode.NotFound,
                                                new { mensaje = "No se encontró el curso" });
                }

                course.Title = request.Title ?? course.Title;
                course.Description = request.Description ?? course.Description;
                course.PublicationDate = request.PublicationDate ?? course.PublicationDate;

                var result = await _context.SaveChangesAsync();

                if( result > 0 )
                {
                    return Unit.Value;
                }

                throw new Exception("No se guardaron los cambios en el curso");
            }
        }
    }
}
