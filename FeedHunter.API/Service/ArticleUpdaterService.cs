using AutoMapper;
using CodeHollow.FeedReader;
using FeedHunter.API.Data;
using FeedHunter.API.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FeedHunter.API.Service
{
    public class ArticleUpdaterService : IHostedService
    {
        private readonly IMapper mapper;
        private readonly IServiceScopeFactory scopeFactory;
        private Timer timer;

        public ArticleUpdaterService(IMapper mapper, IServiceScopeFactory scopeFactory)
        {
            this.mapper = mapper;
            this.scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(UpdateArticles, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private void UpdateArticles(object state)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var repository = scope.ServiceProvider.GetService<IArticleRepository>();
                var sources = repository.GetAll<FeedSource>().Result;
                var articles = new List<Article>();

                foreach (var feed in sources)
                {
                    var fr = FeedReader.ReadAsync(feed.Url).Result;

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
                }

                repository.UpdateArticleList(articles);
            }
        }
    }
}