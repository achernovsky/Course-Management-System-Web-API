using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Course_Management_System_API.Models
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Name = "Basic Programming", StartDate = new DateTime(2021, 7, 15), EndDate = new DateTime(2021, 10, 15) },
                new Course { Id = 2, Name = "Advanced Programming", StartDate = new DateTime(2021, 10, 16), EndDate = new DateTime(2022, 1, 16) }
            );
        }
    }
}
