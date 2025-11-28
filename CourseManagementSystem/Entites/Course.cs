using System.Collections.Generic;

namespace CourseManagementSystem.Entities
{
    public abstract class Course
    {
        public string Title { get; private set; }
        public CourseFormat Format { get; private set; }
        public AssessmentType AssessmentType { get; private set; }
        public int Credits { get; private set; }
        public string Faculty { get; private set; }
        public Teacher Teacher { get; private set; }
        public List<Student> Students { get; private set; }

        protected Course(string title, CourseFormat format, AssessmentType assessmentType,
                        int credits, string faculty)
        {
            Title = title;
            Format = format;
            AssessmentType = assessmentType;
            Credits = credits;
            Faculty = faculty;
            Students = new List<Student>();
        }

        public virtual void AddStudent(Student student)
        {
            if (!Students.Contains(student))
            {
                Students.Add(student);
            }
        }

        public virtual bool RemoveStudent(Student student)
        {
            return Students.Remove(student);
        }

        public virtual string GetCourseInfo()
        {
            return $"{Title} ({Format}), {AssessmentType}, {Credits} кредитов, Факультет: {Faculty}";
        }

        public abstract string GetSpecificDetails();

        public override string ToString()
        {
            return $"{GetCourseInfo()} | {GetSpecificDetails()}";
        }
    }
}