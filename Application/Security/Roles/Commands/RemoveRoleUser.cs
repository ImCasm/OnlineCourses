using Application.ExceptionHandlers;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Security.Roles.Commands
{
    public class RemoveRoleUser
    {
        public class Execute : IRequest
        {
            public string UserName { get; set; }
            public string RoleName { get; set; }
        }

        public class Validation : AbstractValidator<Execute>
        {
            public Validation()
            {
                RuleFor(x => x.UserName).NotEmpty();
                RuleFor(x => x.RoleName).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Execute>
        {
            private readonly UserManager<User> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;

            public Handler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
            {
                _userManager = userManager;
                _roleManager = roleManager;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var role = await _roleManager.FindByNameAsync(request.RoleName);
                var user = await _userManager.FindByNameAsync(request.UserName);

                if (role == null || user == null)
                {
                    throw new ExceptionHandler(
                        HttpStatusCode.BadRequest,
                        new { message = role == null ? "No exsite el rol" : "No existe el usuario" }
                    );
                }

                var result = await _userManager.RemoveFromRoleAsync(user, request.RoleName);

                return result.Succeeded ? 
                    Unit.Value : throw new Exception("No se pudo remover el rol del usuario");
            }
        }
    }
}
