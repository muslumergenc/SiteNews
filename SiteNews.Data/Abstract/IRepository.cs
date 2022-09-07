using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteNews.Data.Abstract
{
    public interface IRepository<T>
    {
        Task<T> GetById(int id);
        Task<List<T>> GetAll();
        Task CreateAsync(T entity);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
