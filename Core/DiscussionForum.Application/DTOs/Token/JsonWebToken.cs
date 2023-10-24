namespace DiscussionForum.Application.DTOs.Tokens;

public class JsonWebToken
{
    public string AccessToken { get; set; }
    public DateTime Expiration { get; set; }
    public string RefreshToken { get; set; }
}