using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SiteNews.Web.EmailService;
using SiteNews.Web.Extensions;
using SiteNews.Web.Identity;
using SiteNews.Web.Models;
using System.Threading.Tasks;

namespace Core.Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class LoginController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailService _emailSender;

        public LoginController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailService;
        }

        public IActionResult Index(string ReturnUrl = null)
        {
            try
            {
                return View(new LoginViewModel
                {
                    ReturnUrl = ReturnUrl
                });
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "Kullanıcı Bulunamadı !");
                    return View(model);
                }
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    ModelState.AddModelError("", "Lütfen Email Adresinize gönderdiğimiz link ile üyeliğinizi onaylayınız !");
                    return View(model);
                }
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return Redirect(model.ReturnUrl ?? "~/admin/index");
                }
                ModelState.AddModelError("", "Girilen kullanıcı adı veya parola yanlış");
                return View(model);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                TempData.Put("message", new AlertMessage
                {
                    Title = "Oturum Kapatıldı.",
                    Message = "",
                    AlertType = "warning"
                });
                return RedirectToAction("index");

            }
            catch (System.Exception)
            {

                throw;
            }
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            try
            {
                if (string.IsNullOrEmpty(Email))
                {
                    TempData.Put("message", new AlertMessage
                    {
                        Title = "Dikkat !",
                        Message = "Lütfen mail adresini boş bırakmayınız !",
                        AlertType = "danger"
                    });
                    return View();
                }
                var user = await _userManager.FindByEmailAsync(Email);
                if (user == null)
                {
                    TempData.Put("message", new AlertMessage
                    {
                        Title = "Mail Adresi Bulunamadı.",
                        Message = "Lütfen yazdığınız mail adresini kontrol ediniz !",
                        AlertType = "warning"
                    });
                    return View();
                }
                if (user != null && await _userManager.IsEmailConfirmedAsync(user))
                {
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var url = Url.Action("ResetPassword", "login", new
                    {
                        userId = user.Id,
                        token = code
                    }, Request.Scheme);
                    TempData.Put("message", new AlertMessage
                    {
                        Title = "Başarılı.",
                        Message = "Şifreniz Mail Adresinize Gönderilmiştir !",
                        AlertType = "success"
                    });
                    await _emailSender.SendEmailAsync(Email, "Şifre Yenileme", $"Parolanızı yenilemek için lütfen linke <a href='https://localhost:44393/{url}'>tıklayınız.</a>");
                }
                else
                {
                    TempData.Put("message", new AlertMessage
                    {
                        Title = "Hata!",
                        Message = "Mail Adresiniz Henüz Onaylanmamıştır.Lütfen mail adresine gelen onay mesajını inceleyiniz!",
                        AlertType = "danger"
                    });
                }
                return RedirectToAction("index", "login");

            }
            catch (System.Exception)
            {

                throw;
            }
        }
        public IActionResult ResetPassword(string userId, string token)
        {
            try
            {
                if (userId == null || token == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                //var model = new ResetPasswordModel { Token = token };

                return View();
            }
            catch (System.Exception) { throw; }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    TempData.Put("message", new AlertMessage
                    {
                        Title = "Hata !",
                        Message = "EMail Adresi bulunamadı!",
                        AlertType = "danger"
                    });
                    return View();
                }
                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    TempData.Put("message", new AlertMessage
                    {
                        Title = "Başarılı !",
                        Message = "Şifreniz başarıyla değiştirildi !",
                        AlertType = "success"
                    });
                    return RedirectToAction("Index", "login");
                }
                else
                {
                    TempData.Put("message", new AlertMessage
                    {
                        Title = "Hata !",
                        Message = "Şifreniz Değiştirilmedi !",
                        AlertType = "danger"
                    });
                }
                return View(model);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                
                User user = new User()
                {
                    UserName = model.Email[..5],
                    Email = model.Email,
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // generate token
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    await _userManager.AddToRoleAsync(user, "kullanici");
                    var url = Url.Action("ConfirmEmail", "Login", new
                    {
                        userId = user.Id,
                        token = code
                    });

                    // email
                    await _emailSender.SendEmailAsync(model.Email, "Hesabınızı onaylayınız.", $"Lütfen email hesabınızı onaylamak için linke <a href='https://localhost:44393{url}'>tıklayınız.</a>");
                    TempData.Put("message", new AlertMessage()
                    {
                        Title = "Başarılı",
                        Message = "Kayıt işlemini tamamlanmak için Lütfen email adresinize gelen mesajı onaylayın !",
                        AlertType = "success"
                    });
                    return RedirectToAction("index", "login");
                }
                TempData.Put("message", new AlertMessage()
                {
                    Title = "Hata!.",
                    Message = "Bilinmeyen hata oldu lütfen tekrar deneyiniz",
                    AlertType = "danger"
                });
                return View(model);

            }
            catch (System.Exception)
            {

                throw;
            }
        }
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {try
            {
                if (userId == null || token == null)
                {
                    TempData.Put("message", new AlertMessage()
                    {
                        Title = "Geçersiz token.",
                        Message = "Geçersiz Token",
                        AlertType = "danger"
                    });
                    return View();
                }
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    var result = await _userManager.ConfirmEmailAsync(user, token);
                    if (result.Succeeded)
                    {
                        TempData.Put("message", new AlertMessage()
                        {
                            Title = "Hesabınız onaylandı.",
                            Message = "Hesabınız onaylandı.",
                            AlertType = "success"
                        });
                        return View();
                    }
                }
                TempData.Put("message", new AlertMessage()
                {
                    Title = "Hesabınızı onaylanmadı.",
                    Message = "Hesabınızı onaylanmadı.",
                    AlertType = "warning"
                });
                return View();
            }
            catch (System.Exception) { throw; }
        }
    }
}
