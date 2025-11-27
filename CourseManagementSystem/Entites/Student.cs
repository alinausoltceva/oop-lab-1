namespace CourseManagementSystem.Entities
{
	public class Student : Person
	{
		public int ISU { get; set; }
		public int CourseNumber { get; set; }
		public string GroupNumber { get; set; }
		public Gender Gender { get; set; }

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