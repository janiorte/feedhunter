using FeedHunter.API.Data;
using System.ComponentModel.DataAnnotations;

namespace FeedHunter.API.Model
{
    public class FeedSource : IHasId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string Url { get; set; }
    }
}
