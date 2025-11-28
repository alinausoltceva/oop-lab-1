namespace CourseManagementSystem.Entities
{
    public abstract class Person
    {
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string MiddleName { get; protected set; }

        public string FullName => $"{LastName} {FirstName} {MiddleName}";

        protected Person(string firstName, string lastName, string middleName)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
        }
    }
}