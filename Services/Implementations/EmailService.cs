using System.Net;
using System.Net.Mail;
using SupplyChainX.Services.Interfaces;

namespace SupplyChainX.Services.Implementations;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task SendEmailAsync(string to, string subject, string message)
    {
        var smtpClient = new SmtpClient(_configuration["Email:SMTP:Host"])
        {
            Port = int.Parse(_configuration["Email:Port"]!),
            Credentials = new NetworkCredential(
                _configuration["Email:Username"],
                _configuration["Email:Password"]),
            EnableSsl = true
        };
        
        var mail = new MailMessage(_configuration["Email:Username"], to, subject, message);
        mail.IsBodyHtml = true;
        
        return smtpClient.SendMailAsync(mail);
    }
}