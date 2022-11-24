
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Registration_Login.Data;
using Registration_Login.Identity;
using Registration_Login.Models.ViewModels;
using Registration_Login.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vonage;
using Vonage.Request;
using Vonage.Verify;

namespace Registration_Login.Controllers
{
    [Route("api/Registration")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRegister _userServie;
        private readonly IMailService _mailService;
        private readonly ISendGridService _sendGridService;
        public VonageCredentials _vonageCredentials  { get; }
        public RegisterController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,IRegister userServie, IOptions<VonageCredentials> vonageCredentials,IMailService mailService,ISendGridService sendGridService)
        {
            _context = context;
            _userManager = userManager;
           _userServie= userServie;
            _vonageCredentials = vonageCredentials.Value;
            _mailService = mailService;
            _sendGridService = sendGridService;
          
        }
        [HttpGet("UserList")]
        public async Task<IEnumerable<ApplicationUser>> GetUserList()
        {
            return _userServie.GetUsers();
        }



        [HttpPost("RegisterUser")]
        public async Task<IActionResult> Register([FromBody] RegisterVM register)
        {
            if (!ModelState.IsValid)
            {
                return Ok("Fill Valid Data");
            }
            else
            {
                var existingUser = await _userManager.FindByEmailAsync(register.Email);
                if (existingUser == null)
                {
                    ApplicationUser user = new ApplicationUser()
                    {

                        Name = register.Name,
                        StreetAddress = register.StreetAddress,
                        State = register.State,
                        City = register.City,
                        PostalCode = register.PostalCode,
                        UserName = register.UserName,
                        Email = register.Email,
                        PhoneNumber = register.PhoneNumber
                    };
                 

                    var result = await _userManager.CreateAsync(user, register.Password);
                    if (!result.Succeeded)

                        return BadRequest("Error ,User creation failed! Please check user details and try again.");




                    //Send Message
                    var credentials = Credentials.FromApiKeyAndSecret(
                      _vonageCredentials.APIKey,
                      _vonageCredentials.APISecret
                        );
                    var VonageClient = new VonageClient(credentials);




                    var response = VonageClient.SmsClient.SendAnSms(new Vonage.Messaging.SendSmsRequest()
                    {
                        To = user.PhoneNumber,
                        From = _vonageCredentials.PhoneNumber,
                        Text = "You have Register Successfully ",
                        Title = "Vonage Sms",
                        Body = "Thank you for register with use ."

                    });

                    //Email Send 
                    _mailService.SendEmailAsync(register.Email);
                    _sendGridService.SendEmailAsync(register.Email);

                    return Ok(result);
                    _context.SaveChanges();
                    return Ok(user);
                }
                return Ok(existingUser);
            }

            return BadRequest(" ");



        }

        [HttpPost("LoginUser")]
        public async Task<IActionResult> login(LoginVM loginVM)
        {
            var user = await _userServie.Authenticate(loginVM);
            if (user == null)
                return BadRequest(new { message = "Wrong Email or Password" });
            return Ok(user);
        }
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateRegister([FromBody]RegisterVM register)
        {

            if (!ModelState.IsValid && register == null)
            {
                return BadRequest("Enter Valid Data");
            }
            else
            {
                var obj = await _userManager.FindByEmailAsync(register.Email);
                if (obj == null)
                    return null;
                else
                {
                    ApplicationUser user = new ApplicationUser()
                    {
                        StreetAddress = register.StreetAddress,
                        State = register.State,
                        City = register.City,
                        PostalCode = register.PostalCode,
                        Name = register.Name,
                        PhoneNumber = register.PhoneNumber,
                    };
                    _userManager.UpdateAsync(user);
                    return Ok(user);
                }
                   
            }

            return Ok();
        }
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteRegister(string REmail)
        {
            var UserInDb = await _userManager.FindByEmailAsync(REmail);
            if (UserInDb == null)
            {
                return BadRequest(new { message = "User Not Found" });
            }
            else
                _context.ApplicationUsers.Remove(UserInDb);
            _context.SaveChanges();
              
            
            return Ok(UserInDb);
        }





    }
}
