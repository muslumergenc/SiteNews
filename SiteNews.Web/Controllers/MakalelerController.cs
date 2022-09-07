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
    public class MakalelerController : Controller
    {
        readonly private IHaberService _haberService;
        readonly private IYazarService _yazarService;
        readonly private IImageService _imageService;
        public MakalelerController(IHaberService haberService, IYazarService yazarService, IImageService imageService)
        {
            _haberService = haberService;
            _yazarService = yazarService;
            _imageService = imageService;
        }

        public async Task<IActionResult> Index(string q, int Id)
        {
            try
            {
                if (q == "read")
                {
                    List<Haber> haberler = await _haberService.ListByMakaleOkuma();
                    return View(haberler);
                }
                else if (Id!=0)
                {
                    List<Haber> haberler = await _haberService.ListByYazar(Id);
                    return View(haberler);
                }
                else
                {
                    List<Haber> haberler = await _haberService.ListByMakaleId();
                    return View(haberler);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IActionResult> Create()
        {try
            {
                List<SelectListItem> yazarlar = (from k in
                                             await _yazarService.GetAll()
                                                 select
                                                 new SelectListItem
                                                 {
                                                     Text = k.Ad,
                                                     Value = k.Id.ToString()
                                                 }).ToList();
                ViewBag.Yazarlar = yazarlar;
                return View();
            }
            catch (System.Exception) { throw; }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestFormLimits(MultipartBodyLengthLimit = 104857600)]
        public async Task<IActionResult> Create(Haber haber, IFormFile[] files)
        {
            try
            {
                List<Yazar> yazarlar = await _yazarService.GetAll();
                ViewBag.Yazarlar = new SelectList(yazarlar, "Id", "Ad");
                try
                {
                    if (files.Count() != 0)
                    {
                        Haber entity = new Haber
                        {
                            Baslik = haber.Baslik,
                            Detay = haber.Detay,
                            EditorMu = haber.EditorMu,
                            EkMansetMi = haber.EkMansetMi,
                            KisaAciklama = haber.KisaAciklama,
                            MansetMi = haber.MansetMi,
                            Okunma = 0,
                            PopulerMi = haber.PopulerMi,
                            SonHaberMi = haber.SonHaberMi,
                            SeoDesc = haber.SeoDesc,
                            Tarih = Convert.ToDateTime(DateTime.Now.ToShortDateString()),
                            Url = haber.Url,
                            YazarId = haber.YazarId,
                            MakaleMi = true
                        };
                        string randomName = string.Format($"{entity.Baslik.ToLower() + ".jpg"}");
                        entity.Foto = randomName;
                        _imageService.Process(files.Select(i => new ImageInputModel
                        {
                            FileName = randomName,
                            Type = i.ContentType,
                            Content = i.OpenReadStream()
                        }));
                        await _haberService.CreateAsync(entity);
                        TempData.Put("message", new AlertMessage()
                        {
                            Title = "Makale eklendi.",
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
                Haber haber = await _haberService.GetById(id);
                List<SelectListItem> yazarlar = (from k in await _yazarService.GetAll()
                                                 select new SelectListItem
                                                 {
                                                     Text = k.Ad,
                                                     Value = k.Id.ToString()
                                                 }).ToList();
                ViewBag.Yazarlar = yazarlar;
                return haber == null ? NotFound() : (IActionResult)View(haber);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Haber haber, IFormFile[] files, int YazarId)
        {
            try
            {
                if (id != haber.Id)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
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
                        _imageService.Process(files.Select(i => new ImageInputModel
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
                    entity.KisaAciklama = haber.KisaAciklama;
                    entity.MansetMi = haber.MansetMi;
                    entity.SeoDesc = haber.SeoDesc;
                    entity.Tarih = haber.Tarih;
                    entity.Url = haber.Url;
                    entity.SonHaberMi = haber.SonHaberMi;
                    entity.PopulerMi = haber.PopulerMi;
                    entity.MakaleMi = true;
                    entity.YazarId = YazarId;
                    _haberService.Update(entity);
                    var msg = new AlertMessage()
                    {
                        Message = "Makale güncellendi.",
                        AlertType = "success"
                    };
                    TempData["message"] = JsonConvert.SerializeObject(msg);
                    return Redirect("/makaleler");
                }
                return View();
            }
            catch (Exception)
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
                var haber = await _haberService.GetById(id);
                return haber == null ? NotFound() : (IActionResult)View(haber);
            }
            catch (Exception)
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
                var haber = await _haberService.GetById(id);
                if (haber != null)
                {
                    var path1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\haberler\\" + haber.Foto);
                    if (System.IO.File.Exists(path1))
                    {
                        System.IO.File.Delete(path1);
                    }
                    _haberService.Delete(haber);
                    return Redirect("/makaleler");
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
