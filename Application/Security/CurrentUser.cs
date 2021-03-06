﻿using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Security
{
    public class CurrentUser
    {
        
        public class Current : IRequest<AuthUserData>
        {

        }

        public class Handler : IRequestHandler<Current, AuthUserData>
        {

            private readonly UserManager<User> _userManager;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly IUserSession _userSession;

            public Handler(UserManager<User> userManager, IJwtGenerator jwtGenerator, IUserSession userSession)
            {
                _userManager = userManager;
                _jwtGenerator = jwtGenerator;
                _userSession = userSession;
            }

            public async Task<AuthUserData> Handle(Current request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(_userSession.GetUserSession());
                var roles = (await _userManager.GetRolesAsync(user)).ToList();

                return new AuthUserData
                {
                    FullName = user.FullName,
                    Username = user.UserName,
                    Email = user.Email,
                    Token = _jwtGenerator.CreateToken(user, roles),
                    Image = null
                };
            }
        }
    }
}
