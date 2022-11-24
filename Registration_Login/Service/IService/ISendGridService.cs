using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Registration_Login.Service.IService
{
    public interface ISendGridService
    {
        Task SendEmailAsync(string ToEmail);
    }
   
}
