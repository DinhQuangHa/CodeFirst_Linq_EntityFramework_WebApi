using QuanLyNhanVienWeb.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace QuanLyNhanVienWeb.Helper
{
    public class RespronMessageHelper
    {
        public static HttpResponseMessage ResponseMessage(HttpRequestMessage request, RespronseDto dto)
        {
            return request.CreateResponse(dto.StatusCode, new RespronseDto
            {
                StatusCode = dto.StatusCode,
                ErrorMessage = dto.ErrorMessage,
                ResponseMessage = dto.ResponseMessage
            });
        }
    }
}