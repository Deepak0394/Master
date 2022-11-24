using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Registration_Login
{
    public class MailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        
    }
}
