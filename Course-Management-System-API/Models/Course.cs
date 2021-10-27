using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
namespace Course_Management_System_API.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        public DateTime? EndDate { get; set; }

        public virtual List<Lecture> Lectures { get; set; }
    }
}