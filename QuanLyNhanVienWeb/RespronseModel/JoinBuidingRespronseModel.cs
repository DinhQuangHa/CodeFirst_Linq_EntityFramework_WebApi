using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyNhanVienWeb.RespronseModel
{
    public class JoinBuidingRespronseModel
    {
        public Guid Id { get; set; }
        public int WorkDate { get; set; }
        public Guid BuidingId { get; set; }
        public Guid EmployeeId { get; set; }
    }
}