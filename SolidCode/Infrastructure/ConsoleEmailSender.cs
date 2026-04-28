using SolidCode.Services;

namespace SolidCode.Infrastructure;

public class ConsoleEmailSender : IEmailSender
{
    public void SendOrderConfirmation(string customerEmail, decimal total)
    {
        Console.WriteLine($"Confirmation email sent to {customerEmail}. Total: {total:0.00}");
    }
}
