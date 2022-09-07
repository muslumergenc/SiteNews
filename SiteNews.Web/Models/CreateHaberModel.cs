using SiteNews.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteNews.Web.Models
{
    public class CreateHaberModel
    {
        public Task<List<Kategori>> Kategoriler { get; set; }
        public Haber Haber { get; set; }
    }
}
