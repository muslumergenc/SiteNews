using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SiteNews.Business.Abstract;
using SiteNews.Entity;
using SiteNews.Web.Extensions;
using SiteNews.Web.Models;
using System.Threading.Tasks;

namespace Core.Web.Controllers
{
    [Authorize]
    public class KategoriController : Controller
    {
        readonly private IKategoriService _kategoriService;
        public KategoriController(IKategoriService kategoriService)
        {
            _kategoriService = kategoriService;
        }
        [Route("admin/kategoriler")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var kategoriler = await _kategoriService.GetBySira();
                return View(kategoriler);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Kategori kategori)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                Kategori entity = new Kategori()
                {
                    Ad = kategori.Ad,
                    Sira = kategori.Sira,
                    Url = kategori.Url,
                    SeoDesc = kategori.SeoDesc
                };
                await _kategoriService.CreateAsync(entity);
                TempData.Put("message", new AlertMessage()
                {
                    Title = "Kategori eklendi.",
                    Message = "",
                    AlertType = "success"
                });

                return Redirect("/admin/kategoriler");

            }
            catch
            {
                return View();
            }
        }
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                if (id == 0)
                {
                    return NotFound();
                }
                var kategori = await _kategoriService.GetById(id);
                if (kategori == null)
                {
                    return NotFound();
                }
                return View(kategori);

            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Kategori kategori)
        {
            try
            {
                if (id != kategori.Id)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return View();
                }
                try
                {
                    var entity = await _kategoriService.GetById(id);
                    if (entity == null)
                    {
                        return NotFound();
                    }
                    entity.Ad = kategori.Ad;
                    entity.Sira = kategori.Sira;
                    entity.Url = kategori.Url;
                    entity.SeoDesc = kategori.SeoDesc;
                    _kategoriService.Update(entity);
                    var msg = new AlertMessage()
                    {
                        Message = "Kategori güncellendi.",
                        AlertType = "success"
                    };
                    TempData["message"] = JsonConvert.SerializeObject(msg);
                    return Redirect("/admin/kategoriler");
                }
                catch (System.Exception)
                {

                    throw;
                }
            }
            catch
            {
                return View();
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    return NotFound();
                }
                var kategori = await _kategoriService.GetById(id);
                return kategori == null ? NotFound() : View(kategori);

            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var kategori = await _kategoriService.GetById(id);
                if (kategori != null)
                {
                    _kategoriService.Delete(kategori);
                    return Redirect("/admin/kategoriler");
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
        }
    }
}