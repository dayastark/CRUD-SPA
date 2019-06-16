using DataAccessLayer.Entity;
using DataAccessLayer.Services;
using DataAccessLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataAccessLayer.Repository
{
    public class EmployeeRepository : IEmployee
    {
        private EmployeeDbEntities context = new EmployeeDbEntities();
        public async Task<List<EmployeeViewModel>> ConfirmDeleteEmplyee(string employeeId)
        {
            Employee employee = await context.Employees.FindAsync(employeeId);
            if (employee != null)
            {
                context.Employees.Remove(employee);
                await context.SaveChangesAsync();
            }
            return await GetEmployees();
        }
        public async Task<List<EmployeeViewModel>> CreateAndEditEmployee(EmployeeViewModel model)
        {
            Employee employee;
            if (string.IsNullOrEmpty(model.EmployeeId))
            {
                // Create Employee
                employee = new Employee()
                {
                    EmployeeId = Guid.NewGuid().ToString(),
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Salary = model.Salary,
                    Designation = model.Designation,
                    DepartmentId = model.DepartmentId
                };
                context.Employees.Add(employee);
                await context.SaveChangesAsync();
            }
            else
            {
                employee = await context.Employees.FindAsync(model.EmployeeId);
                if(employee != null)
                {
                    employee.FirstName = model.FirstName;
                    employee.LastName = model.LastName;
                    employee.Salary = model.Salary;
                    employee.Designation = model.Designation;
                    employee.DepartmentId = model.DepartmentId;
                    context.Entry(employee).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }
            }
            return await GetEmployees();
        }
        public async Task<EmployeeViewModel> DeleteEmployee(string employeeId)
        {
            return await EditEmployee(employeeId);
        }
        public async Task<EmployeeViewModel> EditEmployee(string employeeId)
        {
            Employee employee = await context.Employees.FindAsync(employeeId);
            if(employee != null)
            {
                return new EmployeeViewModel()
                {
                    EmployeeId = employee.EmployeeId,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Salary = Convert.ToInt32(employee.Salary),
                    DepartmentName = employee.Department != null ? employee.Department.DepartmentName : "",
                    DepartmentId = employee.DepartmentId,
                    Designation = employee.Designation,
                    DepartmentList = await DepartmentList(),
                    DesignationList = DesignationList()
                };
            }
            return new EmployeeViewModel();
        }
        public async Task<List<EmployeeViewModel>> GetEmployees()
        {
            var employeeList = await context.Employees.ToListAsync();
            List<EmployeeViewModel> employeeViewModels = new List<EmployeeViewModel>();
            if (employeeList.Count == 0)
                return employeeViewModels;
            foreach(var item in employeeList)
            {
                string departmentName = string.Empty;
                Department department = await context.Departments.FindAsync(item.DepartmentId);
                if (department != null)
                    departmentName = department.DepartmentName;
                else
                    departmentName = "";
                EmployeeViewModel employeeViewModel = new EmployeeViewModel()
                {
                    EmployeeId = item.EmployeeId,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Salary = Convert.ToInt32(item.Salary),
                    Designation = item.Designation,
                    DepartmentId = departmentName
                };
                employeeViewModels.Add(employeeViewModel);
            }
            return employeeViewModels;
        }
        private async Task<IEnumerable<SelectListItem>> DepartmentList()
        {
            var list = await context.Departments.OrderBy(m => m.DepartmentName).ToListAsync();
            return new SelectList(list, "departmentId", "departmentName");
        }
        private IEnumerable<SelectListItem> DesignationList()
        {
            List<SelectListItem> designations =  new List<SelectListItem>()
            {
                new SelectListItem(){ Text = ".NET Developer", Value= "NET Developer" },
                new SelectListItem(){ Text = "Java Developer", Value= "Java Developer" },
                new SelectListItem(){ Text = "Python Developer", Value= "Python Developer" },
            };
            return designations;
        }
        public async Task<EmployeeViewModel> employeeViewModel()
        {
            return new EmployeeViewModel()
            {
                DesignationList = DesignationList(),
                DepartmentList = await DepartmentList()
            };
        }
    }
}
