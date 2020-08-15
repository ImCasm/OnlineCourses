using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.Courses
{
    public class Create
    {

        public class New : IRequest
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime? PublicationDate { get; set; }
        }

        public class Validation : AbstractValidator<New>
        {
            public Validation()
            {
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
                RuleFor(x => x.PublicationDate).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<New>
        {

            private readonly CoursesContext _context;

            public Handler(CoursesContext context)
            {
                this._context = context;
            }

            public async Task<Unit> Handle(New request, CancellationToken cancellationToken)
            {
                Course course = new Course()
                {
                    Title = request.Title,
                    Description = request.Description,
                    PublicationDate = request.PublicationDate

                };

                this._context.Course.Add(course);
                var Result = await _context.SaveChangesAsync();
                if (Result > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("Error al insertar el curso");
            }
        }
    }
}
