using System;
using System.Collections.Generic;

namespace FeedHunter.API.Model
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }

        public string Author { get; set; }
        public ICollection<ArticleCategory> Categories { get; set; }
        public DateTime? PublishingDate { get; set; }
        public FeedSource Channel { get; set; }
    }
}