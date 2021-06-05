using Application.ExceptionHandlers;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Security
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
            private readonly IJwtGenerator _jwtGenerator;

            public Handler(UserManager<User> userManager, SignInManager<User> signInManager, IJwtGenerator jwtGenerator)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _jwtGenerator = jwtGenerator;
            }

            public async Task<AuthUserData> Handle(Run request, CancellationToken cancellationToken)
            {

                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user == null)
                {
                    throw new ExceptionHandler(HttpStatusCode.Unauthorized);
                }

                var roles = (await _userManager.GetRolesAsync(user)).ToList();
                var successLogIn = 
                    await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                if (successLogIn.Succeeded)
                {
                    return new AuthUserData{ 
                        FullName = user.FullName,
                        Email = user.Email,
                        Username = user.UserName,
                        Token = _jwtGenerator.CreateToken(user, roles),
                        Image = null
                    };
                }

                throw new ExceptionHandler(HttpStatusCode.Unauthorized);
            }
        }

    }
}
