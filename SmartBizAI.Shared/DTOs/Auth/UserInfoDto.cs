namespace SmartBizAI.Shared.DTOs.Auth;

public sealed class UserInfoDto
{
    public string UserId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}
