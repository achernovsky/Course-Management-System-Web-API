using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course_Management_System_API.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public int LectureId { get; set; }
        public bool IsAttended { get; set; }
        public string Note { get; set; }

    }
}
