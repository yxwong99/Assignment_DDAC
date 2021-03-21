using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_DDAC.Models
{
    public class CourseAdmin
    {
        public int ID { get; set; }
        public byte[] Image { get; set; }
        public string Level { get; set; }
        public string Type { get; set; }
        public string CourseName { get; set; }
        public string UniversityName { get; set; }
        public string Location { get; set; }
        public string URL { get; set; }
    }
}
