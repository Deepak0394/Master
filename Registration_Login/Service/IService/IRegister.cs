using Registration_Login.Identity;
using Registration_Login.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration_Login.Service.IService
{
   public interface IRegister
    {
        Task<ApplicationUser> Authenticate(LoginVM loginVM);
        IEnumerable<ApplicationUser> GetUsers();

        Task<ApplicationUser> CreateUser(RegisterVM registerVM);

    }
}
