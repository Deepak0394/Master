
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;
using SendGrid;
using System.Threading.Tasks;
using System;
using Registration_Login.Service.IService;

namespace Registration_Login.Service
{
    public class SendGridService : ISendGridService
    {
        
            private readonly IConfiguration _configuration;
            
            public SendGridService(IConfiguration configuration)
            {
                _configuration = configuration;
               
            }

            public async Task SendEmailAsync(string ToEmail)
            {
                var apiKey = _configuration["SendGridApiKey"];
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("sandsea478@gmail.com", "JWT Project");
                var to = new EmailAddress(ToEmail);
                var subject = "New login";
                var content = "hii..congrats";
                var html = "<h1>hii... New Register to your account</h1><p>New LogIn  to yur account at" + DateTime.Now + "</p>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, content, html);
                var response = await client.SendEmailAsync(msg);
            }
        }
    }

