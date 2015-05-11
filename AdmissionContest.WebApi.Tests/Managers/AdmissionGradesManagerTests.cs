using System.Collections.Generic;
using System.Linq;
using AdmissionContest.WebApi.Contracts;
using AdmissionContest.WebApi.Managers;
using AdmissionContest.WebApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdmissionContest.WebApi.Tests.Managers
{
    [TestClass]
    public class AdmissionGradesManagerTests
    {
        [TestMethod]
        public void ComputeAdmissionGrade_ShouldUpdateTheStudent()
        {
            //Arrange
            var student = new Student { BacAverageGrade = 8, ExamGrade = 7.75, InfoBacGrade = 9.25, MathBacGrade = 8.5 };
            var expectedAdmissionGrade = student.ExamGrade / 2 + student.BacAverageGrade / 4 + student.InfoBacGrade / 4;
            IAdmissionGradesManager admissionGradesManager = new AdmissionGradesManager();

            //Act
            var actualAdmissionGrade = admissionGradesManager.GetAdmissionGrade(student);

            //Assert
            Assert.AreEqual(expectedAdmissionGrade, actualAdmissionGrade);
        }

        [TestMethod]
        public void GetResults_ShouldGetTheAdmissionContestResults()
        {
            //Arrange
            var student1 = new Student { AdmissionGrade = 8.5, Cnp = 1, FirstName = "John", LastName = "Doe" };
            var student2 = new Student { AdmissionGrade = 7.75, Cnp = 2, FirstName = "Clark", LastName = "Kent" };
            var student3 = new Student { AdmissionGrade = 10, Cnp = 3, FirstName = "Alex", LastName = "Smith" };
            var student4 = new Student { AdmissionGrade = 6.25, Cnp = 4, FirstName = "Thomas", LastName = "Jones" };
            var student5 = new Student { AdmissionGrade = 9, Cnp = 5, FirstName = "Taylor", LastName = "Wilson" };

            var result1 = new Result { AdmissionGrade = 8.5, Clasification = Categories.Tax, Cnp = 1, FirstName = "John", LastName = "Doe" };
            var result2 = new Result { AdmissionGrade = 7.75, Clasification = Categories.Tax, Cnp = 2, FirstName = "Clark", LastName = "Kent" };
            var result3 = new Result { AdmissionGrade = 10, Clasification = Categories.Budget, Cnp = 3, FirstName = "Alex", LastName = "Smith" };
            var result4 = new Result { AdmissionGrade = 6.25, Clasification = Categories.Rejected, Cnp = 4, FirstName = "Thomas", LastName = "Jones" };
            var result5 = new Result { AdmissionGrade = 9, Clasification = Categories.Budget, Cnp = 5, FirstName = "Taylor", LastName = "Wilson" };

            var students = new List<Student>{student1, student2, student3, student4, student5};
            var expectedResults = new List<Result> { result1, result2, result3, result4, result5 };
            expectedResults = expectedResults.OrderByDescending(r => r.AdmissionGrade).ToList();
            IAdmissionGradesManager admissionGradesManager = new AdmissionGradesManager();

            //Act
            var actualResults = admissionGradesManager.GetResults(students);

            //Assert
            Assert.IsNotNull(actualResults);
            Assert.AreEqual(expectedResults.Count, actualResults.Count);
            Assert.AreEqual(expectedResults[0].AdmissionGrade, actualResults[0].AdmissionGrade);
            Assert.AreEqual(expectedResults[0].Clasification, actualResults[0].Clasification);
            Assert.AreEqual(expectedResults[0].Cnp, actualResults[0].Cnp);
            Assert.AreEqual(expectedResults[0].FirstName, actualResults[0].FirstName);
            Assert.AreEqual(expectedResults[0].LastName, actualResults[0].LastName);
        }
    }
}