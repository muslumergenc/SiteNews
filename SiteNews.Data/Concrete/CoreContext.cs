using Microsoft.EntityFrameworkCore;
using SiteNews.Entity;

namespace SiteNews.Data.Concrete
{
    public class CoreContext : DbContext
    {
        public CoreContext(DbContextOptions<CoreContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);
        }

        public DbSet<Video> Videos { get; set; }
        public DbSet<Haber> Habers { get; set; }
        public DbSet<Kategori> Kategoris { get; set; }
        public DbSet<SosyalMedya> SosyalMedyas { get; set; }
        public DbSet<Yazar> Yazars { get; set; }
    }
}
