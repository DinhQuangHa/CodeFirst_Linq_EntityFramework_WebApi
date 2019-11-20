using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLyNhanVienDataBase.Entities
{
    public class Buiding : BaseEntities
    {
        [StringLength(2000)]
        [Required]
        public string BuidingName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Address { get; set; }

        #region Relate
        public ICollection<JoinBuiding> JoinBuidings { get; set; }
        #endregion
    }
}
