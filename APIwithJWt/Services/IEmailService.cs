using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace APIwithJWt.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(String Email, string subject, string body);
    }
}
