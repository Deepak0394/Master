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
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorsService _doctorService;
        public DoctorController(IDoctorsService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public async Task<ActionResult<Result>> GetDoctors()
        {
            return await _doctorService.Getdoctorlists();
        }

        [HttpGet("{Id:int}", Name = "GetDoctor")]
        public async Task<ActionResult<Result>> GetDoctor(int Id)
        {
            return await _doctorService.Getdoctorlist(Id);
        }

        [HttpPost]
        public async Task<ActionResult<Result>> AddDoctor(doctorlist doctorlist)
        {
            if (doctorlist == null)
            {
                return Result.Failure(new string[] { "Data not Found" });
            }
            if (!ModelState.IsValid)
            {
                return Result.Failure(new string[] { "Enter Valid Data" });
            }
            return await _doctorService.Adddoctorlist(doctorlist);

        }

        [HttpDelete("{DoctorId}")]
        public async Task<ActionResult<Result>> DeleteDoctor(int? DoctorId)
        {
            if (await _doctorService.Deletedoctorlist(DoctorId) == null)
            {
                return Result.Failure(new string[] { "User doesnt found" });
            }
            return Result.Success(new string[] { "Data Deleted successfully" });
        }
        [HttpPut]
        public async Task<ActionResult<Result>> UpdateDoctor(doctorlist doctorlist)
        {
            if (doctorlist == null)
            {
                return Result.Failure(new string[] { "Data not Found" });
            }
            if (!ModelState.IsValid)
            {
                return Result.Failure(new string[] { "Enter Valid Data" });
            }
            return Result.Success(await _doctorService.Updatedoctorlist(doctorlist));
        }
    }
}
