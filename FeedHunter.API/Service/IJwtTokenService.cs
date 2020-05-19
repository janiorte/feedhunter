using FeedHunter.API.Model;

namespace FeedHunter.API.Service
{
    public interface IJwtTokenService
    {
        string GenerateJwtToken(User user);
    }
}