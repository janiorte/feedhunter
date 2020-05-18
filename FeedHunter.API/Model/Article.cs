using System;

namespace FeedHunter.API.Model
{
    public class Article
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }

        public string Author { get; set; }
        public string[] Categories { get; set; }
        public DateTime? PublishingDate { get; set; }
        public FeedSource Channel { get; set; }
    }
}