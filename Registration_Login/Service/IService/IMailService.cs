
using System.Threading.Tasks;

namespace Registration_Login.Service.IService
{
    public interface IMailService
    {
      bool SendEmailAsync(string email);
       // Task SendEmailAsync(MailRequest mailRequest);
    }
}

