using FeedHunter.API.Model;
using System.Collections.Generic;

namespace FeedHunter.API.Service
{
    public interface IJwtTokenService
    {
        string GenerateJwtToken(User user, IList<string> roles);
    }
}