using MediatR;
using Persistence.Dapper.Teacher;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Teachers.Commands
{
    public class Delete
    {
        public class Execute: IRequest
        {
            public Guid TeacherId { get; set; }
        }

        public class Handler : IRequestHandler<Execute>
        {
            private readonly ITeacherRepository _teacherRepository;

            public Handler(ITeacherRepository teacherRepository)
            {
                _teacherRepository = teacherRepository;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                return await _teacherRepository.DeleteTeacher(request.TeacherId) > 0 ? 
                    Unit.Value : throw new Exception("No se pudo eliminar el instructor");
            }
        }
    }
}
