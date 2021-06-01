using Domain;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Comments.Commands
{
    public class Create
    {
        public class Execute : IRequest
        {
            public string Student { get; set; }
            public int Review { get; set; }
            public string Content { get; set; }
            public Guid CourseId { get; set; }
        }

        public class Validation : AbstractValidator<Execute>
        {
            public Validation()
            {
                RuleFor(x => x.Student).NotEmpty();
                RuleFor(x => x.Review).NotEmpty().GreaterThan(0).LessThanOrEqualTo(5);
                RuleFor(x => x.CourseId).NotEmpty();
                RuleFor(x => x.Content).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Execute>
        {

            private readonly CoursesContext _context;

            public Handler(CoursesContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var comment = new Comment
                {
                    CommentId = Guid.NewGuid(),
                    Student = request.Student,
                    Content = request.Content,
                    Review = request.Review,
                    CourseId = request.CourseId
                };

                _context.Comment.Add(comment);

                return await _context.SaveChangesAsync() > 0 ? 
                    Unit.Value : throw new Exception("No se pudo almacenar el comentario");
            }
        }
    }
}
