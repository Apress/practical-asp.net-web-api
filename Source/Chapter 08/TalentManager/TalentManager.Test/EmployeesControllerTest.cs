using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using TalentManager.Data;
using TalentManager.Domain;
using TalentManager.Web.Controllers;
using TalentManager.Web.Models;

namespace TalentManager.Test
{
    [TestClass]
    public class EmployeesControllerTest
    {
        [TestMethod]
        public void MustReturnEmployeeForGetUsingAValidId()
        {
            // Arrange
            int id = 12345;
            var employee = new Employee() { Id = id, FirstName = "John", LastName = "Human" };

            IRepository<Employee> repository = MockRepository.GenerateMock<IRepository<Employee>>();
            repository.Stub(x => x.Find(id)).Return(employee);

            IUnitOfWork uow = MockRepository.GenerateMock<IUnitOfWork>();

            Mapper.CreateMap<Employee, EmployeeDto>();

            var controller = new EmployeesController(uow, repository, Mapper.Engine);
            controller.EnsureNotNull();

            // Act
            HttpResponseMessage response = controller.Get(id);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Content);
            Assert.IsInstanceOfType(response.Content, typeof(ObjectContent<EmployeeDto>));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var content = (response.Content as ObjectContent<EmployeeDto>);
            var result = content.Value as EmployeeDto;

            Assert.AreEqual(employee.Id, result.Id);
            Assert.AreEqual(employee.FirstName, result.FirstName);
            Assert.AreEqual(employee.LastName, result.LastName);
        }

        [TestMethod]
        public void MustReturn404WhenForGetUsingAnInvalidId()
        {
            // Arrange
            int invalidId = 12345;

            IRepository<Employee> repository = MockRepository.GenerateMock<IRepository<Employee>>();
            repository.Stub(x => x.Find(invalidId)).Return(null); // Simulate no match

            IUnitOfWork uow = MockRepository.GenerateMock<IUnitOfWork>();

            Mapper.CreateMap<Employee, EmployeeDto>();

            var controller = new EmployeesController(uow, repository, Mapper.Engine);
            controller.EnsureNotNull();

            // Act
            HttpResponseMessage response = null;
            try
            {
                response = controller.Get(invalidId);
                Assert.Fail();
            }
            catch (HttpResponseException ex)
            {
                // Assert
                Assert.AreEqual(HttpStatusCode.NotFound, ex.Response.StatusCode);
            }
        }

        [TestMethod]
        public void MustReturn201AndLinkForPost()
        {
            // Arrange
            int id = 12345;
            var employeeDto = new EmployeeDto() { Id = id, FirstName = "John", LastName = "Human" };
            string requestUri = "http://localhost:8086/api/employees/";
            Uri uriForNewEmployee = new Uri(new Uri(requestUri), id.ToString());

            IRepository<Employee> repository = MockRepository.GenerateMock<IRepository<Employee>>();
            repository.Expect(x => x.Insert(null)).IgnoreArguments().Repeat.Once();

            IUnitOfWork uow = MockRepository.GenerateMock<IUnitOfWork>();
            uow.Expect(x => x.Save()).Return(1).Repeat.Once();

            Mapper.CreateMap<EmployeeDto, Employee>();

            var controller = new EmployeesController(uow, repository, Mapper.Engine);
            controller.SetRequest("employees", HttpMethod.Post, requestUri);

            // Act
            HttpResponseMessage response = controller.Post(employeeDto);

            // Assert
            repository.VerifyAllExpectations();
            uow.VerifyAllExpectations();

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual(uriForNewEmployee, response.Headers.Location);
        }

    }
}
