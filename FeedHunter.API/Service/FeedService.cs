using AutoMapper;
using CodeHollow.FeedReader;
using FeedHunter.API.Data;
using FeedHunter.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedHunter.API.Service
{
    public class FeedService : IFeedService
    {
        private readonly IMapper mapper;
        private readonly IRepository repository;

        public FeedService(IMapper mapper, IRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<List<FeedSource>> GetSources()
        {
            return await repository.GetAll<FeedSource>();
        }

        public async Task<ArticlePageList> GetArticles(int pageNumber, int pageSize, ArticlesOptions articlesOptions = null)
        {
            if (articlesOptions?.SourceIds == null)
                return new ArticlePageList();

            var articles = new List<Article>();
            var sources = await repository.Get<FeedSource>(articlesOptions.SourceIds);

            foreach (var feed in sources)
            {
                var fr = await FeedReader.ReadAsync(feed.Url);

                articles.AddRange(mapper.Map<ICollection<FeedItem>, List<Article>>(fr.Items,
                opts =>
                {
                    opts.AfterMap((_, dest) =>
                    {
                        foreach (var article in dest)
                        {
                            article.Channel = feed;
                        }
                    });
                }));

                await UpdateFeedSourceName(feed, fr.Title);
            }

            return new ArticlePageList
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalItems = articles.Count,
                Articles = articles.OrderByDescending(x => x.PublishingDate)
                    .Skip(Math.Max(pageNumber - 1, 0) * pageSize)
                    .Take(pageSize)
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