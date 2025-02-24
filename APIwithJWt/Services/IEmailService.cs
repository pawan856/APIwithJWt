using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace APIwithJWt.Services
{
    public interface IEmailService
    {
        Task SendEmailasync(String toEmail, string subject, string body);
    }
}
