namespace SolidCode.Services;

public interface IEmailSender
{
    void SendOrderConfirmation(string customerEmail, decimal total);
}
