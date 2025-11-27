using System.Collections.Generic;
using CourseManagementSystem.Entities;

namespace CourseManagementSystem.Services
{
    public interface ICourseManagementService
    {
        void AddCourse(Course course);
        bool RemoveCourse(string courseTitle);
        bool AssignTeacherToCourse(string courseTitle, Teacher teacher);
        List<Course> GetCoursesByTeacher(Teacher teacher);
        List<Course> GetAllCourses();
        Course GetCourseByTitle(string title);
        void AddStudentToCourse(string courseTitle, Student student);
        bool RemoveStudentFromCourse(string courseTitle, Student student);
        List<Student> GetStudentsInCourse(string courseTitle);
    }
}