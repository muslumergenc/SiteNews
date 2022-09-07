using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteNews.Data.Concrete;
using System.Threading.Tasks;

namespace SiteNews.Web.ViewComponents
{
    public class HeaderNavViewComponent:ViewComponent
    {
        private CoreContext _context;

        public HeaderNavViewComponent(CoreContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync() 
        {
            return View(await _context.Kategoris.ToListAsync());
        }
    }
}
