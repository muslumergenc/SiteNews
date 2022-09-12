using Microsoft.EntityFrameworkCore;
using SiteNews.Data.Abstract;
using SiteNews.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteNews.Data.Concrete.EfCore
{
    public class HaberRepository : GenericRepository<Haber>, IHaberRepository
    {
        public HaberRepository(CoreContext context) : base(context)
        {
        }
        private CoreContext CoreContext
        {
            get { return context as CoreContext; }
        }
        public async Task<Haber> GetByUrl(string url)
        {
            return await CoreContext.Habers.Where(x => x.Url == url).FirstOrDefaultAsync();
        }

        public async Task<List<Haber>> ListById()
        {
            return await CoreContext.Habers.Where(x => x.MakaleMi == false).Include(x => x.Kategoris).ToListAsync();
        }
       
        public async Task<List<Haber>> ListByMakaleId()
        {
            return await CoreContext.Habers.Where(x => x.MakaleMi == true).Include(x=> x.Yazars).OrderByDescending(x => x.Id).ToListAsync();
        }
      
        public async Task<List<Haber>> ListByKategori(int katId)
        {
           return await CoreContext.Habers.Where(x => x.KategoriId == katId).ToListAsync();
        }

        public async Task<List<Haber>> ListByMakale()
        {
            return await CoreContext.Habers.Where(x => x.MakaleMi == true).ToListAsync();
        }

        public async Task<List<Haber>> ListByOkuma()
        {
            return await CoreContext.Habers.Where(x=> x.MakaleMi==false).Include(x=> x.Kategoris).OrderByDescending(x => x.Okunma).ToListAsync();
        }

        public async Task<List<Haber>> ListByYazar(int yazarId)
        {
            return await CoreContext.Habers.Where(x => x.YazarId == yazarId).Include(x=> x.Yazars).OrderByDescending(x=> x.Id).ToListAsync();
        }

        public async Task<List<Haber>> ListByMakaleOkuma()
        {
            return await CoreContext.Habers.Where(x=> x.MakaleMi==true).Include(x=> x.Yazars).OrderByDescending(x => x.Okunma).ToListAsync();
        }
    }
}
