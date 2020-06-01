using AutoMapper;
using FeedHunter.API.Data;
using FeedHunter.API.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedHunter.API.Service
{
    public class FeedService : IFeedService
    {
        private readonly IMapper mapper;
        private readonly IRepository repository;
        private readonly IArticleRepository articleRepository;

        public FeedService(IMapper mapper, IRepository repository, IArticleRepository articleRepository)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.articleRepository = articleRepository;
        }

        public async Task<List<FeedSource>> GetSources()
        {
            return await repository.GetAll<FeedSource>();
        }

        public async Task<ArticlePageList> GetArticles(int pageNumber, int pageSize, ArticlesOptions articlesOptions = null)
        {
            var articles = await articleRepository.GetFilteredListPage(pageSize, pageNumber, articlesOptions.SourceIds);

            return new ArticlePageList
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalItems = await articleRepository.Count(articlesOptions.SourceIds),
                Articles = articles.OrderByDescending(x => x.PublishingDate)
                    .ToList()
            };
        }

        private async Task UpdateFeedSourceName(FeedSource feed, string name)
        {
            if (feed.Name != name)
            {
                feed.Name = name;
                await repository.SaveAll();
            }
        }
    }
}