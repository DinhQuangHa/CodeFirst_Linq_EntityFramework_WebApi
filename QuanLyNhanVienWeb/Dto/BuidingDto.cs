using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyNhanVienWeb.Dto
{
    public class BuidingDto
    {
        public Guid Id { get; set; }
        public string BuidingName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Address { get; set; }
    }
}