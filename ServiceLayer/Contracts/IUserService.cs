using System.Security.Claims;

namespace ServiceLayer.Contracts;

public interface IUserService
{
    Task<List<Claim>> ValidateUserAsync(string username, string password);
    Task AddUserAsync(string username, string password);
}
