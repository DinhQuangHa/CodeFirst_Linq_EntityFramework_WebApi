using QuanLyNhanVienDataBase.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanVienDataBase
{
    public class QuanLyNhanVienDbContext : DbContext
    {
        public QuanLyNhanVienDbContext(): base("QuanLyNhanVienDbContext")
        {

        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Buiding> Buidings { get; set; }
        public DbSet<JoinBuiding> JoinBuidings { get; set; }
    }
}
