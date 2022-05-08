using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Common.Models;
using WebStore.Logic.Interfaces;
using WebStore.Logic.Models.Product;

namespace WebStore.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductsService _productsService;

    public ProductsController(IProductsService productsService)
    {
        _productsService = productsService;
    }

    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetDetails(Guid id)
    {
        var result = await _productsService.GetDetailsAsync(id);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("category/{categoryId:guid}")]
    public async Task<IActionResult> GetCategoryProducts(Guid categoryId, [FromQuery] PageModel pageModel)
    {
        var result = await _productsService.GetCategoryProductsAsync(categoryId, pageModel);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("categories")]
    public async Task<IActionResult> GetCategories()
    {
        var result = await _productsService.GetCategoriesAsync();
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("search/{title}")]
    public async Task<IActionResult> SearchByTitle(string title, [FromQuery] PageModel pageModel)
    {
        var result = await _productsService.SearchByTitleAsync(title, pageModel);
        return Ok(result);
    }

    [Authorize(Policy = "Administrator")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateProductModel createProductModel)
    {
        await _productsService.CreateAsync(createProductModel);
        return NoContent();
    }

    [Authorize(Policy = "Administrator")]
    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateProductModel updateProductModel)
    {
        await _productsService.UpdateAsync(id, updateProductModel);
        return NoContent();
    }
}