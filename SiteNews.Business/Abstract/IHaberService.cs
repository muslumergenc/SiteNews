using SiteNews.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteNews.Business.Abstract
{
    public interface IHaberService: IGenericService<Haber>
    {
        Task<Haber> GetByUrl(string url);
        Task<List<Haber>> ListById();
        Task<List<Haber>> ListByMakaleId();
        Task<List<Haber>> ListByOkuma();
        Task<List<Haber>> ListByMakaleOkuma();
        Task<List<Haber>> ListByKategori(int katId);
        Task<List<Haber>> ListByYazar(int yazarId);
        Task<List<Haber>> ListByMakale();
    }
}
