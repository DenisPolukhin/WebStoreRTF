﻿namespace WebStore.Logic.Models.Product;

public class ProductModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public string ManufacturerName { get; set; } = default!;
    public string CategoryName { get; set; } = default!;
}