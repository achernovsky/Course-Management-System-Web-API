using Course_Management_System_API.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course_Management_System_API.Data
{
    public interface IApplicationRepo
    {
        Task<bool> SaveChanges();
        Task<IEnumerable<Course>> GetAllCourses();
        Task<Course> GetCourse(int id);
        Task CreateCourse(Course course);
        Task DeleteCourse(int id);
        Task<StudentCourse> AddStudentToCourse(int courseId, string studentId);
        Task<IEnumerable<ApplicationUser>> GetStudentsInCourse(int id);
        Task<IEnumerable<Lecture>> AddLecturesToCourse(int courseId, Lecture lecture);
        Task<IEnumerable<Attendance>> GetStudentAttendance(string studentId);
        Task<IEnumerable<Attendance>> GetLectureAttendance(int courseId, int lectureId);
        Task<ApplicationUser> GetStudent(string id);
        Task DeleteStudent(string id);
        Task<Attendance> AddAttendanceToLecture(
            string studentId, int courseId, int lectureId, Attendance attendance);
    }
}
