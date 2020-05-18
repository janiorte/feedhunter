using AutoMapper;
using CodeHollow.FeedReader;
using FeedHunter.API.Model;

namespace FeedHunter.API.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<FeedItem, Article>();
        }
    }
}
