using FeedHunter.API.Helper;
using FeedHunter.API.Model;
using FeedHunter.API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace FeedHunter.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedController : ControllerBase
    {
        private readonly IFeedService feedService;
        private readonly IOptions<AppSettings> appSettings;

        public FeedController(IFeedService feedService, IOptions<AppSettings> appSettings)
        {
            this.feedService = feedService;
            this.appSettings = appSettings;
        }

        [HttpGet("articles")]
        public async Task<IActionResult> GetArticles([FromQuery] int pageNumber, [FromQuery] ArticlesOptions articlesOptions)
        {
            var articles = await feedService.GetArticles(pageNumber == 0 ? 1 : pageNumber, appSettings.Value.PageSize, articlesOptions);

            return Ok(articles);
        }

        [HttpGet("channels")]
        public async Task<IActionResult> GetChannels()
        {
            var channels = await feedService.GetSources();
            return Ok(channels);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddChannel([FromBody] string url)
        {
            return Ok(await feedService.AddSource(url));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteChannel(int id)
        {
            await feedService.DeleteSource(id);
            return NoContent();
        }
    }
}