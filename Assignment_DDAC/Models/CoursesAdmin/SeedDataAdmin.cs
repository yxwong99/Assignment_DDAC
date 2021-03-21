using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment_DDAC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment_DDAC.Models
{
    public class SeedDataAdmin
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var contextAdmin = new Assignment_DDACContext(serviceProvider.GetRequiredService<DbContextOptions<Assignment_DDACContext>>()))
            {
                // Look for any flower.
                if (contextAdmin.CourseAdmin.Any())
                {
                    return;
                    // DB has been seeded
                }
                contextAdmin.CourseAdmin.AddRange(
                    new CourseAdmin
                    {
                        Level = "Foundation",
                        Type = "Business",
                        CourseName = "Foundation in Business Study",
                        UniversityName = "Sunway",
                        Location = "Petaling Jaya",
                        URL = "https://university.sunway.edu.my/",
                    },
                    new CourseAdmin
                    {
                        Level = "Diploma",
                        Type = "Business",
                        CourseName = "Accounting",
                        UniversityName = "Sunway",
                        Location = "Petaling Jaya",
                        URL = "https://university.sunway.edu.my/",
                    },
                    new CourseAdmin
                    {
                        Level = "Degree",
                        Type = "Computing",
                        CourseName = "Software Engineering",
                        UniversityName = "Sunway",
                        Location = "Petaling Jaya",
                        URL = "https://university.sunway.edu.my/",
                    },
                    new CourseAdmin
                    {
                        Level = "Degree",
                        Type = "Computing",
                        CourseName = "Computer Science",
                        UniversityName = "Asia Pacific University",
                        Location = "Petaling Jaya",
                        URL = "https://www.apu.edu.my",
                    }
                );
                contextAdmin.SaveChanges();
            }
        }
    }
}
