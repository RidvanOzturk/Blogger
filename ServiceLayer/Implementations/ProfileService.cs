using DataLayer;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Contracts;
using ServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Implementations;
public class PostService(BlogContext context) : IPostService
{

    public async Task ChangePasswordAsync(ChangePasswordRequestDTO requestDTO)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == requestDTO.Id);

        if (user != null)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(requestDTO.NewPassword);
            context.SaveChanges();
        }

        
    }
}
