using SiteNews.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteNews.Data.Abstract
{
    public interface IKategoriRepository:IRepository<Kategori>
    {
        Task<List<Kategori>> GetBySira();
    }
}