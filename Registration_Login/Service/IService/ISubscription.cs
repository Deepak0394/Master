using Registration_Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration_Login.Service.IService
{
  public  interface ISubscription
    {
        IEnumerable<Subscription> GetSubscriptionList();
        void CreateSubscription(Subscription subscription);
        Subscription FindSubsription(int id);
        void UpdateSubscription(Subscription subscription);
        void DeleteSubscription(Subscription subscription);
        

    }
}
