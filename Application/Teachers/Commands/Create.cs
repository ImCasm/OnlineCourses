using FluentValidation;
using MediatR;
using Persistence.Dapper.Teacher;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Teachers.Commands
{
    public class Create
    {
        public class Execute : Teacher, IRequest { }

        public class Validation : AbstractValidator<Execute>
        {
            public Validation()
            {
                RuleFor(x => x.FirstName).NotEmpty();
                RuleFor(x => x.LastName).NotEmpty();
                RuleFor(x => x.JobTitle).NotEmpty();
            }
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
                return await _teacherRepository.InsertTeacher(request) > 0 ? 
                    Unit.Value : throw new Exception("No se pudo insertar el instructor.");
            }
        }
    }
}
