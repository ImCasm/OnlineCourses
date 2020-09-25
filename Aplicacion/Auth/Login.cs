using Aplicacion.ExceptionHandlers;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Auth
{
    public class Login
    {

        public class Run : IRequest<AuthUserData>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class Validator : AbstractValidator<Run>
        {
            public Validator()
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Run, AuthUserData>
        {
            private readonly UserManager<User> _userManager;
            private readonly SignInManager<User> _signInManager;

            public Handler(UserManager<User> userManager, SignInManager<User> signInManager)
            {
                _userManager = userManager;
                _signInManager = signInManager;
            }

            public async Task<AuthUserData> Handle(Run request, CancellationToken cancellationToken)
            {

                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user == null)
                {
                    throw new ExceptionHandler(HttpStatusCode.Unauthorized);
                }

                var successLogIn = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                if (successLogIn.Succeeded)
                {
                    return new AuthUserData{ 
                        FullName = user.fullName,
                        Email = user.Email,
                        Username = user.UserName,
                        Token = null,
                        Image = null
                    };
                }

                throw new ExceptionHandler(HttpStatusCode.Unauthorized);
            }
        }

    }
}
