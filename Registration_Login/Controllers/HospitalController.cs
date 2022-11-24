using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Registration_Login.Models;
using Registration_Login.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration_Login.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        private readonly IHospitalService _hospitalService;
        public HospitalController(IHospitalService hospitalService)
        {
            _hospitalService = hospitalService;
        }
        [HttpGet]
        public async Task<ActionResult<Result>> GetHospitals()
        {
            return await _hospitalService.GetHospitals();
        }

        [HttpGet("{Id:int}", Name = "GetHospital")]
        public async Task<ActionResult<Result>> GetHospital(int Id)
        {
            return await _hospitalService.GetHospital(Id);
        }

        [HttpPost]
        public async Task<ActionResult<Result>> AddHospital(hospitals hospital)
        {
            if (hospital == null)
            {
                return Result.Failure(new string[] { "Data not Found" });
            }
            if (!ModelState.IsValid)
            {
                return Result.Failure(new string[] { "Enter Valid Data" });
            }
            return await _hospitalService.AddHospital(hospital);

        }

        [HttpDelete("{hospitalId}")]
        public async Task<ActionResult<Result>> DeleteHospital(int? hospitalId)
        {
            if (await _hospitalService.DeleteHospital(hospitalId) == null)
            {
                return Result.Failure(new string[] { "User doesnt found" });
            }
            return Result.Success(new string[] { "Data Deleted successfully" });
        }
        [HttpPut]
        public async Task<ActionResult<Result>> UpdateEmployee(hospitals hospitals)
        {
            if (hospitals == null)
            {
                return Result.Failure(new string[] { "Data not Found" });
            }
            if (!ModelState.IsValid)
            {
                return Result.Failure(new string[] { "Enter Valid Data" });
            }
            return Result.Success(await _hospitalService.UpdateHospital(hospitals));
        }
    }
}
