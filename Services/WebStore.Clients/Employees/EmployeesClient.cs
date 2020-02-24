using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Employees
{
   public class EmployeesClient : BaseClient, IEmployeesData
    {
        public EmployeesClient(IConfiguration config) : base(config, WebApi.Employees)
        {
        }
        public IEnumerable<EmployeeView> GetAll() => Get<List<EmployeeView>>(_ServiceAdress);

        public EmployeeView GetById(int id) => Get<EmployeeView>($"{_ServiceAdress}/{id}");

        public void Add(EmployeeView Employee) => Post(_ServiceAdress, Employee);

        public EmployeeView Edit(int id, EmployeeView Employee)
        {
            var response = Put($"{_ServiceAdress}/{id}", Employee);
            return response.Content.ReadAsAsync<EmployeeView>().Result;
        }

        public bool Delete(int id) => Delete($"{_ServiceAdress}/{id}").IsSuccessStatusCode;

        public void SaveChanges() { }
    }
}
