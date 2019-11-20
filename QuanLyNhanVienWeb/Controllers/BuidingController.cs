using QuanLyNhanVienDataBase;
using QuanLyNhanVienDataBase.Entities;
using QuanLyNhanVienWeb.Dto;
using QuanLyNhanVienWeb.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QuanLyNhanVienWeb.Controllers
{
    public class BuidingController : ApiController
    {
        private readonly QuanLyNhanVienDbContext _context = new QuanLyNhanVienDbContext();
        #region Method
        private Buiding GetBuidingById(Guid Id)
        {
            if (Id == null)
                return null;
            else
            {
                var buiding = _context.Buidings.FirstOrDefault(c => c.Id == Id);
                return buiding;
            }
        }

        private Buiding GetBuidingByDeleted(DeleteIdDto dto)
        {
            if (dto.Id == Guid.Empty)
                return null;

            var department = GetBuidingById(dto.Id);
            return department;
        }
        #endregion

        #region Api

        [HttpGet]
        [Route("get-Buiding-by-id/{id}")]
        public Buiding GetBuidingByIdApi(Guid Id)
        {
            if (Id != Guid.Empty)
            {
                return GetBuidingById(Id);
            }
            return null;
        }

        [HttpGet]
        [Route("get-Buiding")]
        public List<Buiding> GetBuidings() // khong dung duoc buiding nen dung buidingRespronModel
        {
            var Buiding = (from b in _context.Buidings select b).ToList();
            return Buiding;
        }

        [HttpPost]
        [Route("delete-list-Buiding")]
        public HttpResponseMessage DeleteBuidings(List<DeleteIdDto> dtos)
        {
            if (dtos != null)
            {
                var buidings = new List<Buiding>();
                foreach (var item in dtos)
                {
                    var buiding = GetBuidingByDeleted(item);
                    if (buiding != null)
                    {
                        buidings.Add(buiding);
                    }
                }

                if (buidings.Count > 0)
                {
                    _context.Buidings.RemoveRange(buidings);
                    _context.SaveChanges();

                    return RespronMessageHelper.ResponseMessage(Request, new RespronseDto
                    {
                        StatusCode = HttpStatusCode.OK,
                        ErrorMessage = "",
                        ResponseMessage = "Delete Buiding success"
                    });
                }

                return RespronMessageHelper.ResponseMessage(Request, new RespronseDto
                {
                    StatusCode = HttpStatusCode.OK,
                    ErrorMessage = "",
                    ResponseMessage = "Can't find Buiding with list Id"
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
        [Route("delete-Buiding")]
        public HttpResponseMessage DeleteBuiding(DeleteIdDto dto)
        {
            var buiding = GetBuidingByDeleted(dto);
            // Xoa Cung
            if (buiding == null)
            {
                return RespronMessageHelper.ResponseMessage(Request, new RespronseDto
                {
                    StatusCode = HttpStatusCode.OK,
                    ErrorMessage = "",
                    ResponseMessage = "Can't not find Buiding with list Id " + dto.Id
                });
            }
            _context.Buidings.Remove(buiding);
            _context.SaveChanges();

            return RespronMessageHelper.ResponseMessage(Request, new RespronseDto
            {
                StatusCode = HttpStatusCode.OK,
                ErrorMessage = "",
                ResponseMessage = "Delete Buiding Width Id " + dto.Id + " success"
            });
        }

        [HttpPost]
        [Route("add-new-buiding")]
        public HttpResponseMessage AddNewOrEditBuiding(BuidingDto dto)
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
                var buiding = GetBuidingById(dto.Id);
                if (buiding != null)
                {
                    buiding.BuidingName = dto.BuidingName;
                    buiding.StartDate = dto.StartDate;
                    buiding.EndDate = dto.EndDate;
                    buiding.Address = dto.Address;
                    buiding.UpdateDate = DateTime.Now;
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
                var buiding = new Buiding()
                {
                    BuidingName = dto.BuidingName,
                    StartDate = dto.StartDate,
                    EndDate = dto.EndDate,
                    Address = dto.Address,
            };
                _context.Buidings.Add(buiding);
            }
            _context.SaveChanges();
            return RespronMessageHelper.ResponseMessage(Request, new RespronseDto
            {
                StatusCode = HttpStatusCode.OK,
                ErrorMessage = "",
                ResponseMessage = "Add Buiding success"
            });
        }

        #endregion
    }
}
