using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Registration_Login.Models
{
    public class Plans
    {
        public int Id { get; set; }
        [Required]
        public string PlanName { get; set; }
        [Required]
        public string PlanPrice { get; set; }
        [Required]
        public string Data { get; set; }
        [Required]
        public string CallLimit { get; set; }
        [Required]
        public string SMSlimit { get; set; }
        public int Validity { get; set; }
    }
}
