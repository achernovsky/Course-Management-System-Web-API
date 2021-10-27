using Course_Management_System_API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course_Management_System_API.Data
{
    public class ApplicationRepo : IApplicationRepo
    {
        private readonly CourseManagementContext _context;

        public ApplicationRepo(CourseManagementContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public async Task<bool> SaveChanges()
        {
            var created = await _context.SaveChangesAsync();
            return created > 0;
        }
        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<Course> GetCourse(int id)
        {
            return await _context.Courses.FindAsync(id);
        }
        public async Task CreateCourse(Course course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course));
            await _context.Courses.AddAsync(course);
        }

        public async Task DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            _context.Courses.Remove(course);
            await SaveChanges();
        }

        public async Task<StudentCourse> AddStudentToCourse(int courseId, string studentId)
        {
            var course = await _context.Courses.FindAsync(courseId);
            var student = await _context.ApplicationUsers.FindAsync(studentId);
            var studentCourses = await _context.StudentCourses.ToListAsync();

            if (course == null || student == null)
                return null;

            var studentCourseExist = studentCourses.Where<StudentCourse>(c => c.CourseId == courseId && c.StudentId == studentId);
            if (studentCourseExist == null || !studentCourseExist.Any())
            {
                var studentCourse = new StudentCourse { CourseId = courseId, StudentId = studentId };

                await _context.StudentCourses.AddAsync(studentCourse);
                return studentCourse;
            }
            else
                throw new Exception("Student is already registered to this course");
        }

        public async Task<IEnumerable<ApplicationUser>> GetStudentsInCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return null;

            var students = _context.ApplicationUsers;
            var studentCourses = _context.StudentCourses;
            var matchingStudentCourses = studentCourses.Where<StudentCourse>(c => c.CourseId == id).ToList();
            var matchingStudents = new List<ApplicationUser>();

            foreach (StudentCourse c in matchingStudentCourses)
            {
                var student = await students.FindAsync(c.StudentId);
                matchingStudents.Add(student);
            }

            return matchingStudents;
        }

        public async Task<IEnumerable<Lecture>> AddLecturesToCourse(int courseId, Lecture lecture)
        {
            var course = await _context.Courses.FindAsync(courseId);

            if (course == null)
                return null;

            DateTime start = (DateTime)course.StartDate;
            DateTime end = (DateTime)course.EndDate;

            var lectureDates = Enumerable
                .Range(start.DayOfYear, end.Subtract(start).Days + 1)
                .Select(n => start.AddDays(n - start.DayOfYear))
                .Where(d => d.DayOfWeek.ToString() == lecture.Day);

            var lectures = new List<Lecture>();

            foreach (var l in lectureDates)
            {
                Lecture newLecture = new Lecture
                {
                    CourseId = courseId,
                    Day = lecture.Day,
                    StartTime = lecture.StartTime,
                    EndTime = lecture.EndTime,
                    Date = l
                };
                _context.Lectures.Add(newLecture);
                lectures.Add(newLecture);
            }

            return lectures;
        }

        public async Task<IEnumerable<Attendance>> GetStudentAttendance(string id)
        {
            var attendances = await _context.Attendances.ToListAsync();
            var studentAttendances = attendances.Where(a => a.StudentId == id).ToList();
            return studentAttendances;
        }

        public async Task<IEnumerable<Attendance>> GetLectureAttendance(int courseId, int lectureId)
        {
            var course = await _context.Courses.FindAsync(courseId);
            var lecture = await _context.Lectures.FindAsync(lectureId);
            var attendances = await _context.Attendances.ToListAsync();

            if (course == null)
                throw new Exception("Course doesn't exist");
            if (lecture == null)
                throw new Exception("Lecture doesn't exist");
            if (lecture.CourseId != courseId)
                throw new Exception("No such lecture in this course");

            var lectureAttendances = attendances.Where(a => a.LectureId == lectureId).ToList();
            return lectureAttendances;
        }

        public async Task<ApplicationUser> GetStudent(string id)
        {
            return await _context.ApplicationUsers.FindAsync(id);
        }

        public async Task DeleteStudent(string id)
        {
            var student = await _context.ApplicationUsers.FindAsync(id);
            _context.ApplicationUsers.Remove(student);
            await SaveChanges();
        }

        public async Task<Attendance> AddAttendanceToLecture(
           string studentId, int courseId, int lectureId, Attendance attendance)
        {
            ApplicationUser student = await _context.ApplicationUsers.FindAsync(studentId);
            Course course = await _context.Courses.FindAsync(courseId);
            Lecture lecture = await _context.Lectures.FindAsync(lectureId);
            var sc = _context.StudentCourses.Where(s => s.CourseId == courseId && s.StudentId == studentId);

            if (student == null || course == null || lecture == null)
                return null;
            if (sc == null || !sc.Any())
                throw new Exception("The student is not registered for this course");

            Attendance newAttendance = new Attendance
            {
                StudentId = studentId,
                LectureId = lectureId,
                IsAttended = attendance.IsAttended,
                Note = attendance.Note
            };

            _context.Attendances.Add(newAttendance);
            return newAttendance;
        }
    }
}
