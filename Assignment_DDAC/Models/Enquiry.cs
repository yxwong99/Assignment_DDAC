using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment_DDAC.Models
{
    public class Enquiry
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }


        [Display(Name = "Customer Email")]
        public string CustomerEmail { get; set; }

        [Display(Name = "University Name")]
        public string UniversityName { get; set; }

        [Display(Name = "Course Name")]
        public string CourseName { get; set; }
    }
}
