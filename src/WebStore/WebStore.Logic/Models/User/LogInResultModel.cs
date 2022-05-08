namespace WebStore.Logic.Models.User;

public record LogInResultModel(bool Success, string Message, string? AccessToken = default);