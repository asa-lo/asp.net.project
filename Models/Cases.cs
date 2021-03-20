using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Caser.Models
{
    public class Cases
    {
        [Key]
        [Display(Name = "Ticket ID")]
        public int CaseId { get; set; }

        [Display(Name = "Subject")]
        public string CaseSubject { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string CaseDesc { get; set; }

        [Display(Name = "Contact")]
        public string CaseContactName { get; set; }

        [Display(Name = "Phone")]
        public string CaseContactPhone { get; set; }

        [Display(Name = "Customer ID")]
        public int CustId { get; set; }

        [Display(Name = "Check to close ticket")]
        public bool CaseIsFinished { get; set; } = false;
        
        [Display(Name = "Internal Comment")]
        public string CaseIntComment { get; set; }

        public Customers Customers { get; set; }

    }
}
