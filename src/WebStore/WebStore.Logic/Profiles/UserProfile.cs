using AutoMapper;
using WebStore.Common.ValueConverters;
using WebStore.Database.Models.Entities;
using WebStore.Logic.Models.UserProfile;

namespace WebStore.Logic.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UpdateProfileModel, User>()
            .ForMember(x => x.BirthDate, opt => opt.ConvertUsing(new DateTimeToLocalDateConverter()));
        CreateMap<User, UserModel>()
            .ForMember(x => x.BirthDate, opt => opt.ConvertUsing(new LocalDateToDateTimeConverter()));
    }
}