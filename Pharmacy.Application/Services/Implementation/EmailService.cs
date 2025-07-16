using MailKit.Net.Smtp;
using MimeKit;
using Pharmacy.Application.Services.Abstraction.EmailService;

namespace Pharmacy.Application.Services.Implementation;

public class EmailService : IEmailService
{
    private const string SmtpServer = "smtp.gmail.com";
    private const int SmtpPort = 587;
    private const string SenderEmail = "Pharmacyf24@gmail.com";  
    private const string SenderPassword = "escv puts gbpt qyhy";  

    public async Task SendEmailAsync(string fullName,string toEmail, string username, string password)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Pharmacy", SenderEmail));
        message.To.Add(new MailboxAddress("", toEmail));
        message.Subject = "Your Account Credentials";

        message.Body = new TextPart("plain")
        {
            Text = $"Hello {fullName},\n\nYour account information is as follows:\nUsername: {username}\nPassword: {password}\n\nمرحبا {fullName},\n\nمعلومات حسابك كالتالي:\nاسم المستخدم: {username}\nكلمة المرور: {password}"
        };

        using var client = new SmtpClient();
        try
        {
            await client.ConnectAsync(SmtpServer, SmtpPort, false);
            await client.AuthenticateAsync(SenderEmail, SenderPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);

            Console.WriteLine("Email sent successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send email: {ex.Message}");
        }
    }
}