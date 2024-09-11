using ServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Contracts;
public class IProfileService
{
    Task ChangePasswordAsync(ChangePasswordRequestDTO requestDTO);
    Task<bool> ProfileDetailAsync(ChangePasswordResponseDTO responseDTO);
}
