using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeedHunter.API.Data
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<List<T>> GetAll<T>() where T : class;
        Task<List<T>> Get<T>(List<int> ids) where T : class, IHasId;
        Task<bool> SaveAll();
    }
}