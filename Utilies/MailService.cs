using Microsoft.Extensions.Options;
using SistemaMedico.DTOs;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SistemaMedico.Utilies
{
    public class EmailService
    {
        private readonly EmailSettingsDTO _mailSettings;

        public EmailService(IOptions<EmailSettingsDTO> emailSettings)
        {
            _mailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var smtpClient = new SmtpClient(_mailSettings.Host)
            {
                Port = 587,
                Credentials = new NetworkCredential(_mailSettings.Mail, _mailSettings.Password),
                EnableSsl = true,
            };

            var message = new MailMessage(_mailSettings.Mail, toEmail, subject, body)
            {
                IsBodyHtml = true
            };

            await smtpClient.SendMailAsync(message);
        }

        public async Task SendPaymentLinkAsync(string toEmail, string paymentLink)
        {
            string subject = "Link de Pagamento do seu Tratamento";
            string body = $"Olá, segue o link para realizar o pagamento: {paymentLink}";

            await SendEmailAsync(toEmail, subject, body);
        }
    }
}
