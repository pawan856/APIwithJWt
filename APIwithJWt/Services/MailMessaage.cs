using System.Net.Mail;

namespace APIwithJWt.Services
{
    internal class MailMessaage
    {
        public MailAddress? from { get; set; }   
        public string subject { get; set; } = string.Empty;
        public string body { get; set; } = string.Empty;
        public bool IsBodyHtml { get; set; }
    }
}