using Domain;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence
{
    public class TestData
    {

        public static async Task InsertData(CoursesContext context, UserManager<User> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new User { FullName = "Cristian Sánchez", UserName = "admin", Email = "admin@gmail.com" };
                await userManager.CreateAsync(user, "admin");
            }
        }
    }
}
