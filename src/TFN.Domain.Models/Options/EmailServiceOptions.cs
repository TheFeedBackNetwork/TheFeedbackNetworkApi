using System.Net;
using System.Net.Mail;

namespace TFN.Domain.Models.Options
{
    public class EmailServiceOptions
    {
        public MailAddress Sender { get; set; }

        public NetworkCredential Credentials { get; set; }

        public string SmtpHost { get; set; }

        public int SmtpPort { get; set; }
    }
}
