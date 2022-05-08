namespace WebStore.Logic.Models.Category;

public class CategoryModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public IEnumerable<CategoryModel> ChildCategories { get; set; } = default!;
}