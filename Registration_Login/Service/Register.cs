using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Registration_Login.Data;
using Registration_Login.Identity;
using Registration_Login.Models.ViewModels;
using Registration_Login.Service.IService;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Vonage;
using Vonage.Request;

namespace Registration_Login.Service
{
    public class Register : IRegister
    {
        private readonly ApplicationDbContext _context;
        private readonly ApplicationSignInManager _applicationSignInManager;
        private readonly AppSettings _appSettings;
        private readonly ApplicationUserManager _applicationUserManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISendGridService _sendGridService;
        public VonageCredentials _vonageCredentials { get; }
        public Register(IOptions<AppSettings> appSettings, ApplicationUserManager applicationUserManager, ApplicationDbContext context, UserManager<ApplicationUser> userManager, ApplicationSignInManager applicationSignInManager, IOptions<VonageCredentials> vonageCredentials, ISendGridService sendGridService)
        {
            _context = context;
            _applicationSignInManager = applicationSignInManager;
            _applicationUserManager = applicationUserManager;
            _appSettings = appSettings.Value;
            _vonageCredentials = vonageCredentials.Value;
            _userManager = userManager;
            _sendGridService = sendGridService;
        }

        public async Task<ApplicationUser> Authenticate(LoginVM loginVM)
        {
            var user = await _applicationSignInManager.PasswordSignInAsync(loginVM.UserName, loginVM.Password, false, false);
            
            if (user.Succeeded)
            {
                var appicationUser = await _applicationUserManager.FindByNameAsync(loginVM.UserName);
                appicationUser.PasswordHash = "";

               

                //JWT Token Genrated

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, appicationUser.Id),
                  
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                appicationUser.Token = tokenHandler.WriteToken(token);
                return appicationUser;

            }
            else
            {
                return null;
            }
        }
        public IEnumerable<ApplicationUser> GetUsers()
        {
            return _context.ApplicationUsers.ToList();
        }

        public async Task<ApplicationUser> CreateUser(RegisterVM register)
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
                   //Otp
                  /*  var credentials = Credentials.FromApiKeyAndSecret(
                      _vonageCredentials.APIKey,
                      _vonageCredentials.APISecret
                        );
                var VonageClient = new VonageClient(credentials);
                var request = new VerifyRequest()
                {
                    Brand = "Vonage ",
                    Number = user.PhoneNumber,
                    NextEventWait = 180,
                    PinExpiry = 180,
                    CodeLength = 4
                };

                var Response = VonageClient.VerifyClient.VerifyRequest(request);

                if (Response.RequestId.Length > 0)
                {
                    return RedirectToAction("VerifyPhone", "Register");
                }
                if (Response.Status == "0")
                {
                    return Ok("User Verified Successfully");
                }
                else
                {
                    return BadRequest("User Enter wrong OTP");
                }*/
                


           var result = await _userManager.CreateAsync(user, register.Password);
                if (result.Succeeded)
                {
                    //Send Message
                    var credentials = Credentials.FromApiKeyAndSecret(
                      _vonageCredentials.APIKey,
                      _vonageCredentials.APISecret
                        );
                 var  VonageClient = new VonageClient(credentials);

                    var response = VonageClient.SmsClient.SendAnSms(new Vonage.Messaging.SendSmsRequest()
                    {
                        To = user.PhoneNumber,
                        From = _vonageCredentials.PhoneNumber,
                        Text = "You have Register Successfully ",
                        Title = "Vonage Sms",
                        Body = "Thank you for register with use ."

                    });
                    //Email Send
                    _sendGridService.SendEmailAsync(register.Email);
                    return user;
                }




                else
                {
                    return null;


                } ;


            }
            return existingUser;
        }

    }
}
