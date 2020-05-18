using System.Collections.Generic;

namespace FeedHunter.API.Model
{
    public class ArticlePageList
    {
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
        public List<Article> Articles { get; set; }
    }
}
