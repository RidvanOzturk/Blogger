using DataLayer;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Contracts;
using System.Security.Claims;

namespace ServiceLayer.Implementations;

public class UserService(BlogContext context) : IUserService
{
    public async Task<List<Claim>> ValidateUserAsync(string username, string password)
    {
        var user = await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Username == username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            return [];
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };

        return claims;
    }

    public async Task AddUserAsync(string username, string password)
    {
        var newUser = new User { Username = username, Password = BCrypt.Net.BCrypt.HashPassword(password) };
        await context.Users.AddAsync(newUser);
        await context.SaveChangesAsync();
    }
}
