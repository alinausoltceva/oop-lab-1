using CourseManagementSystem.Entities;
using CourseManagementSystem.Services;
using CourseManagementSystem.Factories;

class Program
{
    static void Main()
    {
        Console.WriteLine("==== СИСТЕМА УПРАВЛЕНИЯ УЧЕБНЫМИ КУРСАМИ ====\n");

        var courseService = new CourseManagementService();

        var mathTeacher = new Teacher("Александр", "Иванов", "Петрович",
            "Институт математики", "профессор");
        var languageTeacher = new Teacher("Мария", "Смирнова", "Ивановна",
            "Центр изучения иностранных языков", "доцент");
        var programmingTeacher = new Teacher("Дмитрий", "Кузнецов", "Сергеевич",
            "Факультет прикладной информатики", "старший преподаватель");

        var student1 = new Student("Анна", "Петрова", "Дмитриевна",
            123456, 2, "K3140", Gender.Female);
        var student2 = new Student("Иван", "Сидоров", "Алексеевич",
            123457, 2, "K3140", Gender.Male);
        var student3 = new Student("Екатерина", "Ковалева", "Сергеевна",
            123458, 3, "K3139", Gender.Female);

        var programmingCourse = CourseFactory.CreateOnlineCourse(
            "Алгоритмы и структуры данных",
            AssessmentType.Exam,
            4,
            "Факультет прикладной информатики",
            "Zoom",
            "https://itmo.zoom.us/j/89422695510?pwd=xKG0XVvCg3Y5Caq2USNua6DDc7kWZ4.1",
            true
        );

        var englishCourse = CourseFactory.CreateOfflineCourse(
            "Английский язык B2",
            AssessmentType.Test,
            2,
            "Центр изучения иностранных языков",
            "3315",
            "Ломоносова, 9",
            "Пт 15:30-17:00, 17:10-18:40"
        );

        var mathCourse = CourseFactory.CreateOfflineCourse(
            "Математический анализ",
            AssessmentType.Exam,
            5,
            "Институт математики",
            "1402",
            "Кронверкский пр., 49",
            "Вт 8:10-9:40, 9:50-11:20"
        );

        courseService.AddCourse(programmingCourse);
        courseService.AddCourse(englishCourse);
        courseService.AddCourse(mathCourse);

        courseService.AssignTeacherToCourse("Алгоритмы и структуры данных", programmingTeacher);
        courseService.AssignTeacherToCourse("Английский язык B2", languageTeacher);
        courseService.AssignTeacherToCourse("Математический анализ", mathTeacher);

        courseService.AddStudentToCourse("Алгоритмы и структуры данных", student1);
        courseService.AddStudentToCourse("Алгоритмы и структуры данных", student2);
        courseService.AddStudentToCourse("Английский язык B2", student1);
        courseService.AddStudentToCourse("Английский язык B2", student3);
        courseService.AddStudentToCourse("Математический анализ", student2);

        DisplayAllCourses(courseService);
        DisplayTeacherCourses(courseService, programmingTeacher);
        DisplayCourseStudents(courseService, "Алгоритмы и структуры данных");

        DemonstrateRemoval(courseService, student2, "Алгоритмы и структуры данных");
    }

    static void DisplayAllCourses(CourseManagementService service)
    {
        Console.WriteLine("ВСЕ КУРСЫ В СИСТЕМЕ:");
        Console.WriteLine(new string('═', 80));

        foreach (var course in service.GetAllCourses())
        {
            Console.WriteLine($" {course.Title}");
            Console.WriteLine($"   Формат: {course.Format}, Тип контроля: {course.AssessmentType}");
            Console.WriteLine($"   Зачетные единицы: {course.Credits}, Факультет: {course.Faculty}");
            Console.WriteLine($"   Преподаватель: {course.Teacher?.FullName ?? "Не назначен"}");
            Console.WriteLine($"   КОличество студентов: {course.Students.Count}");
            Console.WriteLine($"   {course.GetSpecificDetails()}");
            Console.WriteLine();
        }
    }

    static void DisplayTeacherCourses(CourseManagementService service, Teacher teacher)
    {
        Console.WriteLine($"КУРСЫ ПРЕПОДАВАТЕЛЯ {teacher.FullName}:");
        Console.WriteLine(new string('─', 60));

        var courses = service.GetCoursesByTeacher(teacher);
        foreach (var course in courses)
        {
            Console.WriteLine($"   • {course.Title} ({course.Format})");
        }
        Console.WriteLine();
    }

    static void DisplayCourseStudents(CourseManagementService service, string courseTitle)
    {
        Console.WriteLine($"СТУДЕНТЫ КУРСА '{courseTitle}':");
        Console.WriteLine(new string('─', 60));

        var students = service.GetStudentsInCourse(courseTitle);
        foreach (var student in students)
        {
            Console.WriteLine($"   • {student.FullName} (ISU: {student.ISU}, {student.CourseNumber} курс, гр. {student.GroupNumber})");
        }
        Console.WriteLine();
    }

    static void DemonstrateRemoval(CourseManagementService service, Student student, string courseTitle)
    {
        Console.WriteLine($"УДАЛЕНИЕ СТУДЕНТА:");
        Console.WriteLine(new string('─', 60));

        Console.WriteLine($"До удаления: {service.GetStudentsInCourse(courseTitle).Count} студентов");
        var removed = service.RemoveStudentFromCourse(courseTitle, student);
        Console.WriteLine($"Студент {student.FullName} удален: {removed}");
        Console.WriteLine($"После удаления: {service.GetStudentsInCourse(courseTitle).Count} студентов");
    }
}
