namespace CourseManagementSystem.Entities
{
	public class Student : Person
	{
		public int ISU { get; private set; }
		public int CourseNumber { get; private set; }
		public string GroupNumber { get; private set; }
		public Gender Gender { get; private set; }

		public Student(string firstName, string lastName, string middleName,
					  int isu, int courseNumber, string groupNumber, Gender gender)
			: base(firstName, lastName, middleName)
		{
			ISU = isu;
			CourseNumber = courseNumber;
			GroupNumber = groupNumber;
			Gender = gender;
		}

		public override string ToString()
		{
			return $"{FullName} (ISU: {ISU}, {CourseNumber} курс, группа {GroupNumber})";
		}
	}
}