using DataAccessLayer.Entity;
using DataAccessLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services
{
    public interface IDepartment
    {
        Task<List<Department>> GetDepartments();
        Task<DepartmentViewModel> DeleteDepartment(string departmentId);
        Task<DepartmentViewModel> EditDepartment(string departmentId);
        Task<List<Department>> CreateAndEditDepartment(DepartmentViewModel model);
        Task<List<Department>> ConfirmDeleteDepartment(string departmentId);
    }
}
