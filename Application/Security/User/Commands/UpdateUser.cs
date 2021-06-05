using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Application.Interfaces;
using Application.ExceptionHandlers;
using System.Net;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace Application.Security.AppUser.Commands
{
    public class UpdateUser
    {
        public class Execute : IRequest<AuthUserData>
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string UserName { get; set; }
        }

        public class Validation : AbstractValidator<Execute>
        {
            public Validation()
            {
                RuleFor(x => x.UserName).NotEmpty();
                RuleFor(x => x.LastName).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.UserName).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Execute, AuthUserData>
        {
            private readonly CoursesContext _context;
            private readonly UserManager<User> _userManager;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly IPasswordHasher<User> _passwordHasher;

            public Handler(
                CoursesContext context,
                UserManager<User> userManager,
                IJwtGenerator jwtGenerator,
                IPasswordHasher<User> passwordHasher)
            {
                _context = context;
                _userManager = userManager;
                _jwtGenerator = jwtGenerator;
                _passwordHasher = passwordHasher;
            }

            public async Task<AuthUserData> Handle(Execute request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(request.UserName);
                if (user == null)
                {
                    throw new ExceptionHandler(
                        HttpStatusCode.NotFound,
                        new { message = "No se encuentra el usuario" }
                    );
                }

                var emailExist = await _context.Users
                    .Where(u => u.Email == request.Email && u.UserName != request.UserName)
                    .AnyAsync();

                if (emailExist)
                {
                    throw new Exception("El email pertenece a otro usuario");
                }

                user.FullName = $"{request.FirstName} {request.LastName}";
                user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
                user.Email = request.Email;

                var result = await _userManager.UpdateAsync(user);
                var roles = (await _userManager.GetRolesAsync(user)).ToList();

                return result.Succeeded ? new AuthUserData()
                {
                    FullName = user.FullName,
                    Email = request.Email,
                    Username = request.UserName,
                    Token = _jwtGenerator.CreateToken(user, roles)
                } : throw new Exception("No se pudo actualizar el usuario");
            }
        }
    }
}
