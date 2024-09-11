using ServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Contracts;
public interface IProfileService
{
    Task ChangePasswordAsync(ChangePasswordRequestDTO request);
    Task<(bool success, ChangePasswordResponseDTO? userProfile)> ProfileDetailAsync(string userId);

}
