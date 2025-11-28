namespace CourseManagementSystem.Entities
{
    public class OfflineCourse : Course
    {
        public string Classroom { get; set; }
        public string Building { get; set; }
        public string Schedule { get; set; }

        public OfflineCourse(string title, AssessmentType assessmentType, int credits,
                           string faculty, string classroom, string building, string schedule)
            : base(title, CourseFormat.Offline, assessmentType, credits, faculty)
        {
            Classroom = classroom;
            Building = building;
            Schedule = schedule;
        }

        public override string GetSpecificDetails()
        {
            return $"Аудитория: {Classroom}, Корпус: {Building}, Расписание: {Schedule}";
        }
    }
}