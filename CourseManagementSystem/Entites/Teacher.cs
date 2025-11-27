namespace CourseManagementSystem.Entities
{
    public class Teacher : Person
    {
        public string Department { get; set; }
        public string AcademicDegree { get; set; }

        public Teacher(string firstName, string lastName, string middleName,
                      string department, string academicDegree)
            : base(firstName, lastName, middleName)
        {
            Department = department;
            AcademicDegree = academicDegree;
        }

        public override string ToString()
        {
            return $"{AcademicDegree} {FullName} ({Department})";
        }
    }
}