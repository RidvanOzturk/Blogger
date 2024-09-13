using Blogger.Models.Requests;
using DataLayer.Entities;

namespace Blogger.Models.Responses;

public class UserProfileResponseModel
{
    public User User { get; set; }
    public ChangePasswordRequestModel ChangePasswordModel { get; set; }
}
