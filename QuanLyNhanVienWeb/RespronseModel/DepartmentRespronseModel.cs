using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyNhanVienWeb.RespronseModel
{
    public class DepartmentRespronseModel
    {
        public Guid Id { get; set; }
        public string DepartmentName { get; set; }
        public string ManagePerson{ get; set; }
    }
}