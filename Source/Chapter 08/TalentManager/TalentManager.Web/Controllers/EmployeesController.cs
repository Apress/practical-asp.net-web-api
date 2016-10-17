using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using TalentManager.Data;
using TalentManager.Domain;
using TalentManager.Web.Models;

namespace TalentManager.Web.Controllers
{
    //public class EmployeesController : ApiController
    //{
    //    //private Context context = new Context();

    //    private IContext context = null;

    //    public EmployeesController()
    //    {
    //        this.context = new Context();
    //    }

    //    public EmployeesController(IContext context)
    //    {
    //        this.context = context;
    //    }

    //    public HttpResponseMessage Get(int id)
    //    {
    //        var employee = context.Employees.FirstOrDefault(e => e.Id == id);
    //        if (employee == null)
    //        {
    //            var response = Request.CreateResponse(HttpStatusCode.NotFound, "Employee not found");

    //            throw new HttpResponseException(response);
    //        }

    //        return Request.CreateResponse<Employee>(HttpStatusCode.OK, employee);
    //    }


    //    protected override void Dispose(bool disposing)
    //    {
    //        //if (context != null)
    //        //    context.Dispose();

    //        if (context != null && context is IDisposable)
    //        {
    //            ((IDisposable)context).Dispose();
    //        }

    //        base.Dispose(disposing);
    //    }
    //}

    //public class EmployeesController : ApiController
    //{
    //    private readonly IEmployeeRepository repository = null;

    //    public EmployeesController()
    //    {
    //        this.repository = new EmployeeRepository();
    //    }

    //    public EmployeesController(IEmployeeRepository repository)
    //    {
    //        this.repository = repository;
    //    }

    //    public HttpResponseMessage Get(int id)
    //    {
    //        var employee = repository.Get(id);
    //        if (employee == null)
    //        {
    //            var response = Request.CreateResponse(HttpStatusCode.NotFound, "Employee not found");

    //            throw new HttpResponseException(response);
    //        }

    //        return Request.CreateResponse<Employee>(HttpStatusCode.OK, employee);
    //    }

    //    public HttpResponseMessage GetByDepartment(int departmentId)
    //    {
    //        var employees = repository.GetByDepartment(departmentId);
    //        if (employees != null && employees.Any())
    //        {
    //            return Request.CreateResponse<IEnumerable<Employee>>(HttpStatusCode.OK, employees);
    //        }

    //        throw new HttpResponseException(HttpStatusCode.NotFound);
    //    }

    //    protected override void Dispose(bool disposing)
    //    {
    //        if (repository != null)
    //            repository.Dispose();

    //        base.Dispose(disposing);
    //    }
    //}

    public class EmployeesController : ApiController
    {
        private readonly IUnitOfWork uow = null;
        private readonly IRepository<Employee> repository = null;
        private readonly IMappingEngine mapper = null;

        //public EmployeesController()
        //{
        //    uow = new UnitOfWork();
        //    repository = new Repository<Employee>(uow);
        //    mapper = Mapper.Engine;
        //}

        public EmployeesController(IUnitOfWork uow, IRepository<Employee> repository, IMappingEngine mapper)
        {
            this.uow = uow;
            this.repository = repository;
            this.mapper = mapper;
        }

        // Action methods go here
        [OptimisticLock]
        public HttpResponseMessage Get(int id)
        {
            var employee = repository.Find(id);
            if (employee == null)
            {
                var response = Request.CreateResponse(HttpStatusCode.NotFound, "Employee not found");

                throw new HttpResponseException(response);
            }

            //return Request.CreateResponse<Employee>(HttpStatusCode.OK, employee);

            return Request.CreateResponse<EmployeeDto>(
                        HttpStatusCode.OK,
                        mapper.Map<Employee, EmployeeDto>(employee));
        }

        public HttpResponseMessage GetByDepartment(int departmentId)
        {
            var employees = repository.All.Where(e => e.DepartmentId == departmentId);
            if (employees != null && employees.Any())
            {
                return Request.CreateResponse<IEnumerable<Employee>>(HttpStatusCode.OK, employees);
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        public HttpResponseMessage Post(EmployeeDto employeeDto)
        {
            var employee = mapper.Map<EmployeeDto, Employee>(employeeDto);

            repository.Insert(employee);
            uow.Save();

            var response = Request.CreateResponse<Employee>(HttpStatusCode.Created, employee);

            string uri = Url.Link("DefaultApi", new { id = employee.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        [OptimisticLock]
        [ConflictExceptionHandler]
        //public void Put(int id, Employee employee)
        //{
        //    repository.Update(employee);
        //    uow.Save();
        //}
        public void Put(int id, EmployeeDto employeeDto)
        {
            var employee = mapper.Map<EmployeeDto, Employee>(employeeDto);

            repository.Update(employee);
            uow.Save();
        }


        public void Delete(int id)
        {
            repository.Delete(id);
            uow.Save();
        }


        protected override void Dispose(bool disposing)
        {
            if (repository != null)
                repository.Dispose();

            if (uow != null)
                uow.Dispose();

            base.Dispose(disposing);
        }
    }

}
