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
public class PostService(BlogContext context) : IPostService
{
    public async Task<bool> ProfileDetailAsync(string userId, out ChangePasswordResponseDTO? userProfile)
    {
        userProfile = null;

        var user = await context.Users
            .Include(u => u.Posts)
            .Include(u => u.Comments)
            .FirstOrDefaultAsync(u => u.Id == int.Parse(userId));

        if (user == null)
        {
            return false; // User not found
        }

        userProfile = new ChangePasswordResponseDTO
        {
            Id = user.Id,
           
        };

        return true;
    }


    public async Task ChangePasswordAsync(ChangePasswordRequestDTO requestDTO)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == requestDTO.Id);

        if (user != null)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(requestDTO.NewPassword);
            context.SaveChangesAsync();
        }

        
    }
}
