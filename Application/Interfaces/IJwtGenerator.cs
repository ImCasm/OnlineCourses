using Domain;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IJwtGenerator
    {
        string CreateToken(User user, List<string> roles);
    }
}
