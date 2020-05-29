using FeedHunter.API.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FeedHunter.API.Data
{
    public class ArticleRepository : Repository, IArticleRepository
    {
        public ArticleRepository(DataContext context) : base(context)
        {
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