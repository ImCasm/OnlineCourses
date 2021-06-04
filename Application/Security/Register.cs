
using Application.ExceptionHandlers;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Security
{
    public class Register
    {

        public class SignUp : IRequest<AuthUserData>
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class Validator : AbstractValidator<SignUp>
        {
            public Validator()
            {
                RuleFor(x => x.FirstName).NotEmpty();
                RuleFor(x => x.LastName).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Username).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<SignUp, AuthUserData>
        {

            private readonly CoursesContext _coursesContext;
            private readonly UserManager<User> _userManager;
            private readonly IJwtGenerator _jwtGenerator;

            public Handler(CoursesContext coursesContext, UserManager<User> userManager, IJwtGenerator jwtGenerator)
            {
                _coursesContext = coursesContext;
                _userManager = userManager;
                _jwtGenerator = jwtGenerator;
            }

            public async Task<AuthUserData> Handle(SignUp request, CancellationToken cancellationToken)
            {
                var validUser = await _coursesContext.Users
                    .Where(user => user.Email == request.Email || user.UserName == request.Username).AnyAsync();

                if (validUser)
                {
                    throw new ExceptionHandler(HttpStatusCode.BadRequest, new
                    {
                        message = "Ya existe un usuario registrado con este email o username"
                    });
                }

                var user = new User
                {
                    FullName = $"{request.FirstName} {request.LastName}",
                    Email = request.Email,
                    UserName = request.Username
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    return new AuthUserData
                    {
                        FullName = user.FullName,
                        Email = user.Email,
                        Token = _jwtGenerator.CreateToken(user),
                        Username = user.UserName
                    };
                }

                throw new Exception("Error, No se pudo crear el usuario");
            }
        }
    }
}
