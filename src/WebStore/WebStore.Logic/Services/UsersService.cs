using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using WebStore.Common.Models;
using WebStore.Database.Models;
using WebStore.Database.Models.Entities;
using WebStore.Database.Models.Enums;
using WebStore.Logic.Interfaces;
using WebStore.Logic.Models.User;

namespace WebStore.Logic.Services;

public class UsersService : IUsersService
{
    private readonly IDbContextFactory<DatabaseContext> _dbContextFactory;
    private readonly IJwtService _jwtService;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public UsersService(IDbContextFactory<DatabaseContext> dbContextFactory, IJwtService jwtService, UserManager<User> userManager,
        SignInManager<User> signInManager)
    {
        _dbContextFactory = dbContextFactory;
        _jwtService = jwtService;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<LogInResultModel> LogInAsync(LogInModel logInModel)
    {
        var databaseContext = await _dbContextFactory.CreateDbContextAsync();
        const string InvalidLoginOrPasswordMessage = "You entered an incorrect username or password!";

        var user = await _userManager.FindByEmailAsync(logInModel.Email);
        if (user is null)
        {
            return new LogInResultModel(false, InvalidLoginOrPasswordMessage);
        }

        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, logInModel.Password, false);
        if (!signInResult.Succeeded)
        {
            return new LogInResultModel(false, InvalidLoginOrPasswordMessage);
        }

        user.LastSeenAt = SystemClock.Instance.GetCurrentInstant();
        await databaseContext.SaveChangesAsync();

        return await GenerateSuccessfulLogInResultModelAsync(user);
    }

    public async Task<ResultModel> SignUpAsync(SignUpModel signUpModel)
    {
        var user = await _userManager.FindByEmailAsync(signUpModel.Email);
        if (user is not null)
        {
            return new ResultModel(false, "User with this email already exists");
        }

        user = new User
        {
            UserName = signUpModel.Email,
            Email = signUpModel.Email
        };

        var result = await _userManager.CreateAsync(user, signUpModel.Password);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Aggregate("", (current, e) => current + e.Description + "\n");
            return new ResultModel(false, errors);
        }

        return new ResultModel(true, "Successfully");
    }

    private async Task<LogInResultModel> GenerateSuccessfulLogInResultModelAsync(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        if (await _userManager.IsInRoleAsync(user, nameof(Role.Administrator)))
        {
            claims.Add(new Claim(ClaimTypes.Role, nameof(Role.Administrator)));
        }

        var accessToken = _jwtService.GenerateAccessToken(claims);

        return new LogInResultModel(true, "Successfully", accessToken);
    }
}