using Microsoft.EntityFrameworkCore;
using SiteNews.Data.Abstract;
using SiteNews.Entity;

namespace SiteNews.Data.Concrete.EfCore
{
    public class SosyalMedyaRepository : GenericRepository<SosyalMedya>, ISosyalMedyaRepository
    {
        public SosyalMedyaRepository(DbContext context) : base(context)
        {

        }
    }
}
