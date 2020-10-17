using Aplicacion.Courses.DTO;
using Aplicacion.ExceptionHandlers;
using AutoMapper;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Courses.CQRS
{
    public class QueryById
    {
    
        public class CourseById : IRequest<CourseDTO>
        {
             public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<CourseById, CourseDTO>
        {
            private readonly CoursesContext _context;
            private readonly IMapper _mapper;

            public Handler(CoursesContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CourseDTO> Handle(CourseById request, CancellationToken cancellationToken)
            {
                var course = await _context.Course.
                    Include(c => c.OfferPrice).
                    Include(c => c.Comments).
                    Include(c => c.Teachers).
                    ThenInclude(courseTeacher => courseTeacher.Teacher).
                    FirstOrDefaultAsync(c => c.CourseId == request.Id);

                if (course == null)
                {
                    throw new ExceptionHandler(HttpStatusCode.NotFound,
                                                new { mensaje = "No se encontró el curso" });
                }

                // Retorna el el mapeo de Course a CourseDTO
                return _mapper.Map<Course, CourseDTO>(course);
            }
        }
    }
}
