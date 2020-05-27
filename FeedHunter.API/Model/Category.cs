using System.Collections.Generic;

namespace FeedHunter.API.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ArticleCategory> Articles { get; set; }
    }
}
