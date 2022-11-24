using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Registration_Login.Data;
using Registration_Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration_Login.Controllers
{
    [Authorize]
    [Route("api/Plan")]
    [ApiController]
    public class PlansController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PlansController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("SubscriptionPlanList")]
        public async Task<IEnumerable<Plans>> GetPlan()
        {
            return _context.Plans.ToList();
        }

        [HttpPost("SavePlan")]
        public async Task<IActionResult> SavePlan(Plans plans)
        {
            if (!ModelState.IsValid) return BadRequest("Kindly enter correct data");
            _context.Plans.Add(plans);
            _context.SaveChanges();
            return Ok();

        }
    }
}
