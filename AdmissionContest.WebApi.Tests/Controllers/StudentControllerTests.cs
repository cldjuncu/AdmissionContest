using System.Collections.Generic;
using AdmissionContest.WebApi.Contracts;
using AdmissionContest.WebApi.Controllers;
using AdmissionContest.WebApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AdmissionContest.WebApi.Tests.Controllers
{
    [TestClass]
    public class StudentControllerTests
    {
        [TestMethod]
        public void TheDefaultCtor_ShouldCreateNewInstance()
        {
            //Arrange

            //Act
            var studentController = new StudentsController();

            //Assert
            Assert.IsNotNull(studentController);
        }

        [TestMethod]
        public void Add_ShouldComputeTheAdmissionGradeAndAddTheStudentToTheDb()
        {
            //Arrange
            var mockIAdmissionGradesManager = new Mock<IAdmissionGradesManager>();
            var student = new Student { Cnp = 1, FirstName = "John" };
            var students = new List<Student> { student };
            var admissionGrade = 8.5;
            mockIAdmissionGradesManager.Setup(manager => manager.GetAdmissionGrade(student)).Returns(admissionGrade);
            var studentsController = new StudentsController(mockIAdmissionGradesManager.Object);

            //Act
            studentsController.Add(students);

            //Assert
            mockIAdmissionGradesManager.Verify(manager => manager.GetAdmissionGrade(student), Times.Once());
            Assert.AreEqual(admissionGrade, student.AdmissionGrade);
        }
    }
}
