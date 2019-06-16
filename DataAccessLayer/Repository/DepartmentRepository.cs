using DataAccessLayer.Entity;
using DataAccessLayer.Services;
using DataAccessLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class DepartmentRepository : IDepartment
    {
        private EmployeeDbEntities context = new EmployeeDbEntities();
        public async Task<List<Department>> ConfirmDeleteDepartment(string departmentId)
        {
            Department department = await context.Departments.FindAsync(departmentId);
            if (department != null)
            {
                context.Departments.Remove(department);
                await context.SaveChangesAsync();
            }
            return await GetDepartments();
        }
        public async Task<List<Department>> CreateAndEditDepartment(DepartmentViewModel model)
        {
            if (string.IsNullOrEmpty(model.DepartmentId))
            {
                // Create Department
                Department department = new Department()
                {
                    DepartmentId = Guid.NewGuid().ToString(),
                    DepartmentName = model.DepatmentName,
                };
                context.Departments.Add(department);
                await context.SaveChangesAsync();
            }
            else
            {
                // Edit Department
                Department department = await context.Departments.FindAsync(model.DepartmentId);
                if (department != null)
                {
                    department.DepartmentName = model.DepatmentName;
                    context.Entry(department).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }
            }
            return await GetDepartments();
        }
        public async Task<DepartmentViewModel> DeleteDepartment(string departmentId)
        {
            return await EditDepartment(departmentId);
        }
        public async Task<DepartmentViewModel> EditDepartment(string departmentId)
        {
            Department department = await context.Departments.FindAsync(departmentId);
            if(department != null)
            {
                return new DepartmentViewModel() {
                    DepartmentId = department.DepartmentId,
                    DepatmentName = department.DepartmentName
                };
            }
            return new DepartmentViewModel();
        }
        public async Task<List<Department>> GetDepartments()
        {
            return await context.Departments.ToListAsync();
        }
    }
}
