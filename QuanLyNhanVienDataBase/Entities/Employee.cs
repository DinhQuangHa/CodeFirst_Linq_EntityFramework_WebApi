using QuanLyNhanVienDataBase.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyNhanVienDataBase.Entities
{
    public class Employee : BaseEntities
    {
        [StringLength(250)]
        [Required]
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }
        public string Phone { get; set; }
        public string Gmail { get; set; }
        public string Gender { get; set; }
        public Status Status { get; set; }

        #region Relate
        public ICollection<JoinBuiding> JoinBuidings { get; set; }

        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
        public Guid DepartmentId { get; set; }
        #endregion
    }
}
