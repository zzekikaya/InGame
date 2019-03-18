using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
 
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace InGame.Common.MailService
{
    //mvc ve api'den şifresmi unuttum işlemleri için ortak bir proje oluşturdum. 
    //ayağa kalktığı uygulamadan appsettings dosyasındaki Secret  bilgilerini alarak owin üzerinden mail atar.
    // maili admin@gmail.com kullanıcının tablodaki bilgilerini okuyarak mail atar
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(Options.SendGridKey, subject, message, email);
        }

        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("admin@gmail.com", "admin users"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
    }
}

