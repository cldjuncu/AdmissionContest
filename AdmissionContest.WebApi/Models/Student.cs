namespace AdmissionContest.WebApi.Models
{
    public class Student
    {
        public long Cnp { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double ExamGrade { get; set; }
        public double MathBacGrade { get; set; }
        public double InfoBacGrade { get; set; }
        public double BacAverageGrade { get; set; }
        public double AdmissionGrade { get; set; }
    }
}