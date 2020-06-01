using FeedHunter.API.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedHunter.API.Data
{
    public class ArticleRepository : Repository, IArticleRepository
    {
        public ArticleRepository(DataContext context) : base(context)
        {
        }

        public async Task<int> Count(List<int> sourceIds = null)
        {
            return await context.Set<Article>()
                .CountAsync(art => sourceIds == null || sourceIds.Contains(art.Channel.Id));
        }

        public async Task<List<Article>> GetFilteredListPage(int pageSize, int pageNumber, List<int> sourceIds = null)
        {
            return await context.Set<Article>()
                .Include(x => x.Channel)
                .Where(art => sourceIds == null || sourceIds.Contains(art.Channel.Id))
                .OrderByDescending(art => art.PublishingDate)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize).ToListAsync();
        }

        public async void UpdateArticleList(List<Article> articles)
        {
            var articlesLinks = articles.Select(x => x.Link).ToList();

            var existingLinks = await context.Set<Article>()
                .Where(x => articlesLinks.Contains(x.Link)).Select(x => x.Link).ToListAsync();

            var articlesToAdd = articles.Where(x => !existingLinks.Contains(x.Link));

            if (articlesToAdd.Any())
            {
                await context.AddRangeAsync(articlesToAdd);
                await SaveAll();
            }
        }
    }
}