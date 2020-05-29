using FeedHunter.API.Model;
using System.Collections.Generic;

namespace FeedHunter.API.Data
{
    public interface IArticleRepository : IRepository
    {
        void UpdateArticleList(List<Article> articles);
    }
}