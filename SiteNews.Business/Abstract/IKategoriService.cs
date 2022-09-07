using SiteNews.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteNews.Business.Abstract
{
    public interface IKategoriService:IGenericService<Kategori>
    {
        Task<List<Kategori>> GetBySira();
    }
}
