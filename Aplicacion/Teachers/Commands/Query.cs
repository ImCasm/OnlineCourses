using MediatR;
using Persistence.Dapper.Teacher;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Teachers.Commands
{
    public class Query
    {
        public class TeachersList : IRequest<List<Teacher>> { }

        public class Handler : IRequestHandler<TeachersList, List<Teacher>>
        {
            private readonly ITeacherRepository _teacherRepository;

            public Handler(ITeacherRepository teacherRepository)
            {
                this._teacherRepository = teacherRepository;
            }

            public async Task<List<Teacher>> Handle(TeachersList request, CancellationToken cancellationToken)
            {
                return (await _teacherRepository.GetTeachers()).ToList();
            }
        }
    }
}
