using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using VoiceOfKarabakh.Application.Interfaces.Email;
using VoiceOfKarabakh.Application.Options;

namespace VoiceOfKarabakh.Application.Services.Email
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly SmtpOption _smtpOption;

        public EmailSenderService(IOptions<SmtpOption> smtpOption)
        {
            _smtpOption = smtpOption.Value;
        }

        //we send
        public void Send(string to, string topic, string message)
        {
            SmtpClient smtpClient = new SmtpClient()
            {
                Host = _smtpOption.Host,
                Port = _smtpOption.Port,
                EnableSsl = false,
                Credentials = new NetworkCredential(_smtpOption.Email, _smtpOption.Password)
            };

            MailMessage mailMessage = new MailMessage()
            {
                From = new MailAddress(_smtpOption.Email),
                Subject = topic,
                Body = message
            };
            mailMessage.To.Add(to);

            smtpClient.Send(mailMessage);
        }

        //we receive
        public void Receive(string from, string topic, string message, IEnumerable<IFormFile> files = null)
        {
            SmtpClient smtpClient = new SmtpClient()
            {
                Host = _smtpOption.Host,
                Port = _smtpOption.Port,
                EnableSsl = false,
                Credentials = new NetworkCredential(_smtpOption.Email, new System.Security.SecureString())
            };

            MailMessage mailMessage = new MailMessage()
            {
                From = new MailAddress(from),
                Subject = topic,
                Body = message
            };

            if (files != null)
            {
                foreach (var file in files)
                {
                    mailMessage.Attachments.Add(new Attachment(file.OpenReadStream(), file.FileName));
                }
            }

            mailMessage.To.Add(_smtpOption.Email);

            smtpClient.Send(mailMessage);
        }
    }
}
