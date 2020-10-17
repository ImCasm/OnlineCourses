using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.Courses.CQRS
{
    public class Create
    {

        public class New : IRequest
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime? PublicationDate { get; set; }
            public IEnumerable<Guid> Teachers { get; set; }
            public decimal ActualPrice { get; set; }
            public decimal OfferPrice { get; set; }
        }

        public class Validation : AbstractValidator<New>
        {
            public Validation()
            {
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
                RuleFor(x => x.PublicationDate).NotEmpty();
                RuleFor(x => x.Teachers).NotEmpty();
                RuleFor(x => x.ActualPrice).NotEmpty();
                RuleFor(x => x.OfferPrice).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<New>
        {

            private readonly CoursesContext _context;

            public Handler(CoursesContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(New request, CancellationToken cancellationToken)
            {
                /*Insertar Curso*/
                Course course = new Course()
                {
                    Title = request.Title,
                    Description = request.Description,
                    PublicationDate = request.PublicationDate,
                };

                var createdCourse = _context.Course.Add(course);

                /*Insertar Profesores*/
                foreach (var teacherId in request.Teachers)
                {
                    _context.CourseTeacher.Add(new CourseTeacher { 
                        TeacherId = teacherId,
                        CourseId = createdCourse.Entity.CourseId
                    });
                }

                /*Insertar Precio*/
                var newPrice = new Price
                {
                    PriceId = Guid.NewGuid(),
                    ActualPrice = request.ActualPrice,
                    Offer = request.OfferPrice,
                    CourseId = createdCourse.Entity.CourseId
                };

                _context.Price.Add(newPrice);

                /*Guardando Cambios*/
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
