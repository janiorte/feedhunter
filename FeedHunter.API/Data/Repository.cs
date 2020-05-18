using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedHunter.API.Data
{
    public class Repository : IRepository
    {
        private readonly DataContext context;

        public Repository(DataContext context)
        {
            this.context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            context.Remove(entity);
        }

        public async Task<List<T>> GetAll<T>() where T : class
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<List<T>> Get<T>(List<int> ids) where T : class, IHasId
        {
            return await context.Set<T>().Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}