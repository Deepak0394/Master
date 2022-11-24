using Microsoft.EntityFrameworkCore;
using Registration_Login.Data;
using Registration_Login.Models;
using Registration_Login.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration_Login.Service
{
    public class HospitalService:IHospitalService
    {
        private readonly ApplicationDbContext _context;
        public HospitalService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> AddHospital(hospitals hospitals)
        {
            await _context.hospitals.AddAsync(hospitals);
            await Save();
            return Result.Success();
        }

        public async Task<Result> DeleteHospital(int? hospitalId)
        {
            var hospitalInDb = await _context.hospitals.FindAsync(hospitalId);
            if (hospitalInDb == null)
            {
                Result.Failure(new String[] { "ID not found" });
            }
            else
                _context.hospitals.Remove(hospitalInDb);
            await Save();

            return Result.Success();
        }

        public async Task<Result> GetHospital(int hospitalId)
        {
            var obj = await _context.hospitals.FindAsync(hospitalId);
            if (obj != null)
            {
                return Result.Success(obj);
            }
            else
                return Result.Failure(new string[] { "Hospital not found" });
        }

        public async Task<Result> GetHospitals()
        {
            var obj = await _context.hospitals.Include(x=>x.doctorlist).ToListAsync();
            return Result.Success(obj);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();

        }

        public async Task<Result> UpdateHospital(hospitals hospitals)
        {
            var obj = await _context.hospitals.FindAsync(hospitals.Id);
            if (obj == null)
                return Result.Failure(new string[] { "Hospital not found" });
            else
                obj.hospitalname = hospitals.hospitalname;
            obj.facilities = hospitals.facilities;
            obj.department = hospitals.department;
            ; obj.doctorId = hospitals.doctorId;
            _context.hospitals.Update(obj);
            await Save();
            return Result.Success();
        }
    }
}
