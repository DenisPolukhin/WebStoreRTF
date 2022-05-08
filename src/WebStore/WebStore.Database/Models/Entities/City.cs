namespace WebStore.Database.Models.Entities;

public class City
{
    public Guid Id { get; set; }
    public string Address { get; set; } = default!;
    public string? PostalCode { get; set; }
    public string Country { get; set; } = default!;
    public string FederalDistrict { get; set; } = default!;
    public string RegionType { get; set; } = default!;
    public string Region { get; set; } = default!;
    public string? AreaType { get; set; }
    public string? Area { get; set; }
    public string? Type { get; set; }
    public string? Name { get; set; }
    public string? SettlementType { get; set; }
    public string? Settlement { get; set; }
    public string KladrId { get; set; } = default!;
    public string FiasLevel { get; set; } = default!;
    public string CapitalMarker { get; set; } = default!;
    public string Okato { get; set; } = default!;
    public string Oktmo { get; set; } = default!;
    public string TaxOffice { get; set; } = default!;
    public string Timezone { get; set; } = default!;
    public string GeoLat { get; set; } = default!;
    public string GeoLon { get; set; } = default!;
    public long Population { get; set; }
    public string FoundationYear { get; set; } = default!;
}