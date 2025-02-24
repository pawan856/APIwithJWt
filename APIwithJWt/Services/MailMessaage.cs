using System.Net.Mail;

namespace APIwithJWt.Services
{
    internal class MailMessaage
    {
        public MailAddress from { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public bool IsBodyHtml { get; set; }
    }
}