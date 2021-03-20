using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Caser.Models
{
    public class Customers
    {
        [Key]
        [Display(Name = "Customer ID")]
        public int CustId { get; set; }

        [Required]
        [Display(Name = "Customer Name")]
        public string CustName { get; set; }

        [Display(Name = "E-mail address")]
        public string CustEmail { get; set; }

        [Display(Name = "Phone")]
        public string CustPhone { get; set; }

    }
}
