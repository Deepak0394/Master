

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Registration_Login.Identity;
using Registration_Login.Service.IService;
using System;
using System.Threading.Tasks;

namespace Registration_Login.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMailService _mailService;
        private readonly ISendGridService _sendGridService; 
        public EmailController(IMailService mailService,UserManager<ApplicationUser> userManager, ISendGridService sendGridService)
        {
            _mailService = mailService; 
            _userManager = userManager;
            _sendGridService = sendGridService;
        }
        [HttpPost("SendEmail")]
        public async Task<IActionResult>Sendmail([FromBody] MailRequest request)
        {
            try
            {
                _sendGridService.SendEmailAsync(request.ToEmail);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //[HttpPost("EmailSend")]
        //public async Task<IActionResult> ConfirmEmail(string token,string email)
        //{
        //    var user = await _userManager.FindByEmailAsync(email);
        //    if (user == null)
        //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Invalid Email..please check it again " });

        //    var result = await _userManager.ConfirmEmailAsync(user, token);
        //    return Ok(result.Succeeded ? "ConfirmEmail" : "Error");
        //}
        [HttpPost("Send")]
        public async  Task<IActionResult> Send([FromBody] MailRequest request)
        {
            try
            {
                _mailService.SendEmailAsync(request.ToEmail);
                return Ok();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
