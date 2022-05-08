using WebStore.Common.Models;
using WebStore.Logic.Models.User;

namespace WebStore.Logic.Interfaces;

public interface IUsersService
{
    Task<ResultModel> SignUpAsync(SignUpModel signUpModel);
    Task<LogInResultModel> LogInAsync(LogInModel logInModel);
}