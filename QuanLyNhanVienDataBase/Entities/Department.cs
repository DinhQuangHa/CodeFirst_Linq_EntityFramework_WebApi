using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLyNhanVienDataBase.Entities
{
    public class Department : BaseEntities
    {
        [StringLength(365)]
        public string DepartmentName { get; set; }
        public Guid ManagerPerson {get;set;}

        public ICollection<Employee> Employees { get; set; }
    }
}
