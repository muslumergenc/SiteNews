using System.ComponentModel.DataAnnotations;

namespace SiteNews.Web.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email Adresi Boş Geçilemez")]
        [DataType(DataType.EmailAddress,ErrorMessage ="")]
        public string Email { get; set; }
        public string UserName { get; set; }
        [Required(ErrorMessage = "Şifre Boş Geçilemez")]
        [DataType(DataType.Password,ErrorMessage = "Şifreniz En az 6 Karakter ve en az 1 Büyük harf ve özel karakter olmak zorunda")]
        public string Password { get; set; }
        [Required(ErrorMessage ="Şifre Onayı Boş Geçilemez")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Şifreler Uyuşmuyor !")]
        public string RePassword { get; set; }
    }
}
