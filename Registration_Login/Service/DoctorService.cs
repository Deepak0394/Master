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
    public class DoctorService : IDoctorsService
    {
        private readonly ApplicationDbContext _context;
        public DoctorService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Adddoctorlist(doctorlist doctorlist)
        {
            await _context.doctorlists.AddAsync(doctorlist);
            await Save();
            return Result.Success();
        }

        public async Task<Result> Deletedoctorlist(int? doctorlistId)
        {
            var DoctorInDb = await _context.doctorlists.FindAsync(doctorlistId);
            if (DoctorInDb == null)
            {
                Result.Failure(new String[] { "ID not found" });
            }
            else
                _context.doctorlists.Remove(DoctorInDb);
            await Save();

            return Result.Success();
        }

        public async Task<Result> Getdoctorlist(int doctorlistId)
        {
            var obj = await _context.doctorlists.FindAsync(doctorlistId);
            if (obj != null)
            {
                return Result.Success(obj);
            }
            else
                return Result.Failure(new string[] { "Employee not found" });
        }

        public async Task<Result> Getdoctorlists()
        {
            var obj = await _context.doctorlists.ToListAsync();
            return Result.Success(obj);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Result> Updatedoctorlist(doctorlist doctorlist)
        {
            var obj = await _context.doctorlists.FindAsync(doctorlist.Id);
            if (obj == null)
                return Result.Failure(new string[] { "Employee not found" });
            else
                obj.doctorname = doctorlist.doctorname;
            obj.qualification = doctorlist.qualification;
            obj.expierence = doctorlist.expierence;
            obj.SpecialisedIn = doctorlist.SpecialisedIn;
            _context.doctorlists.Update(obj);
            await Save();
            return Result.Success();
        }
    }
}
