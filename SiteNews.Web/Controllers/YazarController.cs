using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SiteNews.Business.Abstract;
using SiteNews.Entity;
using SiteNews.Web.Extensions;
using SiteNews.Web.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Core.Web.Controllers
{
    [Authorize]
    public class YazarController : Controller
    {
        readonly private IYazarService _yazarService;
        public YazarController(IYazarService yazarService)
        {
            _yazarService = yazarService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                List<Yazar> yazarlar = await _yazarService.GetAll();
                return View(yazarlar);
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
        public async Task<IActionResult> Create(Yazar yazars, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        Yazar entity = new Yazar()
                        {
                            Ad = yazars.Ad,
                            Foto = yazars.Foto,
                            Tarih = yazars.Tarih
                        };
                        var extension = Path.GetExtension(file.FileName);
                        var randomName = string.Format($"{entity.Ad.ToLower()}{extension}");
                        entity.Foto = randomName;
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\yazarlar", randomName);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        await _yazarService.CreateAsync(entity);
                        TempData.Put("message", new AlertMessage()
                        {
                            Title = "Yazar eklendi.",
                            Message = "",
                            AlertType = "success"
                        });
                        return Redirect("/yazar");
                    }
                    else
                    {
                        Yazar entity = new Yazar()
                        {
                            Ad = yazars.Ad,
                            Foto = "resimyok.jpg",
                            Tarih = yazars.Tarih
                        };
                        await _yazarService.CreateAsync(entity);
                        TempData.Put("message", new AlertMessage()
                        {
                            Title = "Yazar eklendi.",
                            Message = "",
                            AlertType = "success"
                        });
                        return Redirect("/yazar");
                    }
                }
                return View();
            }
            catch (System.Exception)
            {

                throw;
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
                var yazar = await _yazarService.GetById(id);
                return yazar == null ? NotFound() : (IActionResult)View(yazar);

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
                var yazar = await _yazarService.GetById(id);
                if (yazar != null)
                {
                    if (yazar.Foto!="resimyok.jpg")
                    {
                        var yol = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\yazarlar\\" + yazar.Foto);
                        if (System.IO.File.Exists(yol))
                        {
                            System.IO.File.Delete(yol);
                        }
                    }
                    _yazarService.Delete(yazar);
                    return Redirect("/yazar");
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (System.Exception)
            {

                throw;
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
                var yazar = await _yazarService.GetById(id);
                if (yazar == null)
                {
                    return NotFound();
                }
                return View(yazar);

            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Yazar yazar, IFormFile file)
        {
            try
            {
                if (id != yazar.Id)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    try
                    {
                        var entity = await _yazarService.GetById(id);
                        if (entity == null)
                        {
                            return NotFound();
                        }
                        if (file != null)
                        {
                            var path1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\yazarlar" + entity.Foto);
                            if (System.IO.File.Exists(path1))
                            {
                                System.IO.File.Delete(path1);
                            }
                            var extension = Path.GetExtension(file.FileName);
                            entity.Foto = string.Format($"{entity.Ad.ToLower()}{extension}");
                            var path2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\yazarlar", entity.Foto);
                            using var stream = new FileStream(path2, FileMode.Create);
                            await file.CopyToAsync(stream);
                        }
                        entity.Ad = yazar.Ad;
                        entity.Tarih = yazar.Tarih;
                        _yazarService.Update(entity);
                        var msg = new AlertMessage()
                        {
                            Message = "Yazar güncellendi.",
                            AlertType = "success"
                        };
                        TempData["message"] = JsonConvert.SerializeObject(msg);
                        return Redirect("/yazar");
                    }
                    catch (System.Exception)
                    {

                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
