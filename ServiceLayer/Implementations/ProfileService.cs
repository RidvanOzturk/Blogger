using DataLayer;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Contracts;
using ServiceLayer.DTOs;


namespace ServiceLayer.Implementations;
public class ProfileService(BlogContext context) : IProfileService
{
    public async Task<(bool Success, ChangePasswordResponseDTO? UserProfile, User? User)> ProfileDetailAsync(int userId)
    {
        var user = await context.Users
            .Include(u => u.Posts)
            .Include(u => u.Comments)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            return (false, null, null);
        }

        var userProfile = new ChangePasswordResponseDTO
        {
            Id = user.Id
        };

        return (true, userProfile, user);
    }



    //sorulacak

    public async Task ChangePasswordAsync(ChangePasswordRequestDTO model)
    {
        // Model'i DTO'ya dönüştürüyoruz.
        var changePasswordRequestDTO = new ChangePasswordRequestDTO
        {
            Id = model.Id,
            NewPassword = model.NewPassword,
            ConfirmPassword = model.ConfirmPassword
        };

        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == changePasswordRequestDTO.Id);

        if (user != null)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(changePasswordRequestDTO.NewPassword);
            await context.SaveChangesAsync();
        }
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        return await context.Users
            .Include(u => u.Posts)
            .Include(u => u.Comments)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }


    public async Task<ChangePasswordResponseDTO?> CurrentUserAsync(int userId)
    {
        var user = await context.Users
            .AsNoTracking()
            .Include(u => u.Posts)
            .Include(u => u.Comments)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            return null;
        }

        return new ChangePasswordResponseDTO
        {
            Id = user.Id,
        };
    }
}
