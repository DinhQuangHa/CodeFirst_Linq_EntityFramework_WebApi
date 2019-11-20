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
    public class JoinBuidingController : ApiController
    {
        private readonly QuanLyNhanVienDbContext _context = new QuanLyNhanVienDbContext();
        #region Method
        private JoinBuiding GetJoinBuidingById(Guid Id)
        {
            if (Id == null)
                return null;
            else
            {
                var joinBuiding = _context.JoinBuidings.FirstOrDefault(c => c.Id == Id);
                return joinBuiding;
            }
        }

        private JoinBuiding GetJoinBuidingByDeleted(DeleteIdDto dto)
        {
            if (dto.Id == Guid.Empty)
                return null;

            var joinBuiding = GetJoinBuidingById(dto.Id);
            return joinBuiding;
        }
        #endregion

        #region Api

        [HttpGet]
        [Route("get-JoinBuiding-by-id/{id}")]
        public JoinBuiding GetJoinBuidingByIdApi(Guid Id)
        {
            if (Id != Guid.Empty)
            {
                return GetJoinBuidingById(Id);
            }
            return null;
        }

        [HttpGet]
        [Route("get-JoinBuiding")]
        public List<JoinBuiding> GetJoinBuidings()
        {
            var joinBuidings = (from j in _context.JoinBuidings
                              select j).ToList();
            return joinBuidings;
        }

        [HttpPost]
        [Route("delete-list-JoinBuiding")]
        public HttpResponseMessage DeleteJoinBuidings(List<DeleteIdDto> dtos)
        {
            if (dtos != null)
            {
                var joinBuidings = new List<JoinBuiding>();
                foreach (var item in dtos)
                {
                    var joinBuiding = GetJoinBuidingByDeleted(item);
                    if (joinBuiding != null)
                    {
                        joinBuidings.Add(joinBuiding);
                    }
                }

                if (joinBuidings.Count > 0)
                {
                    _context.JoinBuidings.RemoveRange(joinBuidings);
                    _context.SaveChanges();

                    return RespronMessageHelper.ResponseMessage(Request, new RespronseDto
                    {
                        StatusCode = HttpStatusCode.OK,
                        ErrorMessage = "",
                        ResponseMessage = "Delete JoinBuiding success"
                    });
                }

                return RespronMessageHelper.ResponseMessage(Request, new RespronseDto
                {
                    StatusCode = HttpStatusCode.OK,
                    ErrorMessage = "",
                    ResponseMessage = "Can't find JoinBuiding with list Id"
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
        [Route("delete-JoinBuiding")]
        public HttpResponseMessage DeleteJoinBuiding(DeleteIdDto dto)
        {
            var joinBuiding = GetJoinBuidingByDeleted(dto);
            // Xoa Cung
            if (joinBuiding == null)
            {
                return RespronMessageHelper.ResponseMessage(Request, new RespronseDto
                {
                    StatusCode = HttpStatusCode.OK,
                    ErrorMessage = "",
                    ResponseMessage = "Can't not find JoinBuiding with list Id " + dto.Id
                });
            }
            _context.JoinBuidings.Remove(joinBuiding);
            _context.SaveChanges();

            return RespronMessageHelper.ResponseMessage(Request, new RespronseDto
            {
                StatusCode = HttpStatusCode.OK,
                ErrorMessage = "",
                ResponseMessage = "Delete JoinBuiding Width Id " + dto.Id + " success"
            });
        }

        [HttpPost]
        [Route("add-new-JoinBuiding")]
        public HttpResponseMessage AddNewOrEditJoinBuiding(JoinBuidingDto dto)
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
                var joinBuiding = GetJoinBuidingById(dto.Id);
                if (joinBuiding != null)
                {
                    joinBuiding.WorkDate = dto.WorkDate;
                    joinBuiding.EmployeeId = dto.EmployeeId;
                    joinBuiding.BuidingId = dto.EmployeeId;
                }
                else
                {
                    return RespronMessageHelper.ResponseMessage(Request, new RespronseDto
                    {
                        StatusCode = HttpStatusCode.OK,
                        ErrorMessage = "",
                        ResponseMessage = "Can't Find JoinBuiding Width Id" + dto.Id
                    });
                }
            }
            else
            {
                var joinBuiding = new JoinBuiding()
                {
                    WorkDate = dto.WorkDate,
                    EmployeeId = dto.EmployeeId,
                    BuidingId = dto.EmployeeId
                };
                _context.JoinBuidings.Add(joinBuiding);
            }
            _context.SaveChanges();
            return RespronMessageHelper.ResponseMessage(Request, new RespronseDto
            {
                StatusCode = HttpStatusCode.OK,
                ErrorMessage = "",
                ResponseMessage = "Add JoinBuiding success"
            });
        }

        #endregion
    }
}
