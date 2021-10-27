using Course_Management_System_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Course_Management_System_API.Data;
using Microsoft.AspNetCore.Authorization;

namespace Course_Management_System_API.Controllers
{
    [Authorize(Roles = UserRoles.Professor)]
    [ApiController]
    [Route("/professors")]
    public class ProfessorsController : ControllerBase
    { 
        private IApplicationRepo _repo;

        public ProfessorsController(IApplicationRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("courses")]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _repo.GetAllCourses();
            return Ok(courses);
        }

        [HttpGet("courses/{id}", Name = "GetCourse")]
        public async Task<IActionResult> GetCourse(int id)
        {
            var course = await _repo.GetCourse(id);
            if (course == null)
                return NotFound();
            return Ok(course);
        }

        [HttpPost("courses")]
        public async Task<ActionResult<Course>> CreateCourse(Course course)
        {
            if (ModelState.IsValid)
                await _repo.CreateCourse(course);
            else
                return BadRequest();

            var created = await _repo.SaveChanges();
            if (created)
                return CreatedAtRoute(nameof(GetCourse), new { Id = course.Id }, course);
            else
                return BadRequest();
        }

        [HttpDelete("courses/{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            await _repo.DeleteCourse(id);
            return NoContent();
        }

        [HttpPost("courses/{courseId}/addStudent/{studentId}")]
        public async Task<ActionResult<StudentCourse>> AddStudentToCourse(int courseId, string studentId)
        {
            var studentCourse = await _repo.AddStudentToCourse(courseId, studentId);
            if (studentCourse == null)
                return NotFound();
            var created = await _repo.SaveChanges();
            if (created)
                return Ok(studentCourse);
            else
                return BadRequest();
        }

        [HttpGet("courses/{id}/students")]
        public async Task<IActionResult> GetStudentsInCourse(int id)
        {
            var matchingStudents = await _repo.GetStudentsInCourse(id);
            if (matchingStudents == null)
                return NotFound();
            return Ok(matchingStudents);
        }

        [HttpPost("courses/{courseId}/addLectures")]
        public async Task<ActionResult<Lecture>> AddLecturesToCourse(int courseId, [FromBody] Lecture lecture)
        {
            if (ModelState.IsValid)
            {
                var lectures = await _repo.AddLecturesToCourse(courseId, lecture);
                if (lectures == null)
                    return NotFound();
                var created = await _repo.SaveChanges();
                if (created)
                    return Ok(lectures);
                else
                    return BadRequest();
            }
            else
                return BadRequest();
        }

        [HttpGet("students/{id}/attendances")]
        public async Task<IActionResult> GetStudentAttendance(string id)
        {
            var studentAttendances = await _repo.GetStudentAttendance(id);
            if (studentAttendances == null)
                return NoContent();
            return Ok(studentAttendances);
        }

        [HttpGet("courses/{courseId}/lectures/{lectureId}/attendances")]
        public async Task<IActionResult> GetLectureAttendance(int courseId, int lectureId)
        {
            var studentAttendances = await _repo.GetLectureAttendance(courseId, lectureId);
            if (studentAttendances == null)
                return NoContent();
            return Ok(studentAttendances);
        }


        [HttpGet("students/{id}", Name = "GetStudent")]
        public async Task<IActionResult> GetStudent(string id)
        {
            var student = await _repo.GetStudent(id);
            if (student == null)
                return NotFound();
            return Ok(student);
        }

        [HttpDelete("students/{id}")]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            await _repo.DeleteStudent(id);
            return NoContent();
        }
    }
}
