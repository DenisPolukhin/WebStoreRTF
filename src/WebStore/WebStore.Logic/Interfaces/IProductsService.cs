using WebStore.Common.Models;
using WebStore.Logic.Models.Category;
using WebStore.Logic.Models.Product;

namespace WebStore.Logic.Interfaces;

public interface IProductsService
{
    Task<ProductModel> GetDetailsAsync(Guid id);
    Task<IPaginatedList<ProductModel>> GetCategoryProductsAsync(Guid categoryId, PageModel pageModel);
    Task<IEnumerable<CategoryModel>> GetCategoriesAsync();
    Task<IPaginatedList<ProductModel>> SearchByTitleAsync(string title, PageModel pageModel);
    Task CreateAsync(CreateProductModel createProductModel);
    Task UpdateAsync(Guid id, UpdateProductModel updateProductModel);
}