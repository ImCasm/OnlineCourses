using Application.ExceptionHandlers;
using MediatR;
using Persistence;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Comments.Commands
{
    public class Delete
    {
        public class Execute : IRequest
        {
            public Guid CommentId { get; set; }
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
                var comment = await _context.Comment.FindAsync(request.CommentId);
                if (comment == null)
                {
                    throw new ExceptionHandler(
                        HttpStatusCode.NotFound,
                        new { message = "No se encontró el curso" }
                    );
                }

                _context.Remove(comment);

                return await _context.SaveChangesAsync() > 0 ? 
                    Unit.Value : throw new Exception("No se pudo eliminar el curso");
            }
        }
    }
}
