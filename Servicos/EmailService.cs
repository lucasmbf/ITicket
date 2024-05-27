using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

public class EmailService : IEmailService
{
    public async Task SendEmailAsync(string from, string to, string subject, string message)
    {
        var mail = "iticket.lab@gmail.com";
        var password  = "wsta fdwn swpv cfva";

        var client = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential(mail, password),
            EnableSsl = true
        };

        await client.SendMailAsync(
            new MailMessage(from: from,
            to: to,
            subject,
            message
        ));   
    }
}