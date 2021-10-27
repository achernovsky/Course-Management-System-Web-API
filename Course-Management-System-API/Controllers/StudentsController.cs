using Course_Management_System_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;
using Course_Management_System_API.Data;

namespace Course_Management_System_API.Controllers
{
    [Authorize(Roles = UserRoles.Student)]
    [Route("/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private IApplicationRepo _repo;

        public StudentsController(IApplicationRepo repo)
        {
            _repo = repo;
        }

        [HttpPost("{studentId}/courses/{courseId}/lectures/{lectureId}/addAttendance")]
        public async Task<ActionResult<Attendance>> AddAttendanceToLecture(
            string studentId, int courseId, int lectureId, [FromBody] Attendance attendance)
        {
            var newAttendance = await _repo.AddAttendanceToLecture(studentId, courseId, lectureId, attendance);
            if (newAttendance == null)
                return NotFound();
            else
            {
                var created = await _repo.SaveChanges();
                if (created)
                    return Ok(newAttendance);
                else
                    return BadRequest();
            }
        }
    }
}
