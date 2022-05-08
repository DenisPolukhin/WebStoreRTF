namespace WebStore.Logic.Models.UserProfile;

public class UpdateProfileModel
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
    public Guid? CityId { get; set; }
    public DateTime? BirthDate { get; set; }
}