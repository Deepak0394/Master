using Registration_Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration_Login.Service.IService
{
   public interface IHospitalService
    {
        Task<Result> GetHospitals();
        Task<Result> AddHospital(hospitals hospitals);
        Task<Result> DeleteHospital(int? hospitalId);
        Task<Result> UpdateHospital(hospitals hospitals);
        Task Save();
        Task<Result> GetHospital(int hospitalId);
    }
}
