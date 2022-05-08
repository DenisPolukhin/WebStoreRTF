using Microsoft.AspNetCore.Identity;
using NodaTime;

namespace WebStore.Database.Models.Entities;

public class User : IdentityUser<Guid>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
    public Guid? CityId { get; set; }
    public City? City { get; set; }
    public LocalDate? BirthDate { get; set; }
    public Instant CreatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();
    public Instant LastSeenAt { get; set; } = SystemClock.Instance.GetCurrentInstant();
    public ICollection<Order> Orders { get; set; } = default!;
}