using FeedHunter.API.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeedHunter.API.Service
{
    public interface IFeedService
    {
        Task<ArticlePageList> GetArticles(int pageNumber, int pageSize, ArticlesOptions articlesOptions = null);

        Task<List<FeedSource>> GetSources();
    }
}