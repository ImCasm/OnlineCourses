using Application.Courses.DTO;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Courses.Commands
{
    public class Query
    {
        public class CoursesList : IRequest<List<CourseDTO>>{}

        public class Handler : IRequestHandler<CoursesList, List<CourseDTO>>
        {
            private readonly CoursesContext _context;
            private readonly IMapper _mapper;

            public Handler(CoursesContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<CourseDTO>> Handle(CoursesList request, CancellationToken cancellationToken)
            {
                
                var courses = await _context.Course.
                    Include(c => c.OfferPrice).
                    Include(c => c.Comments).
                    Include(c => c.Teachers).
                    ThenInclude(t => t.Teacher).
                    ToListAsync();

                return _mapper.Map<List<Course>, List<CourseDTO>>(courses);
            }
        }
    }
}