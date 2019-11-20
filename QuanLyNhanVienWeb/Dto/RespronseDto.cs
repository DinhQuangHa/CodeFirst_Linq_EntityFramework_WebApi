using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace QuanLyNhanVienWeb.Dto
{
    public class RespronseDto
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ResponseMessage { get; set; }
        public string ErrorMessage { get; set; }
    }
}