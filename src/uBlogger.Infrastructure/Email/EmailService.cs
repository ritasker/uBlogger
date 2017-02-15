using System;
using System.Net;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using uBlogger.Infrastructure.Email.Templates;

namespace uBlogger.Infrastructure.Email
{
    public class EmailService
    {
        private readonly EmailConfiguation _config;

        public EmailService(EmailConfiguation config)
        {
            _config = config;
        }
        public async Task SendEmail(EmailTemplate email, string toAddress)
        {
            try
            {
                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress(_config.FromName, _config.FromAddress));
                emailMessage.To.Add(new MailboxAddress("", toAddress));
                emailMessage.Subject = email.Subject;
                emailMessage.Body = new TextPart(TextFormat.Html) { Text = email.Body };

                using (var client = new SmtpClient())
                {
                    client.LocalDomain = _config.LocalDomain;

                    await client.ConnectAsync(_config.MailServerAddress, Convert.ToInt32(_config.MailServerPort)).ConfigureAwait(false);
                    await client.AuthenticateAsync(new NetworkCredential(_config.UserId, _config.UserPassword));
                    await client.SendAsync(emailMessage).ConfigureAwait(false);
                    await client.DisconnectAsync(true).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}