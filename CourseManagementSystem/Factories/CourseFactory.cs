using CourseManagementSystem.Entities;

namespace CourseManagementSystem.Factories
{
    public static class CourseFactory
    {
        public static OnlineCourse CreateOnlineCourse(string title, AssessmentType assessmentType,
            int credits, string faculty, string platform, string meetingLink, bool hasRecordings)
        {
            return new OnlineCourse(title, assessmentType, credits, faculty, platform, meetingLink, hasRecordings);
        }

        public static OfflineCourse CreateOfflineCourse(string title, AssessmentType assessmentType,
            int credits, string faculty, string classroom, string building, string schedule)
        {
            return new OfflineCourse(title, assessmentType, credits, faculty, classroom, building, schedule);
        }
    }
}