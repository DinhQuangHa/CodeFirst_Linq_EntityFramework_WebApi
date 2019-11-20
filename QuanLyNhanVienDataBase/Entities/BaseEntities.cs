using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLyNhanVienDataBase.Entities
{
    public class BaseEntities
    {
        public BaseEntities()
        {
            Id = Guid.NewGuid();
            CreateDate = DateTime.Now;
        }
        [Key]
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid? CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
}
