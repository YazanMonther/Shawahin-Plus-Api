using Microsoft.Extensions.Options;
using MimeKit;
using ShawahinAPI.Core.DTO;
using ShawahinAPI.Services.Contract;
using MailKit.Net.Smtp;
using MailKit.Security;


namespace ShawahinAPI.Services.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly EmailSetting emailSetting;
        public EmailService(IOptions<EmailSetting> options) {
        this.emailSetting = options.Value;
        }
        public async Task SendEmailAsync(EmailRequest mailRequest)
        {
            var email = new MimeMessage();

            email.Sender = MailboxAddress.Parse(emailSetting.Email);

            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));

            email.Subject = mailRequest.Subject;

            var builder = new BodyBuilder();

            builder.HtmlBody = mailRequest.Body;

            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(emailSetting.Host, emailSetting.Port, SecureSocketOptions.StartTls);

            smtp.Authenticate(emailSetting.Email, emailSetting.Password);

            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
