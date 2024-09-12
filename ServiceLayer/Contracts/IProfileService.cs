using DataLayer.Entities;
using ServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Contracts;
public interface IProfileService
{
    Task<(bool success, ChangePasswordResponseDTO? userProfile, User user)> ProfileDetailAsync(string userId);
    Task ChangePasswordAsync(ChangePasswordRequestDTO request);
    Task<ChangePasswordResponseDTO?> CurrentUserAsync(int userId);

}
