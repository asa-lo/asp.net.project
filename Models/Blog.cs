using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Caser.Models
{
    public class Blog
    {
        [Key]
        public int PostId { get; set; }

        [Required]
        [Display(Name = "Subject")]
        public string PostSub { get; set; }

        [Required]
        [Display(Name = "Post")]
        [DataType(DataType.MultilineText)]
        public string PostContent { get; set; }

    }
}
