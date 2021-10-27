using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Course_Management_System_API.Models
{
    public class CourseManagementContext : IdentityDbContext
    {
        public CourseManagementContext(DbContextOptions<CourseManagementContext> options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Course>().HasMany(c => c.Lectures);
            modelBuilder.Entity<Lecture>().HasMany(l => l.Attendances);
            modelBuilder.Entity<StudentCourse>().HasKey(s => new { s.StudentId, s.CourseId });

            modelBuilder.Seed();
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
