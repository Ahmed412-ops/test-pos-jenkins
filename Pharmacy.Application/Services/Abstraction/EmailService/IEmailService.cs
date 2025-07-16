namespace Pharmacy.Application.Services.Abstraction.EmailService;

public interface IEmailService
{
    Task SendEmailAsync(string fullName, string toEmail, string username, string password);
}
