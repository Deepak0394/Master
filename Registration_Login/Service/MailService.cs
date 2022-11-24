


using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Registration_Login.Service.IService;

namespace Registration_Login.Service
{
    public class MailService:IMailService
    {
        private readonly AppSetting _mailSettings;
        //private readonly MailRequest _mailRequest;

        public MailService(IOptions<AppSetting> mailSettings)
        {
            _mailSettings = mailSettings.Value;
            //_mailRequest = mailRequest.Value;
        }
        public bool SendEmailAsync(string email)
        {
            SmtpClient mailClient = new SmtpClient(_mailSettings.Host, _mailSettings.Port);
            mailClient.Credentials = new NetworkCredential(_mailSettings.Mail, _mailSettings.Password);
            mailClient.EnableSsl = true;

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_mailSettings.Mail);
            mailMessage.To.Add(email);

            mailMessage.Subject = "Kuch nahi";
            //var body = GetMailBodyWithButton(email);
            mailMessage.Body = "Hello";
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority =MailPriority.High;


             mailClient.Send(mailMessage);

            return true;

        }

        //Task IMailService.SendEmailAsync(MailRequest mailRequest)
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}
