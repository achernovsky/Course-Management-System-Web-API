using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Course_Management_System_API.Models
{
    public class Lecture
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        [Required(ErrorMessage = "Day is required")]
        [RegularExpression(@"^((Monday)|(Tuesday)|(Wednesday)|(Thursday)|(Friday)|(Saturday)|(Sunday))$",
         ErrorMessage = "Name of the day should be a full name")]
        public string Day { get; set; }

        [Required(ErrorMessage = "Start mime is required")]
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$",
         ErrorMessage = "Time should be HH:MM")]
        public string StartTime { get; set; }

        [Required(ErrorMessage = "End time is required")]
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$",
         ErrorMessage = "Time should be HH:MM")]
        public string EndTime { get; set; }

        public DateTime Date { get; set; }

        public virtual List<Attendance> Attendances { get; set; }
    }
}
