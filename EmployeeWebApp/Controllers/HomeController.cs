using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer.Services;
using DataAccessLayer.Repository;
using DataAccessLayer.ViewModels;
using System.Threading.Tasks;
using DataAccessLayer.Entity;

namespace EmployeeWebApp.Controllers
{
    public class HomeController : Controller
    {
        private IEmployee iEmployee = new EmployeeRepository();
        private IDepartment iDepartment = new DepartmentRepository();
        public async Task<ActionResult> Index()
        {
            MasterEmployeeViewModel masterEmployeeViewModel = new MasterEmployeeViewModel()
            {
                employeeViewModel = await iEmployee.employeeViewModel(),
                deprtmentViewModel = new DepartmentViewModel(),
                employeeList = await iEmployee.GetEmployees(),
                departmentList = await iDepartment.GetDepartments()
            };
            return View(masterEmployeeViewModel);
        }

        public async Task<ActionResult> CreateAndEditEmployee(string employeeId)
        {
            if (string.IsNullOrEmpty(employeeId))
                return PartialView(await iEmployee.employeeViewModel());
            else
                return PartialView(await iEmployee.EditEmployee(employeeId));
        }
        public async Task<ActionResult> CreateAndEditDepartment(string departmentId)
        {
            if (string.IsNullOrEmpty(departmentId))
                return PartialView(new DepartmentViewModel());
            else
                return PartialView(await iDepartment.EditDepartment(departmentId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAndEditEmployee(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
                return PartialView("EmployeeList", await iEmployee.CreateAndEditEmployee(model));
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAndEditDepartment(DepartmentViewModel model)
        {
            if (ModelState.IsValid)
                return PartialView("DepartmentList", await iDepartment.CreateAndEditDepartment(model));
            return PartialView(model);
        }


        public async Task<ActionResult> DeleteDepartment(string departmentId)
        {
            if(string.IsNullOrEmpty(departmentId))
                return PartialView("DepartmentList", await iDepartment.GetDepartments());
            return PartialView(await iDepartment.DeleteDepartment(departmentId));
        }
        public async Task<ActionResult> ConfirmDeleteDepartment(string departmentId)
        {
            if (string.IsNullOrEmpty(departmentId))
                return PartialView("DepartmentList", await iDepartment.GetDepartments());
            return PartialView("DepartmentList", await iDepartment.ConfirmDeleteDepartment(departmentId));
        }
        public async Task<ActionResult> DeleteEmployee(string employeeId)
        {
            if (string.IsNullOrEmpty(employeeId))
                return PartialView("EmployeeList", await iEmployee.GetEmployees());
            return PartialView(await iEmployee.DeleteEmployee(employeeId));
        }
        public async Task<ActionResult> ConfirmDeleteEmployee(string employeeId)
        {
            if (string.IsNullOrEmpty(employeeId))
                return PartialView("EmployeeList", await iEmployee.GetEmployees());
            return PartialView("EmployeeList", await iEmployee.ConfirmDeleteEmplyee(employeeId));
        }
    }
}