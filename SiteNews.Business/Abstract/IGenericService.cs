using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteNews.Business.Abstract
{
    public interface IGenericService<T> where T : class
    {
        Task<T> GetById(int id);
        Task<List<T>> GetAll();
        Task<T> CreateAsync(T entity);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
