using Registration_Login.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Registration_Login.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        [Required]
        public DateTime SubStartDate { get; set; }
        [Required]
        public DateTime SubEndDate { get; set; }
        [Required]
        public int PlanId { get; set; }
        [ForeignKey("PlanId")]
        public Plans Plans { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser  ApplicationUser { get; set; }
    }
}
