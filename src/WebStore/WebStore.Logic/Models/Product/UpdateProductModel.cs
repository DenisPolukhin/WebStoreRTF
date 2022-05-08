namespace WebStore.Logic.Models.Product;

public record UpdateProductModel(string Title, string Description, decimal Price,
    Guid ManufacturerId, Guid CategoryId);