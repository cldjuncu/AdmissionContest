using System.Collections.Generic;
using System.Linq;
using AdmissionContest.WebApi.Contracts;
using AdmissionContest.WebApi.Models;

namespace AdmissionContest.WebApi.Managers
{
    public class AdmissionGradesManager : IAdmissionGradesManager
    {
        #region IAdmissionGradeManager members

        double IAdmissionGradesManager.GetAdmissionGrade(Student student)
        {
            return ComputeAdmissionGrade(student);
        }

        List<Result> IAdmissionGradesManager.GetResults(List<Student> students)
        {
            return GetResults(students);
        }

        #endregion

        #region Private methods

        private double ComputeAdmissionGrade(Student student)
        {
            var grades = new List<double> { student.MathBacGrade, student.InfoBacGrade, student.ExamGrade };

            var admissionGrade = student.ExamGrade / 2 + student.BacAverageGrade / 4 + grades.Max() / 4;

            return admissionGrade;
        }

        private List<Result> GetResults(List<Student> students)
        {
            var orderedStudents = students.OrderByDescending(s => s.AdmissionGrade).ToList();
            var results = new List<Result>();

            foreach (var student in orderedStudents)
            {
                var result = new Result
                {
                    AdmissionGrade = student.AdmissionGrade,
                    Cnp = student.Cnp,
                    FirstName = student.FirstName,
                    LastName = student.LastName
                };

                var index = orderedStudents.IndexOf(student);
                //var sameAdmissionGradeStudents = orderedStudents.Select(s => s.AdmissionGrade == student.AdmissionGrade);
                //if (sameAdmissionGradeStudents != null)
                //{
                    
                //}
                if (index < 2)
                {
                    result.Clasification = Categories.Budget;
                }
                else if (index < 4) {
                    result.Clasification = Categories.Tax;
                }
                else
                {
                    result.Clasification = Categories.Rejected;
                }

                results.Add(result);
            }

            return results;
        }

        #endregion
    }
}
