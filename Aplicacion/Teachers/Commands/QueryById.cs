using Application.ExceptionHandlers;
using MediatR;
using Persistence.Dapper.Teacher;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Teachers.Commands
{
    public class QueryById
    {
        public class Execute : IRequest<Teacher>
        {
            public Guid TeacherId{ get; set; }
        }

        public class Handler : IRequestHandler<Execute, Teacher>
        {
            private readonly ITeacherRepository _teacherRepository;
            public Handler(ITeacherRepository teacherRepository)
            {
                _teacherRepository = teacherRepository;
            }

            public async Task<Teacher> Handle(Execute request, CancellationToken cancellationToken)
            {
                var response = await _teacherRepository.GetTeacherById(request.TeacherId);
                return response ?? throw new ExceptionHandler(
                    HttpStatusCode.NotFound,
                    new { message = "No se encontró el instructor" });
            }
        }
    }
}
