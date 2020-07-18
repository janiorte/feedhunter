using FeedHunter.API.Model;
using FeedHunter.API.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;

namespace FeedHunter.API.Data
{
    public static class Seed
    {
        private static readonly List<string> feedSources = new List<string>
        {
            "https://es.gizmodo.com/rss",
            "https://www.elespanol.com/rss/omicrono/",
            "https://feed.hipertextual.com/",
            "https://www.investigacionyciencia.es/rss/noticias",
            "http://www.eljueves.es/feeds/rss.html",
            "http://www.bbc.co.uk/mundo/temas/ciencia/index.xml",
            "https://www.bbc.com/mundo/temas/tecnologia/index.xml",
            "https://es.digitaltrends.com/computadoras/feed/",
            "https://www.agenciasinc.es/feed/Tierra",
            "http://feeds.feedburner.com/naukas/francis"
        };

        public static void SeedFeedSources(IRepository repository, IFeedService feedService)
        {
            if (repository.GetAll<FeedSource>().Result.Count == 0)
            {
                foreach (var url in feedSources)
                {
                    feedService.AddSource(url).Wait();
                }
            }
        }

        public static void SeedUsers(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            if (!userManager.Users.Any())
            {
                CreateRoles(roleManager);
                CreateUser("admin", "P@55w0rd", "Admin", userManager);
            }
        }

        private static void CreateRoles(RoleManager<Role> roleManager)
        {
            var roles = new List<Role>
            {
                new Role{ Name = "Admin" },
                new Role{ Name = "Member" }
            };

            foreach (var role in roles)
            {
                roleManager.CreateAsync(role).Wait();
            }
        }

        private static void CreateUser(string username, string password, string role,
            UserManager<User> userManager)
        {
            var user = new User { UserName = username };

            userManager.CreateAsync(user, password).Wait();
            userManager.AddToRoleAsync(user, role);
        }
    }
}