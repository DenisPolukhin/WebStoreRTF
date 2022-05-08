using WebStore.Common.Models;
using WebStore.Logic.Models.UserProfile;

namespace WebStore.Logic.Interfaces;

public interface IUsersProfileService
{
    Task<UserModel> GetDetailsAsync(Guid id);
    Task<ResultModel> UpdateAsync(Guid id, UpdateProfileModel updateProfileModel);
}