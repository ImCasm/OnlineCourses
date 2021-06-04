﻿using Application.ExceptionHandlers;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Security.Roles.Commands
{
    public class NewUserRole
    {
        public class Execute : IRequest
        {
            public string RoleName { get; set; }
        }

        public class Validation : AbstractValidator<Execute>
        {
            public Validation()
            {
                RuleFor(x => x.RoleName).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Execute>
        {
            private readonly RoleManager<IdentityRole> _roleManager;

            public Handler(RoleManager<IdentityRole> roleManager)
            {
                _roleManager = roleManager;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var role = await _roleManager.FindByNameAsync(request.RoleName);
                if (role != null)
                {
                    throw new ExceptionHandler(HttpStatusCode.BadRequest, new { message = "Ya existe el rol" });
                }

                var result = await _roleManager.CreateAsync(new IdentityRole(request.RoleName));

                return result.Succeeded ? Unit.Value : throw new Exception("No se pudo crear el rol");
            }
        }
    }
}
