namespace CourseManagementSystem.Entities
{
    public class OnlineCourse : Course
    {
        public string Platform { get; set; }
        public string MeetingLink { get; set; }
        public bool HasRecordings { get; set; }

        public OnlineCourse(string title, AssessmentType assessmentType, int credits,
                          string faculty, string platform, string meetingLink, bool hasRecordings)
            : base(title, CourseFormat.Online, assessmentType, credits, faculty)
        {
            Platform = platform;
            MeetingLink = meetingLink;
            HasRecordings = hasRecordings;
        }

        public override string GetSpecificDetails()
        {
            return $"Платформа: {Platform}, Ссылка: {MeetingLink}, Записи: {(HasRecordings ? "Да" : "Нет")}";
        }
    }
}