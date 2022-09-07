using System.ComponentModel.DataAnnotations;

namespace SiteNews.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Mail Adresi Zorunludur.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifre Zorunludur.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        [Display(Name = "Beni Hatırla")]
        public bool RememberMe { get; set; }
        public string UserName { get; set; }
    }
}
