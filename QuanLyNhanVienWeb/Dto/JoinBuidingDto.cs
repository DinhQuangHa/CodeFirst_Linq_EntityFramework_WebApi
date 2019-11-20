using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyNhanVienWeb.Dto
{
    public class JoinBuidingDto
    {
        public Guid Id { get; set; }
        public int WorkDate { get; set; }
        public Guid BuidingId { get; set; }
        public Guid EmployeeId { get; set; }
    }
}