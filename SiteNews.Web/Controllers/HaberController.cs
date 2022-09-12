using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SiteNews.Business.Abstract;
using SiteNews.Entity;
using SiteNews.Web.Extensions;
using SiteNews.Web.Models;
using SiteNews.Web.Models.Images;
using SiteNews.Web.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Web.Controllers
{
    [Authorize]
    public class HaberController : Controller
    {
        readonly private IHaberService _haberService;
        readonly private IKategoriService _kategoriService;
        private readonly IImageService imageService;
        public HaberController(IHaberService haberService, IKategoriService kategoriService, IImageService imageService)
        {
            _haberService = haberService;
            _kategoriService = kategoriService;
            this.imageService = imageService;
        }
        public async Task<IActionResult> Index(string q)
        {
            try
            {
                if (q == "read")
                {
                    Task<List<Haber>> haberler = _haberService.ListByOkuma();
                    
                    return View(await haberler);
                }
                else
                {
                    Task<List<Haber>> haberler = _haberService.ListById();
                    return View(await haberler);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IActionResult> Create()
        {
            try
            {
                var kategoriler = await _kategoriService.GetAll();
                ViewBag.Kategoriler = new SelectList(kategoriler, "Id", "Ad");
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestFormLimits(MultipartBodyLengthLimit = 104857600)]
        public async Task<IActionResult> Create(Haber haber, IFormFile[] files)
        {
            try
            {
                List<Kategori> kategoriler = await _kategoriService.GetAll();
                ViewBag.Kategoriler = new SelectList(kategoriler, "Id", "Ad");
                if (files.Count() != 0)
                {
                    Haber entity = new Haber()
                    {
                        Baslik = haber.Baslik,
                        Detay = haber.Detay,
                        EditorMu = haber.EditorMu,
                        EkMansetMi = haber.EkMansetMi,
                        KategoriId = haber.KategoriId,
                        KisaAciklama = haber.KisaAciklama,
                        MansetMi = haber.MansetMi,
                        Okunma = 0,
                        PopulerMi = haber.PopulerMi,
                        SonHaberMi = haber.SonHaberMi,
                        SeoDesc = haber.SeoDesc,
                        Tarih = Convert.ToDateTime(DateTime.Now.ToShortDateString()),
                        Url = haber.Url,
                        MakaleMi = false
                    };
                    string randomName = string.Format($"{entity.Baslik.ToLower() + ".jpg"}");
                    entity.Foto = randomName;
                    imageService.Process(files.Select(i => new ImageInputModel
                    {
                        FileName = randomName,
                        Type = i.ContentType,
                        Content = i.OpenReadStream()
                    }));
                    await _haberService.CreateAsync(entity);
                    TempData.Put("message", new AlertMessage()
                    {
                        Title = "Haber eklendi.",
                        Message = "",
                        AlertType = "success"
                    });
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                var msg = new AlertMessage()
                {
                    Message = "Hata!",
                    AlertType = "danger"
                };
                TempData["message"] = JsonConvert.SerializeObject(msg);
                return View();
            }
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                if (id == 0)
                {
                    return NotFound();
                }
                var haber = await _haberService.GetById(id);
                List<SelectListItem> kategoriler = (from k in await _kategoriService.GetBySira()
                                                    select new SelectListItem
                                                    {
                                                        Text = k.Ad,
                                                        Value = k.Id.ToString()
                                                    }).ToList();
                ViewBag.Kategoriler = kategoriler;
                return haber == null ? NotFound() : View(haber);

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 104857600)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Haber haber, IFormFile[] files, int KategoriId)
        {
            try
            {
                if (id != haber.Id)
                {
                    return NotFound();
                }
                Haber entity = await _haberService.GetById(id);
                if (entity == null)
                {
                    return NotFound();
                }
                if (files.Count() != 0)
                {
                    string path1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\haberler\\" + entity.Foto);
                    if (System.IO.File.Exists(path1))
                    {
                        System.IO.File.Delete(path1);
                    }
                    string randomName = string.Format($"{entity.Baslik.ToLower() + ".jpg"}");
                    entity.Foto = randomName;
                    imageService.Process(files.Select(i => new ImageInputModel
                    {
                        FileName = randomName,
                        Type = i.ContentType,
                        Content = i.OpenReadStream()
                    }));
                }
                entity.Baslik = haber.Baslik;
                entity.Detay = haber.Detay;
                entity.EditorMu = haber.EditorMu;
                entity.EkMansetMi = haber.EkMansetMi;
                entity.KategoriId = KategoriId;
                entity.KisaAciklama = haber.KisaAciklama;
                entity.MansetMi = haber.MansetMi;
                entity.SeoDesc = haber.SeoDesc;
                entity.Tarih = haber.Tarih;
                entity.Url = haber.Url;
                entity.SonHaberMi = haber.SonHaberMi;
                entity.PopulerMi = haber.PopulerMi;
                entity.MakaleMi = haber.MakaleMi;
                entity.YazarId = haber.YazarId;
                _haberService.Update(entity);
                var msg = new AlertMessage()
                {
                    Message = "Haber güncellendi.",
                    AlertType = "success"
                };
                TempData["message"] = JsonConvert.SerializeObject(msg);
                return Redirect("/haber");


            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IActionResult> Delete(int id)
        {try
            {
                if (id == 0)
                {
                    return NotFound();
                }
                var haber = await _haberService.GetById(id);
                return haber == null ? NotFound() : View(haber);
            }
            catch (Exception) { throw; }
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var haber = await _haberService.GetById(id);
                if (haber != null)
                {
                    var path1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\haberler\\" + haber.Foto);
                    if (System.IO.File.Exists(path1))
                    {
                        System.IO.File.Delete(path1);
                    }
                    _haberService.Delete(haber);
                    return Redirect("/haber");
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