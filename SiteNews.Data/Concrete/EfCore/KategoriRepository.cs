using Microsoft.EntityFrameworkCore;
using SiteNews.Data.Abstract;
using SiteNews.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteNews.Data.Concrete.EfCore
{
    public class KategoriRepository : GenericRepository<Kategori>, IKategoriRepository
    {
        public KategoriRepository(CoreContext context) : base(context)
        {
        }
        private CoreContext CoreContext
        {
            get { return context as CoreContext; }
        }
        public async Task<List<Kategori>> GetBySira()
        {
            return await CoreContext.Kategoris.OrderBy(x => x.Sira).ToListAsync();
        }
    }
}