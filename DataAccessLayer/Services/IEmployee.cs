using DataAccessLayer.Entity;
using DataAccessLayer.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Services
{
    public interface IEmployee
    {

        Task<List<EmployeeViewModel>> GetEmployees();
        Task<EmployeeViewModel> DeleteEmployee(string employeeId);
        Task<EmployeeViewModel> EditEmployee(string employeeId);
        Task<List<EmployeeViewModel>> CreateAndEditEmployee(EmployeeViewModel model);
        Task<List<EmployeeViewModel>> ConfirmDeleteEmplyee(string EmployeeId);
        Task<EmployeeViewModel> employeeViewModel();
    }
}
