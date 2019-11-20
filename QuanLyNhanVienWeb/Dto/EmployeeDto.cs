using QuanLyNhanVienDataBase.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyNhanVienWeb.Dto
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }
        public string Phone { get; set; }
        public string Gmail { get; set; }
        public string Gender { get; set; }
        public Status Status { get; set; }
        public Guid DepartmentId { get; set; }
    }
}