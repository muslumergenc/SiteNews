using Microsoft.EntityFrameworkCore;
using SiteNews.Data.Abstract;
using SiteNews.Entity;

namespace SiteNews.Data.Concrete.EfCore
{
    public class YazarRepository : GenericRepository<Yazar>, IYazarRepository
    {
        public YazarRepository(DbContext context) : base(context)
        {
        }
    }
}
