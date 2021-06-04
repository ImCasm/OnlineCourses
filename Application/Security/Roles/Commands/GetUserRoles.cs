using Application.ExceptionHandlers;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Security.Roles.Commands
{
    public class GetUserRoles
    {
        public class Execute : IRequest<List<string>>
        {
            public string UserName { get; set; }
        }

        public class Validation : AbstractValidator<Execute>
        {
            public Validation()
            {
                RuleFor(x => x.UserName).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Execute, List<string>>
        {
            private readonly UserManager<User> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;

            public Handler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
            {
                _userManager = userManager;
                _roleManager = roleManager;
            }

            public async Task<List<string>> Handle(Execute request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(request.UserName);

                if (user == null)
                {
                    throw new ExceptionHandler(
                        HttpStatusCode.NotFound,
                        new { message = "No existe el usuario" }
                    );
                }

                return new List<string>(await _userManager.GetRolesAsync(user));
            }
        }
    }
}
