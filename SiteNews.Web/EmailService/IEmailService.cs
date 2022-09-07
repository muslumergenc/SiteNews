using System.Threading.Tasks;

namespace SiteNews.Web.EmailService
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
