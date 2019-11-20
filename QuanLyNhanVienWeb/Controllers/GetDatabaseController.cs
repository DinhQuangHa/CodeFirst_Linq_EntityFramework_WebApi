using QuanLyNhanVienDataBase;
using QuanLyNhanVienDataBase.Entities;
using QuanLyNhanVienWeb.RespronseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QuanLyNhanVienWeb.Controllers
{
    [RoutePrefix("Api/Manage")]
    public class GetDatabaseController : ApiController
    {
        private readonly QuanLyNhanVienDbContext _context = new QuanLyNhanVienDbContext();

        [HttpGet]
        [Route("Cau-7")]
        public List<EmployeeRespronseModel> GetEmployeeCau7()
        {
            var ListEmployee = (from e in _context.Employees
                                join d in _context.Departments on e.DepartmentId equals d.Id
                                where (d.DepartmentName == "Phong Kiem Thu") && 
                                (e.Salary > 1500000000 && e.Salary < 2000000000)
                                select new EmployeeRespronseModel
                                {
                                    Id = e.Id,
                                    Name = e.Name,
                                    Birthday = e.Birthday,
                                    Address = e.Address,
                                    Gender = e.Gender,
                                    Salary = e.Salary,
                                    Phone = e.Phone,
                                    Gmail = e.Gmail,
                                    Status = e.Status
                                }).ToList();
            return ListEmployee;
        }

        [HttpGet]
        [Route("Cau-4")]
        public List<EmployeeRespronseModel> GetEmployeeAddress()
        {
            var date = DateTime.Parse("1/1/2005");
            var luongtoithieu = 1200000000;
            var ListEmployee = (from e in _context.Employees
                                where (e.Address == "2958 Montana Place" ) || (e.Birthday > date && e.Salary > luongtoithieu)
                                select new EmployeeRespronseModel
                                {
                                    Id = e.Id,
                                    Name = e.Name,
                                    Birthday = e.Birthday,
                                    Address = e.Address,
                                    Gender = e.Gender,
                                    Salary = e.Salary,
                                }).ToList();
            return ListEmployee;
        }

        [HttpGet]
        [Route("Get-Salary")]
        public List<EmployeeRespronseModel> GetSalary()
        {
            var ListEmployee = (from e in _context.Employees
                                where e.Salary > 1700000000
                                select new EmployeeRespronseModel
                                {
                                    Name = e.Name,
                                    Birthday = e.Birthday,
                                    Address = e.Address,
                                    Gender = e.Gender,
                                    Salary = e.Salary
                                }).ToList();
            return ListEmployee;
        }

        [HttpGet]
        [Route("Get-List-Employee")]
        public List<EmployeeRespronseModel> GetListEmployee()
        {
            var ListEmployee = (from e in _context.Employees
                                orderby e.Salary ascending
                                select new EmployeeRespronseModel
                                {
                                    Name = e.Name,
                                    Birthday =  e.Birthday,
                                    Address = e.Address,
                                    Gender = e.Gender,
                                    Salary = e.Salary
                                }).ToList();
            return ListEmployee;
        }
    }
}
