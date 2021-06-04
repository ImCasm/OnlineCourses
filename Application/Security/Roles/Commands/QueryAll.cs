using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Security.Roles.Commands
{
    public class QueryAll
    {
        public class Execute : IRequest<List<IdentityRole>> { }

        public class Handler : IRequestHandler<Execute, List<IdentityRole>>
        {
            private readonly CoursesContext _context;

            public Handler(CoursesContext context)
            {
                _context = context;
            }

            public async Task<List<IdentityRole>> Handle(Execute request, CancellationToken cancellationToken)
            {
                return await _context.Roles.ToListAsync();
            }
        }
    }
}
