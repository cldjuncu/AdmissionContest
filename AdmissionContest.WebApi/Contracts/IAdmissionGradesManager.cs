using System.Collections.Generic;
using AdmissionContest.WebApi.Models;

namespace AdmissionContest.WebApi.Contracts
{
    public interface IAdmissionGradesManager
    {
        /// <summary>
        /// Computes the admission grade.
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        double GetAdmissionGrade(Student student);

        /// <summary>
        /// Computes the admission contest results.
        /// </summary>
        /// <param name="students"></param>
        /// <returns>A list of Result.</returns>
        List<Result> GetResults(List<Student> students);
    }
}
