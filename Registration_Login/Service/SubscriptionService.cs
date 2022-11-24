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
    public class SubscriptionService : ISubscription
    {
        private readonly ApplicationDbContext _context;
        public SubscriptionService(ApplicationDbContext context)
        {
            _context = context;
        }
        public void CreateSubscription(Subscription subscription)
        {
            _context.Subscriptions.Add(subscription);
            _context.SaveChanges();

        }

        public void DeleteSubscription(Subscription subscription)
        {
            _context.Remove(subscription);
            _context.SaveChanges();
        }

       

        public Subscription FindSubsription(int id)
        {
            return _context.Subscriptions.Include(b => b.ApplicationUser).Include(b => b.Plans).FirstOrDefault(m => m.Id == id);

        }

        public IEnumerable<Subscription> GetSubscriptionList()
        {
            return _context.Subscriptions.Include(b => b.ApplicationUser).Include(b => b.Plans).ToList();
        }

        public void UpdateSubscription(Subscription subscription)
        {
            _context.Subscriptions.Update(subscription);
            _context.SaveChanges();
        }
    }
}
