using DataLayer;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Contracts;
using ServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Implementations;
public class ProfileService(BlogContext context) : IProfileService
{
    public async Task<(bool success, ChangePasswordResponseDTO? userProfile, User? user)> ProfileDetailAsync(string userId)
    {
        var user = await context.Users
            .Include(u => u.Posts)
            .Include(u => u.Comments)
            .FirstOrDefaultAsync(u => u.Id == int.Parse(userId));
        if (user == null)
        {
            return (false, null, user); 
        }
        var userProfile = new ChangePasswordResponseDTO
        {
            Id = user.Id,
           
        };
        return (true, userProfile, user); 
    }

    //sorulacak

    public async Task ChangePasswordAsync(ChangePasswordRequestDTO request)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (user != null)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            await context.SaveChangesAsync(); 
        }
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
