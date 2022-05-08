using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebStore.Common.Exceptions;
using WebStore.Common.Models;
using WebStore.Database.Models;
using WebStore.Logic.Interfaces;
using WebStore.Logic.Models.UserProfile;

namespace WebStore.Logic.Services;

public class UsersProfileService : IUsersProfileService
{
    private readonly DatabaseContext _databaseContext;
    private readonly IMapper _mapper;

    public UsersProfileService(DatabaseContext databaseContext, IMapper mapper)
    {
        _databaseContext = databaseContext;
        _mapper = mapper;
    }

    public async Task<UserModel> GetDetailsAsync(Guid id)
    {
        var user = await _databaseContext.Users
            .Include(x => x.City)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (user is null)
        {
            throw new EntityFindException();
        }

        return _mapper.Map<UserModel>(user);
    }

    public async Task<ResultModel> UpdateAsync(Guid id, UpdateProfileModel updateProfileModel)
    {
        var user = await _databaseContext.Users.FindAsync(id);
        if (user is null)
        {
            throw new EntityFindException();
        }

        if (updateProfileModel.CityId is not null && updateProfileModel.CityId != user.CityId)
        {
            var _ = await _databaseContext.Cities.FindAsync(updateProfileModel.CityId) ??
                    throw new EntityFindException();
        }

        _mapper.Map(updateProfileModel, user);
        await _databaseContext.SaveChangesAsync();

        return new ResultModel(true, "Data updated successfully!");
    }
}