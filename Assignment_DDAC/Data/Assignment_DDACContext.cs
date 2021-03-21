using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Assignment_DDAC.Models;


namespace Assignment_DDAC.Data
{
    public class Assignment_DDACContext : DbContext
    {
        public Assignment_DDACContext (DbContextOptions<Assignment_DDACContext> options)
            : base(options)
        {
        }

        public DbSet<Assignment_DDAC.Models.CourseAdmin> CourseAdmin { get; set; }

        public DbSet<Assignment_DDAC.Models.CourseAdmin> CourseUser { get; set; }

        public DbSet<Assignment_DDAC.Models.Enquiry> Enquiry { get; set; }

    }
}
