using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QuanLyNhanVienDataBase;
using QuanLyNhanVienDataBase.Entities;
using QuanLyNhanVienWeb.Dto;
using QuanLyNhanVienWeb.Helper;
using QuanLyNhanVienWeb.RespronseModel;

namespace QuanLyNhanVienWeb.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly QuanLyNhanVienDbContext _context = new QuanLyNhanVienDbContext();
        #region Method
        private Employee GetEmployeeById(Guid Id)
        {
            if (Id == null)
                return null;
            else
            {
                var employee = _context.Employees.FirstOrDefault(c => c.Id == Id);
                return employee;
            }
        }

        private Employee GetEmployeeByDeleted(DeleteIdDto dto)
        {
            if (dto.Id == Guid.Empty)
                return null;

            var employee = GetEmployeeById(dto.Id);
            return employee;
        }
        #endregion

        #region Api

        [HttpGet]
        [Route("get-Employee-by-id/{id}")]
        public Employee GetEmployeeByIdApi(Guid Id)
        {
            if (Id != Guid.Empty)
            {
                return GetEmployeeById(Id);
            }
            return null;
        }

        [HttpGet]
        [Route("get-Employee")]
        public List<Employee> GetEmployees() // khong dung duoc employee nen dung emploỷeeRespronModel
        {
            var employees = (from e in _context.Employees select e).ToList();
            return employees;
        }

        [HttpPost]
        [Route("delete-list-Employee")]
        public HttpResponseMessage DeleteEmployees(List<DeleteIdDto> dtos)
        {
            if (dtos != null)
            {
                var employees = new List<Employee>();
                foreach (var item in dtos)
                {
                    var employee = GetEmployeeByDeleted(item);
                    if (employee != null)
                    {
                        employees.Add(employee);
                    }
                }

                if (employees.Count > 0)
                {
                    _context.Employees.RemoveRange(employees);
                    _context.SaveChanges();

                    return RespronMessageHelper.ResponseMessage(Request, new RespronseDto
                    {
                        StatusCode = HttpStatusCode.OK,
                        ErrorMessage = "",
                        ResponseMessage = "Delete Employee success"
                    });
                }

                return RespronMessageHelper.ResponseMessage(Request, new RespronseDto
                {
                    StatusCode = HttpStatusCode.OK,
                    ErrorMessage = "",
                    ResponseMessage = "Can't find Employee with list Id"
                });
            }

            return RespronMessageHelper.ResponseMessage(Request, new RespronseDto
            {
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessage = "",
                ResponseMessage = "Parammeter is not invalid"
            });
        }

        [HttpPost]
        [Route("delete-Employee")]
        public HttpResponseMessage DeleteDepartment(DeleteIdDto dto)
        {
            var employee = GetEmployeeByDeleted(dto);
            // Xoa Cung
            if (employee == null)
            {
                return RespronMessageHelper.ResponseMessage(Request, new RespronseDto
                {
                    StatusCode = HttpStatusCode.OK,
                    ErrorMessage = "",
                    ResponseMessage = "Can't not find Employee with list Id " + dto.Id
                });
            }
            _context.Employees.Remove(employee);
            _context.SaveChanges();

            return RespronMessageHelper.ResponseMessage(Request, new RespronseDto
            {
                StatusCode = HttpStatusCode.OK,
                ErrorMessage = "",
                ResponseMessage = "Delete Employee Width Id " + dto.Id + " success"
            });
        }

        [HttpPost]
        [Route("add-new-Employee")]
        public HttpResponseMessage AddNewOrEditEmployee(EmployeeDto dto)
        {
            if (dto == null)
            {
                return RespronMessageHelper.ResponseMessage(Request, new RespronseDto
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessage = "Parammeter null",
                    ResponseMessage = ""
                });
            }

            if (dto.Id != Guid.Empty)
            {
                var employee = GetEmployeeById(dto.Id);
                if (employee != null)
                {
                    employee.Name = dto.Name;
                    employee.Birthday = dto.Birthday;
                    employee.Address = dto.Address;
                    employee.Salary = dto.Salary;
                    employee.Phone = dto.Phone;
                    employee.Gmail = dto.Gmail;
                    employee.Gender = dto.Gender;
                    employee.Status = dto.Status;
                    employee.DepartmentId = dto.DepartmentId;
                    employee.UpdateDate = DateTime.Now;
                }
                else
                {
                    return RespronMessageHelper.ResponseMessage(Request, new RespronseDto
                    {
                        StatusCode = HttpStatusCode.OK,
                        ErrorMessage = "",
                        ResponseMessage = "Can't Find Employee Width Id" + dto.Id
                    });
                }
            }
            else
            {
                var employee = new Employee()
                {
                    Name = dto.Name,
                    Birthday = dto.Birthday,
                    Address = dto.Address,
                    Salary = dto.Salary,
                    Phone = dto.Phone,
                    Gmail = dto.Gmail,
                    Gender = dto.Gender,
                    Status = dto.Status,
                    DepartmentId = dto.DepartmentId
            };
                _context.Employees.Add(employee);
            }
            _context.SaveChanges();
            return RespronMessageHelper.ResponseMessage(Request, new RespronseDto
            {
                StatusCode = HttpStatusCode.OK,
                ErrorMessage = "",
                ResponseMessage = "Add Employee success"
            });
        }
        #endregion
    }
}
