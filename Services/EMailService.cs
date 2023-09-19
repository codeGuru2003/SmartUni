using SmartUni.Models;

namespace SmartUni.Services
{
    public interface EMailService
    {
        bool SendMail(MailData mailData);
    }
}
