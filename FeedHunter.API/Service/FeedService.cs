﻿using CodeHollow.FeedReader;
using FeedHunter.API.Data;
using FeedHunter.API.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedHunter.API.Service
{
    public class FeedService : IFeedService
    {
        private readonly IRepository repository;
        private readonly IArticleRepository articleRepository;

        public FeedService(IRepository repository, IArticleRepository articleRepository)
        {
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
                Articles = articles.ToList()
            };
        }

        public async Task<bool> AddSource(string url)
        {
            var feed = await FeedReader.ReadAsync(url);

            repository.Add(new FeedSource { Url = url, Name = feed.Title });

            return await repository.SaveAll();
        }

        public async Task<bool> DeleteSource(int id)
        {
            var source = (await repository.Get<FeedSource>(new List<int> { id })).First();

            repository.Delete(source);

            return await repository.SaveAll();
        }
    }
}