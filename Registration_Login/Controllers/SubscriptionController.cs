using Microsoft.AspNetCore.Authorization;
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
    
    [Route("api/Subscriptions")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscription _subscription;
        public SubscriptionController(ISubscription subscription)
        {
            _subscription = subscription;
        }
        [HttpGet("SubscriptionList")]

        public async Task<IEnumerable<Subscription>> GetList()
        {
            return  _subscription.GetSubscriptionList();
        }
        [HttpPost("AddSubscriptionMember")]
        public async Task<IActionResult> SaveSubscription(Subscription subscription )
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Data is not feed as per requirement");
            }
            _subscription.CreateSubscription(subscription);
            return Ok();
        }
        [HttpPut("UpdateSubscriptionMember")]
        public async Task<IActionResult> UpdateSubscription(Subscription subscription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Data is not feed as per requirement");
            }
            _subscription.UpdateSubscription(subscription);
            return Ok();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var subsriptionDetail = _subscription.FindSubsription(id);
            _subscription.DeleteSubscription(subsriptionDetail);
            return Ok();
          


        }

    }
}
