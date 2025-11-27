using System;
using System.Collections.Generic;
using System.Linq;
using CourseManagementSystem.Entities;

namespace CourseManagementSystem.Services
{
    public class CourseManagementService : ICourseManagementService
    {
        private readonly List<Course> _courses;

        public CourseManagementService()
        {
            _courses = new List<Course>();
        }

        public void AddCourse(Course course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course));

            if (_courses.Any(c => c.Title.Equals(course.Title, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException($"Курс с названием '{course.Title}' уже существует");

            _courses.Add(course);
        }

        public bool RemoveCourse(string courseTitle)
        {
            var course = _courses.FirstOrDefault(c => c.Title.Equals(courseTitle, StringComparison.OrdinalIgnoreCase));
            return course != null && _courses.Remove(course);
        }

        public bool AssignTeacherToCourse(string courseTitle, Teacher teacher)
        {
            var course = _courses.FirstOrDefault(c => c.Title.Equals(courseTitle, StringComparison.OrdinalIgnoreCase));
            if (course != null)
            {
                course.Teacher = teacher;
                return true;
            }
            return false;
        }

        public List<Course> GetCoursesByTeacher(Teacher teacher)
        {
            if (teacher == null)
                return new List<Course>();

            return _courses.Where(c => c.Teacher != null && c.Teacher.Equals(teacher)).ToList();
        }

        public List<Course> GetAllCourses()
        {
            return new List<Course>(_courses);
        }

        public Course GetCourseByTitle(string title)
        {
            return _courses.FirstOrDefault(c => c.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        }

        public void AddStudentToCourse(string courseTitle, Student student)
        {
            var course = GetCourseByTitle(courseTitle);
            if (course == null)
                throw new ArgumentException($"Курс с названием '{courseTitle}' не найден");

            course.AddStudent(student);
        }

        public bool RemoveStudentFromCourse(string courseTitle, Student student)
        {
            var course = GetCourseByTitle(courseTitle);
            return course?.RemoveStudent(student) ?? false;
        }

        public List<Student> GetStudentsInCourse(string courseTitle)
        {
            var course = GetCourseByTitle(courseTitle);
            return course?.Students ?? new List<Student>();
        }
    }
}