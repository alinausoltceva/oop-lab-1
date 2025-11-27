using Xunit;
using CourseManagementSystem.Entities;
using CourseManagementSystem.Services;
using CourseManagementSystem.Factories;

namespace CourseManagementSystem.Tests
{
    public class CourseManagementServiceTests
    {
        private readonly CourseManagementService _service;
        private readonly Teacher _teacher;
        private readonly Student _student;

        public CourseManagementServiceTests()
        {
            _service = new CourseManagementService();
            _teacher = new Teacher("Иван", "Петров", "Сергеевич",
                "Институт математики", "доцент");
            _student = new Student("Анна", "Сидорова", "Владимировна",
                123456, 2, "K3145", Gender.Female);
        }

        [Fact]
        public void AddCourse_ValidCourse_ShouldAddCourse()
        {
            var course = CourseFactory.CreateOnlineCourse(
                "Методы визуализации данных",
                AssessmentType.Exam,
                4,
                "Факультет прикладной информатики",
                "Zoom",
                "https://itmo.zoom.us/j/81365957482?pwd=J6XLaPBQpVGtw9e0VyAb5N5kk4r0r6.1",
                true
            );

            _service.AddCourse(course);

            var allCourses = _service.GetAllCourses();
            Assert.Single(allCourses);
            Assert.Equal("Методы визуализации данных", allCourses[0].Title);
        }

        [Fact]
        public void AddCourse_DuplicateCourse_ShouldThrowException()
        {

            var course1 = CourseFactory.CreateOfflineCourse(
                "Математический анализ",
                AssessmentType.Exam,
                5,
                "Институт математики",
                "1402",
                "Кронверский проспект, 49",
                "Пт 8:10-9:40"
            );

            var course2 = CourseFactory.CreateOfflineCourse(
                "Математический анализ",
                AssessmentType.Exam,
                5,
                "Институт математики",
                "2202",
                "Ломоносова, 9",
                "Пн 9:50-11:20"
            );

            _service.AddCourse(course1);

            Assert.Throws<InvalidOperationException>(() => _service.AddCourse(course2));
        }

        [Fact]
        public void RemoveCourse_ExistingCourse_ShouldReturnTrue()
        {
            var course = CourseFactory.CreateOnlineCourse(
                "Базы данных",
                AssessmentType.Test,
                3,
                "Факультет прикладной информатики",
                "Zoom",
                "https://yandex.zoom.us/j/93412482980",
                true
            );
            _service.AddCourse(course);

            var result = _service.RemoveCourse("Базы данных");

            Assert.True(result);
            Assert.Empty(_service.GetAllCourses());
        }

        [Fact]
        public void RemoveCourse_NonExistingCourse_ShouldReturnFalse()
        {
            var result = _service.RemoveCourse("Несуществующий курс");

            Assert.False(result);
        }

        [Fact]
        public void AssignTeacherToCourse_ValidAssignment_ShouldAssignTeacher()
        {
            var course = CourseFactory.CreateOfflineCourse(
                "Философия",
                AssessmentType.Test,
                2,
                "Центр социальных и гуманитарных наук",
                "2301",
                "Кронверский проспект, 49",
                "Вт 15:30-17:00"
            );
            _service.AddCourse(course);

            var result = _service.AssignTeacherToCourse("Философия", _teacher);

            Assert.True(result);
            var assignedCourse = _service.GetCourseByTitle("Философия");
            Assert.Equal(_teacher, assignedCourse.Teacher);
        }

        [Fact]
        public void GetCoursesByTeacher_ShouldReturnCorrectCourses()
        {
            var course1 = CourseFactory.CreateOnlineCourse(
                "Линейная алгебра",
                AssessmentType.Exam,
                4,
                "Институт математики",
                "Zoom",
                "https://itmo.zoom.us/j/87630312975?pwd=kY7dRw7K9ubRbUAfvhUVRatD2C8Trg.1",
                true
            );

            var course2 = CourseFactory.CreateOfflineCourse(
                "Теория веротяностей",
                AssessmentType.Exam,
                4,
                "Институт математики",
                "301",
                "Чайковского 11/2",
                "Ср 11:30-13:00"
            );

            _service.AddCourse(course1);
            _service.AddCourse(course2);
            _service.AssignTeacherToCourse("Линейная алгебра", _teacher);
            _service.AssignTeacherToCourse("Теория веротяностей", _teacher);

            var teacherCourses = _service.GetCoursesByTeacher(_teacher);

            Assert.Equal(2, teacherCourses.Count);
            Assert.Contains(teacherCourses, c => c.Title == "Линейная алгебра");
            Assert.Contains(teacherCourses, c => c.Title == "Теория веротяностей");
        }

        [Fact]
        public void AddStudentToCourse_ShouldAddStudent()
        {
            var course = CourseFactory.CreateOfflineCourse(
                "Английский язык C1",
                AssessmentType.Test,
                2,
                "Центр изучения иностранных языков",
                "3311",
                "Ломоносова, 9",
                "Сб 17:10-18:40"
            );
            _service.AddCourse(course);

            _service.AddStudentToCourse("Английский язык C1", _student);

            var students = _service.GetStudentsInCourse("Английский язык C1");
            Assert.Single(students);
            Assert.Equal(_student.ISU, students[0].ISU);
        }

        [Fact]
        public void RemoveStudentFromCourse_ShouldRemoveStudent()
        {
            var course = CourseFactory.CreateOnlineCourse(
                "Облачные технологии и услуги",
                AssessmentType.Exam,
                4,
                "Факультет прикладной информатики",
                "Яндекс телемост",
                "https://telemost.yandex.ru/j/64413096954470",
                true
            );
            _service.AddCourse(course);
            _service.AddStudentToCourse("Облачные технологии и услуги", _student);

            var result = _service.RemoveStudentFromCourse("Облачные технологии и услуги", _student);

            Assert.True(result);
            var students = _service.GetStudentsInCourse("Облачные технологии и услуги");
            Assert.Empty(students);
        }

        [Theory]
        [InlineData(AssessmentType.Exam, 5)]
        [InlineData(AssessmentType.Test, 3)]
        public void CourseCreation_WithDifferentParameters_ShouldWorkCorrectly(
            AssessmentType assessmentType, int credits)
        {
            var course = CourseFactory.CreateOnlineCourse(
                "Тестовый курс",
                assessmentType,
                credits,
                "Тестовый факультет",
                "Платформа",
                "https://test.com",
                true
            );

            Assert.Equal(assessmentType, course.AssessmentType);
            Assert.Equal(credits, course.Credits);
            Assert.Equal(CourseFormat.Online, course.Format);
        }
    }
}