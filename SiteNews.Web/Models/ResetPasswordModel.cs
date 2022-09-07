using System.ComponentModel.DataAnnotations;

namespace SiteNews.Web.Models
{
    public class ResetPasswordModel
    {
        [Required]
        public string Token { get; set; }
        [Required(ErrorMessage = "Mail Adresi Zorunludur.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifreniz En az 6 Karakter ve en az 1 Büyük harf ve özel karakter olmak zorunda.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifreler Uyuşmuyor !")]
        public string RePassword { get; set; }
    }
}
