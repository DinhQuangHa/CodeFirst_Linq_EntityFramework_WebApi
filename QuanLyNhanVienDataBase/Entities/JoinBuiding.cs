using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyNhanVienDataBase.Entities
{
    public class JoinBuiding : BaseEntities
    {
        public int WorkDate { get; set; }

        #region Relate
        [ForeignKey("BuidingId")]
        public Buiding Buiding { get; set; }
        public Guid BuidingId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public Guid EmployeeId { get; set; }
        #endregion
    }
}
