using DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataAccessLayer.ViewModels
{
    public class MasterEmployeeViewModel
    {
        public EmployeeViewModel employeeViewModel { get; set; }
        public List<EmployeeViewModel> employeeList { get; set; }
        public DepartmentViewModel deprtmentViewModel { get; set; }
        public List<Department> departmentList { get; set; }
    }
    public class EmployeeViewModel
    {
        public string EmployeeId { get; set; }

        [Required(ErrorMessage = "First name Required")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name Required")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Designation { get; set; }
        public IEnumerable<SelectListItem> DesignationList { get; set; }
        
        [Required(ErrorMessage = "Salary Required")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Salary Must be Numeric")]
        public int Salary { get; set; }

        public string DepartmentName { get; set; }

        [Required(ErrorMessage = "Deparment Required")]
        [Display(Name = "Department")]
        public string DepartmentId { get; set; }
        public IEnumerable<SelectListItem> DepartmentList { get; set; }
    }
    public class DepartmentViewModel
    {
        public string DepartmentId { get; set; }

        [Required(ErrorMessage = "Department name Required")]
        [Display(Name = "Department Name")]
        public string DepatmentName { get; set; }
    }
}
