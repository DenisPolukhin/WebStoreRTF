using AutoMapper;
using WebStore.Database.Models.Entities;
using WebStore.Logic.Models.Category;

namespace WebStore.Logic.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryModel>();
    }
}