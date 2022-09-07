using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SiteNews.Business.Abstract;
using SiteNews.Entity;
using SiteNews.Web.Extensions;
using SiteNews.Web.Identity;
using SiteNews.Web.Models;
using SiteNews.Web.Models.Images;
using SiteNews.Web.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SiteNews.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IImageService imageService;
        private readonly IVideoService videoService;
        private readonly ISosyalMedyaService sosyalMedyaService;
        private readonly UserManager<User> userManager;
        public AdminController(IImageService imageService, IVideoService videoService, ISosyalMedyaService sosyalMedyaService, UserManager<User> userManager)
        {
            this.imageService = imageService;
            this.videoService = videoService;
            this.sosyalMedyaService = sosyalMedyaService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Fotolar()
        {
            try
            {
                string[] filePaths = Directory.GetFiles(Path.Combine("wwwroot\\images\\galeri"));
                List<PhotoModel> files = new();
                foreach (string filePath in filePaths)
                {
                    files.Add(new PhotoModel { ImageUrl = Path.GetFileName(filePath) });
                }
                return View(files);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost, ValidateAntiForgeryToken]
        [RequestFormLimits(MultipartBodyLengthLimit = 104857600)]
        public IActionResult Fotolar(IEnumerable<IFormFile> files)
        {
            try
            {
                imageService.ProcessGallery(files.Select(i => new ImageInputModel
                {
                    FileName = i.FileName,
                    Type = i.ContentType,
                    Content = i.OpenReadStream()
                }));
                return RedirectToAction("Fotolar");
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult FotoSil(string fileName)
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\galeri\\" + fileName);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                return RedirectToAction("Fotolar");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IActionResult Videolar()
        {
            try
            {
                List<Video> videos = videoService.GetAll().Result.OrderByDescending(x => x.Id).ToList();
                ViewBag.Videos = videos;
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Videolar(string url)
        {
            try
            {
                if (url != "")
                {
                    Video videos = new()
                    {
                        Url = url
                    };
                    await videoService.CreateAsync(videos);
                }
                return RedirectToAction("videolar");
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IActionResult> VideoDelete(int id)
        {
            try
            {
                if (id == 0)
                {
                    return NotFound();
                }
                Video video = await videoService.GetById(id);
                return video == null ? NotFound() : View(video);
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        [HttpPost, ActionName("VideoDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VideoDeleteConfirmed(int id)
        {
            try
            {
                Video video = await videoService.GetById(id);
                if (video != null)
                {
                    videoService.Delete(video);
                    return Redirect("/admin/videolar");
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
    
        public IActionResult SosyalMedya(int id)
        {
            try
            {
                if (id != 0)
                {
                    SosyalMedya medya = sosyalMedyaService.GetById(id).Result;
                    if (medya != null)
                    {
                        sosyalMedyaService.Delete(medya);
                        return Redirect("/admin/sosyalmedya");
                    }
                    return Redirect("/admin/sosyalmedya");
                }
                Task<List<SosyalMedya>> medyalar = sosyalMedyaService.GetAll();
                return View(medyalar);
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        public IActionResult SosyalMedyaEkle()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SosyalMedyaEkle(SosyalMedya entity)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                else
                {
                    SosyalMedya medya = new()
                    {
                        Link = entity.Link,
                        Name = entity.Name
                    };
                    await sosyalMedyaService.CreateAsync(medya);
                    TempData.Put("message", new AlertMessage()
                    {
                        Title = "Sosyal Medya eklendi.",
                        Message = "",
                        AlertType = "success"
                    });

                    return Redirect("/admin/sosyalmedya");
                }
            }
            catch (Exception)
            {

                return View();
            }
        }
        public IActionResult Kullanicilar(string id)
        {
            try
            {
                if (id != "")
                {
                    User user = userManager.FindByIdAsync(id).Result;
                    if (user!=null)
                    {
                        userManager.DeleteAsync(user);
                        return Redirect("/admin/kullanicilar");
                    }
                }
                List<User> users = userManager.GetUsersInRoleAsync("kullanici").Result.ToList();
                return View(users);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IActionResult KullaniciEkle()
        {
            return View();
        }
    }
}
