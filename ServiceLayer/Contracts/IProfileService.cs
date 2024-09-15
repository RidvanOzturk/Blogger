using DataLayer.Entities;
using ServiceLayer.DTOs;

namespace ServiceLayer.Contracts;
public interface IProfileService
{
    Task<(bool Success, ChangePasswordResponseDTO? UserProfile, User? User)> ProfileDetailAsync(int userId);
    Task ChangePasswordAsync(ChangePasswordRequestDTO request);
    Task<ChangePasswordResponseDTO?> CurrentUserAsync(int userId);
    Task<User?> GetUserByIdAsync(int userId);

}
