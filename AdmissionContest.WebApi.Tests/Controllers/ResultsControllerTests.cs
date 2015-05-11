using System.Collections.Generic;
using AdmissionContest.WebApi.Contracts;
using AdmissionContest.WebApi.Controllers;
using AdmissionContest.WebApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AdmissionContest.WebApi.Tests.Controllers
{
    [TestClass]
    public class ResultsControllerTests
    {
        [TestMethod]
        public void TheDefaultCtor_ShouldCreateNewInstance()
        {
            //Arrange

            //Act
            var resultsController = new ResultsController();

            //Assert
            Assert.IsNotNull(resultsController);
        }

        [TestMethod]
        public void Get_ShouldGetTheAdmissionContestResults()
        {
            //Arrange
            var mockIAdmissionGradesManager = new Mock<IAdmissionGradesManager>();
            var student = new Student { AdmissionGrade = 8.5, Cnp = 1, FirstName = "John", LastName = "Doe" };
            var students = new List<Student> { student };

            var result = new Result { AdmissionGrade = 8.5, Clasification = Categories.Budget, Cnp = 1, FirstName = "John", LastName = "Doe" };
            var expectedResults = new List<Result> { result };

            mockIAdmissionGradesManager.Setup(manager => manager.GetResults(students)).Returns(expectedResults);
            var resultsController = new ResultsController(mockIAdmissionGradesManager.Object);

            //Act
            var actualResults = resultsController.Get();

            //Assert
            Assert.IsNotNull(actualResults);
            Assert.AreEqual(expectedResults.Count, actualResults.Count);
            mockIAdmissionGradesManager.Verify(mock => mock.GetResults(students), Times.Once());
        }
    }
}
