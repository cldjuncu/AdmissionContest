using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web.Http;
using AdmissionContest.WebApi.Contracts;
using AdmissionContest.WebApi.Managers;
using AdmissionContest.WebApi.Models;

namespace AdmissionContest.WebApi.Controllers
{
    public class ResultsController : ApiController
    {
        #region Constructors

        public ResultsController()
        {
            this.admissionGradesManager = new AdmissionGradesManager();
        }

        public ResultsController(IAdmissionGradesManager admissionGradesManager)
        {
            this.admissionGradesManager = admissionGradesManager;
        }

        #endregion

        #region Actions

        [ActionName("Get")]
        [HttpGet]
        public List<Result> Get()
        {
            var students = GetStudentsFromDatabase();
            var results = admissionGradesManager.GetResults(students);

            AddResultsToDatabase(results);

            return results;
        }

        #endregion

        #region Private methods

        private List<Student> GetStudentsFromDatabase()
        {
            var students = new List<Student>();

            using (System.IO.StreamReader studentsFile = new System.IO.StreamReader(@"D:\Master\Anul 1\Sem 2\CSS\AdmissionContestDb\students.txt"))
            {
                string line;
                while ((line = studentsFile.ReadLine()) != null)
                {
                    var student = ConvertStringToStudent(line);
                    students.Add(student);
                }
            }

            return students;
        }

        private void AddResultsToDatabase(List<Result> results)
        {
            using (System.IO.StreamWriter resultsFile = new System.IO.StreamWriter(@"D:\Master\Anul 1\Sem 2\CSS\AdmissionContestDb\results.txt", false))
            {

                foreach (var result in results)
                {
                    var dbResult = ConvertResultToString(result);
                    resultsFile.WriteLine(dbResult);
                }
            }
        }

        private Student ConvertStringToStudent(string line)
        {
            var student = new Student();
            string[] fields = line.Split(' ');
            student.Cnp = Convert.ToInt64(fields[0].Split('=')[1]);
            student.FirstName = fields[1].Split('=')[1];
            student.LastName = fields[2].Split('=')[1];
            student.ExamGrade = Convert.ToDouble(fields[3].Split('=')[1]);
            student.MathBacGrade = Convert.ToDouble(fields[4].Split('=')[1]);
            student.InfoBacGrade = Convert.ToDouble(fields[5].Split('=')[1]);
            student.BacAverageGrade = Convert.ToDouble(fields[6].Split('=')[1]);
            student.AdmissionGrade = Convert.ToDouble(fields[7].Split('=')[1]);
            return student;
        }

        private string ConvertResultToString(Result result)
        {
            var dbResult = new StringBuilder("");
            dbResult.Append("Cnp=").Append(result.Cnp.ToString()).Append(" ");
            dbResult.Append("FirstName=").Append(result.FirstName).Append(" ");
            dbResult.Append("LastName=").Append(result.LastName).Append(" ");
            dbResult.Append("AdmissionGrade=").Append(result.AdmissionGrade.ToString(CultureInfo.InvariantCulture)).Append(" ");
            dbResult.Append("Classification=").Append(result.Clasification);
            return dbResult.ToString();
        }

        #endregion

        #region Private members

        private IAdmissionGradesManager admissionGradesManager;

        #endregion
    }
}
