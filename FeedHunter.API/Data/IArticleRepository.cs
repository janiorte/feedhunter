using FeedHunter.API.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeedHunter.API.Data
{
    public interface IArticleRepository : IRepository
    {
        Task<int> Count(List<int> sourceIds = null);

        Task<List<Article>> GetFilteredListPage(int pageSize, int pageNumber, List<int> sourceIds = null);

        void UpdateArticleList(List<Article> articles);
    }
}