using Registration_Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration_Login.Service.IService
{
  public  interface IDoctorsService
    {
        Task<Result> Getdoctorlists();
        Task<Result> Adddoctorlist(doctorlist doctorlist);
        Task<Result> Deletedoctorlist(int? doctorlistId);
        Task<Result> Updatedoctorlist(doctorlist doctorlist);
        Task Save();
        Task<Result> Getdoctorlist(int doctorlistId);
    }
}
