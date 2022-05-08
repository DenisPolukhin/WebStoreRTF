namespace WebStore.Database.Models.Entities;

public class Manufacturer
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public ICollection<Product> Products { get; set; } = default!;
}