namespace WebStore.Logic.Models.UserProfile;

public class UserModel
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
    public string? CityName { get; set; }
    public DateTime? BirthDate { get; set; }
}