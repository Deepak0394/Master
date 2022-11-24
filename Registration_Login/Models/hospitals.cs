using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Registration_Login.Models
{
    public class hospitals
    {
        public int Id { get; set; }
        public string hospitalname { get; set; }
        public string facilities { get; set; }
        public string department { get; set; }
        public int doctorId { get; set; }
        [ForeignKey("doctorId")]
        public doctorlist doctorlist { get; set; }
    }
}
