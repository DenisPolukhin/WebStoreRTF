using AutoMapper;
using WebStore.Database.Models.Entities;
using WebStore.Logic.Models.Product;

namespace WebStore.Logic.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductModel>();
        CreateMap<CreateProductModel, Product>();
    }
}