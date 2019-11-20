using QuanLyNhanVienDataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QuanLyNhanVienDataBase;
using QuanLyNhanVienWeb.Dto;
using QuanLyNhanVienWeb.Helper;

namespace QuanLyNhanVienWeb.Controllers
{
    [RoutePrefix("Api/Manage")]
    public class DepartmentController : ApiController
    {
        private readonly QuanLyNhanVienDbContext _context = new QuanLyNhanVienDbContext();
        #region Method
        private Department GetDepartmentById(Guid Id)
        {
            if (Id == null)
                return null;
            else
            {
                var department = _context.Departments.FirstOrDefault(c => c.Id == Id);
                return department;
            }
        }

        private Department GetDepartmentByDeleted(DeleteIdDto dto)
        {
            if (dto.Id == Guid.Empty)
                return null;

            var department = GetDepartmentById(dto.Id);
            return department;
        }
        #endregion

        #region Api

        [HttpGet]
        [Route("get-Departments-by-id/{id}")]
        public Department GetDepartmentByIdApi(Guid Id)
        {
            if (Id != Guid.Empty)
            {
                return GetDepartmentById(Id);
            }
            return null;
        }

        [HttpGet]
        [Route("get-Departments")]
        public List<Department> GetDepartments() // khong dung duoc Department nen dung DepartmentRespronModel
        {
            var Department = (from d in _context.Departments select d).ToList();
            return Department;
        }

        [HttpPost]
        [Route("delete-list-Department")]
        public HttpResponseMessage DeleteDepartments(List<DeleteIdDto> dtos)
        {
            if (dtos != null)
            {
                var departments = new List<Department>();
                foreach (var item in dtos)
                {
                    var department = GetDepartmentByDeleted(item);
                    if (department != null)
                    {
                        departments.Add(department);
                    }
                }

                if (departments.Count > 0)
                {
                    _context.Departments.RemoveRange(departments);
                    _context.SaveChanges();

                    return RespronMessageHelper.ResponseMessage(Request, new RespronseDto
                    {
                        StatusCode = HttpStatusCode.OK,
                        ErrorMessage = "",
                        ResponseMessage = "Delete department success"
                    });
                }

                return RespronMessageHelper.ResponseMessage(Request, new RespronseDto
                {
                    StatusCode = HttpStatusCode.OK,
                    ErrorMessage = "",
                    ResponseMessage = "Can't find department with list Id"
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
        [Route("delete-Department")]
        public HttpResponseMessage DeleteDepartment(DeleteIdDto dto)
        {
            var department = GetDepartmentByDeleted(dto);
            // Xoa Cung
            if (department == null)
            {
                return RespronMessageHelper.ResponseMessage(Request, new RespronseDto
                {
                    StatusCode = HttpStatusCode.OK,
                    ErrorMessage = "",
                    ResponseMessage = "Can't not find department with list Id " + dto.Id
                });
            }
            _context.Departments.Remove(department);
            _context.SaveChanges();

            return RespronMessageHelper.ResponseMessage(Request, new RespronseDto
            {
                StatusCode = HttpStatusCode.OK,
                ErrorMessage = "",
                ResponseMessage = "Delete department Width Id " + dto.Id + " success"
            });
        }

        [HttpPost]
        [Route("add-new-department")]
        public HttpResponseMessage AddNewOrEditDepartment(DepartmentDto dto)
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
                var department = GetDepartmentById(dto.Id);
                if (department != null)
                {
                    department.DepartmentName = dto.DepartmentName;
                    department.ManagerPerson = dto.ManagerPerson;
                    department.UpdateDate = DateTime.Now;
                }
                else
                {
                    return RespronMessageHelper.ResponseMessage(Request, new RespronseDto
                    {
                        StatusCode = HttpStatusCode.OK,
                        ErrorMessage = "",
                        ResponseMessage = "Can't Find Department Width Id" + dto.Id
                    });
                }
            }
            else
            {
                var department = new Department()
                {
                    DepartmentName = dto.DepartmentName,
                    ManagerPerson = dto.ManagerPerson
                };
                _context.Departments.Add(department);
            }
            _context.SaveChanges();
            return RespronMessageHelper.ResponseMessage(Request, new RespronseDto
            {
                StatusCode = HttpStatusCode.OK,
                ErrorMessage = "",
                ResponseMessage = "Add Department success"
            });
        }

        #endregion
    }
}
