using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Course_Management_System_API.Models
{
    public class StudentCourse
    {
        public string StudentId { get; set; }
        public int CourseId { get; set; }
    }
}
