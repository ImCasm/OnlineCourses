using MediatR;
using Persistencia;
using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Aplicacion.ExceptionHandlers;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using Dominio;

namespace Aplicacion.Courses.CQRS
{
    public class EditCourse
    {

        public class Edit : IRequest
        {
            public Guid CourseId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime? PublicationDate { get; set; }
            public List<Guid> Teachers { get;set; }
            public decimal ActualPrice { get; set; }
            public decimal OfferPrice { get; set; }
        }

        public class Validation : AbstractValidator<Edit>
        {
            public Validation()
            {
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
                RuleFor(x => x.PublicationDate).NotEmpty();
                RuleFor(x => x.ActualPrice).NotEmpty();
                RuleFor(x => x.OfferPrice).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Edit>
        {

            private readonly CoursesContext _context;

            public Handler(CoursesContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Edit request, CancellationToken cancellationToken)
            {
                var course = await _context.Course.FindAsync(request.CourseId);

                if (course == null)
                {
                    throw new ExceptionHandler(HttpStatusCode.NotFound,
                                                new { mensaje = "No se encontró el curso" });
                }

                course.Title = request.Title ?? course.Title;
                course.Description = request.Description ?? course.Description;
                course.PublicationDate = request.PublicationDate ?? course.PublicationDate;

                /*Actualizar precio del curso*/
                var coursePrice = _context.Price.
                    Where(p => p.CourseId == course.CourseId).
                    FirstOrDefault();

                if (coursePrice != null)
                {
                    coursePrice.ActualPrice = request.ActualPrice;
                    coursePrice.Offer = request.OfferPrice;
                } else
                {
                    await _context.Price.AddAsync(new Price
                    {
                        PriceId = Guid.NewGuid(),
                        CourseId = course.CourseId,
                        ActualPrice = request.ActualPrice,
                        Offer = request.OfferPrice
                    });
                }

                /*Actualizar profesores del curso*/
                int result = 0;

                if (request.Teachers != null)
                {
                    
                    var teachersList = _context.CourseTeacher.
                        Where(c => c.CourseId == request.CourseId).
                        ToList();

                    foreach (var newTeacher in request.Teachers)
                    {

                        var repeatTeacher = teachersList.
                            Where(t => t.TeacherId == newTeacher);

                        if (repeatTeacher.Count() <= 0)
                        {
                            _context.CourseTeacher.Add(new CourseTeacher
                            {
                                CourseId = request.CourseId,
                                TeacherId = newTeacher
                            });

                        }
                        else { result++; }
                    }
                }

                result += await _context.SaveChangesAsync();

                if( result > 0 )
                {
                    return Unit.Value;
                }

                throw new Exception("No se guardaron los cambios en el curso");
            }
        }
    }
}
