namespace WebStore.Database.Models.Entities;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public Guid? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }
    public ICollection<Category> ChildCategories { get; set; } = default!;
    public ICollection<Product> Products { get; set; } = default!;
}