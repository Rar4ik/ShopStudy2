using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebApi.Employees)]
    public class EmployeesApiController : ControllerBase, IEmployeesData
    {
        private readonly IEmployeesData _EmployeesData;

        public EmployeesApiController(IEmployeesData employeesData)
        {
            _EmployeesData = employeesData;
        }

        [HttpGet, ActionName("Get")]
        public IEnumerable<EmployeeView> GetAll()
        {
            return _EmployeesData.GetAll();
        }

        [HttpGet("{id}"), ActionName("Get")]
        public EmployeeView GetById(int id)
        {
            return _EmployeesData.GetById(id);
        }

        [HttpPost, ActionName("Post")]
        public void Add([FromBody] EmployeeView Employee)
        {
            _EmployeesData.Add(Employee);
        }

        [HttpPut("{id}"), ActionName("Put") ]
        public EmployeeView Edit(int id, [FromBody] EmployeeView Employee)
        {
           return _EmployeesData.Edit(id, Employee);
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _EmployeesData.Delete(id);
        }

        [NonAction]
        public void SaveChanges()
        {
            _EmployeesData.SaveChanges();
        }
    }
}
