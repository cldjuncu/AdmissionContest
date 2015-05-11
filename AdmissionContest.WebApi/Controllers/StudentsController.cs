using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.Http;
using AdmissionContest.WebApi.Contracts;
using AdmissionContest.WebApi.Managers;
using AdmissionContest.WebApi.Models;

namespace AdmissionContest.WebApi.Controllers
{
    public class StudentsController : ApiController
    {
        #region Constructors

        public StudentsController()
        {
            admissionGradesManager = new AdmissionGradesManager();
        }

        public StudentsController(IAdmissionGradesManager admissionGradeManager)
        {
            admissionGradesManager = admissionGradeManager;
        }

        #endregion

        #region Actions

        [HttpPost]
        [ActionName("Add")]
        public void Add([FromBody]List<Student> students)
        {
            using (StreamWriter file = new StreamWriter(@"D:\Master\Anul 1\Sem 2\CSS\AdmissionContestDb\students.txt", true))
            {
                foreach (var student in students)
                {
                    student.AdmissionGrade = admissionGradesManager.GetAdmissionGrade(student);
                    var dbStudent = ConvertStudentToString(student);
                    file.WriteLine(dbStudent);
                }
            }
        }

        #endregion

        #region Private methods

        private string ConvertStudentToString(Student student)
        {
            var dbStudent = new StringBuilder("");
            dbStudent.Append("Cnp=").Append(student.Cnp.ToString()).Append(" ");
            dbStudent.Append("FirstName=").Append(student.FirstName).Append(" ");
            dbStudent.Append("LastName=").Append(student.LastName).Append(" ");
            dbStudent.Append("ExamGrade=").Append(student.ExamGrade.ToString(CultureInfo.InvariantCulture)).Append(" ");
            dbStudent.Append("MathBacGrade=").Append(student.MathBacGrade.ToString(CultureInfo.InvariantCulture)).Append(" ");
            dbStudent.Append("InfoBacGrade=").Append(student.InfoBacGrade.ToString(CultureInfo.InvariantCulture)).Append(" ");
            dbStudent.Append("BacAverageGrade=").Append(student.BacAverageGrade.ToString(CultureInfo.InvariantCulture)).Append(" ");
            dbStudent.Append("AdmissionGrade=").Append(student.AdmissionGrade.ToString(CultureInfo.InvariantCulture));
            return dbStudent.ToString();
        }

        #endregion

        #region Private members

        private IAdmissionGradesManager admissionGradesManager;

        #endregion
    }
}
