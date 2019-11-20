namespace QuanLyNhanVienDataBase.Migrations
{
    using GenFu;
    using QuanLyNhanVienDataBase.Entities;
    using QuanLyNhanVienDataBase.Enums;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<QuanLyNhanVienDataBase.QuanLyNhanVienDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(QuanLyNhanVienDataBase.QuanLyNhanVienDbContext context)
        {
            DepartmentSeedData(context);
            EmployeeSeedData(context);
            BuidingSeedData(context);
            JoinBuidingSeedData(context);
            context.SaveChanges();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
        private void DepartmentSeedData(QuanLyNhanVienDbContext context)
        {
            if (!context.Departments.Any())
            {
                var Company = new string[] {
                    "Phong Phan Mem 1", "Phong Kiem Thu", "Phong Phan Mem 2",
                    "Phong Tuyen Nhan Vien", "Phong Thu Ngan", "Phong Hanh Chinh"
                };
                int i = 0;
                A.Configure<Department>()
                    .Fill(c => c.DepartmentName, () => { return Company[i++]; });
                var departments = A.ListOf<Department>(6);
                context.Departments.AddRange(departments);
            }
        }

        private void BuidingSeedData(QuanLyNhanVienDbContext context)
        {
            if (!context.Buidings.Any())
            {
                var buiding = new string[] {
                    "Nha hat Opera Sydney", "Thap Eiffel", "Toa nha Empire State",
                    "Nha tho Duc Ba Paris", "Casa Mila", "The Shard","Nha tho Brasilia",
                    "Toa thap Burj Khalifa","Burj Khalifa", "Lotte World Tower",
                    "Guangzhou CTF finance centre","Tianjin CTF finance centre",
                    "Landmark 81", "Zifeng tower","International Commerce Centre"
                };

                var address = new string[] {
                    "Dubai", "Phap", "Nga",
                    "Anh", "Viet Nam", "Nauy","Bi",
                    "Trung Quoc","Han Quoc", "Viet Nam",
                    "Trieu Tien","My",
                    "Viet Nam", "Thuy Sy","Uc"
                };

                var Date = new string[]
                {
                     "5/15/1980", "5/16/2015",
                     "6/15/1980", "6/16/2015",
                     "7/15/1980", "7/16/2015",
                     "5/15/1980", "6/16/2015",
                     "2/24/1980", "6/6/2019",
                     "1/24/1980", "1/6/2019",
                     "8/24/1980", "6/6/2019",
                     "1/24/1980", "6/6/2019",
                     "9/3/1980", "6/23/2031",
                     "4/3/1980", "6/23/2031",
                     "2/3/2021", "2/23/2031",
                     "1/3/2021", "1/23/2031",
                     "1/9/2021", "6/23/2031",
                     "3/9/2021", "3/23/2031",
                     "10/19/2014", "10/21/2021",
                     "3/9/2021", "3/23/2031",
                     "10/19/2014", "10/21/2021",
                     "3/9/2021", "3/23/2031",
                     "10/19/2014", "10/21/2021"
                };

                int i = 0, d = 0, s = 1, add = 0;
                A.Configure<Buiding>()
                    .Fill(c => c.BuidingName, () => { return buiding[i++]; })
                    .Fill(c => c.StartDate, () => { return DateTime.Parse(Date[d += 2]); })
                    .Fill(c => c.EndDate, () => { return DateTime.Parse(Date[s += 2]); })
                    .Fill(c => c.Address, () => { return address[add++]; });
                var buidings = A.ListOf<Buiding>(15);
                context.Buidings.AddRange(buidings);
            }
        }

        private void EmployeeSeedData(QuanLyNhanVienDbContext context)
        {
            if (!context.Employees.Any())
            {
                var _random = new Random(Environment.TickCount);
                var Gioitinh = new string[] {
                    "Nam", "Nu", "Chua Xac Dinh","Nam","Nam","Nu"
                };
                var departments = context.Departments.Take(6).ToList();

                A.Configure<Employee>()
                        .Fill(c => c.Salary).WithinRange(100000000, 2000000000)
                        .Fill(c => c.Gmail, c => $"{c.Name}@gmail.com")
                        .Fill(c => c.Gender, () => { return Gioitinh[_random.Next(0, Gioitinh.Length)]; })
                        .Fill(c => c.Status, () => { return Status.DangDiLam; })
                        .Fill(c => c.DepartmentId, () => { return departments[_random.Next(0, departments.Count)].Id; });
                var employees = A.ListOf<Employee>(20);
                context.Employees.AddRange(employees);
            }
        }

        private void JoinBuidingSeedData(QuanLyNhanVienDbContext context)
        {
            if (!context.JoinBuidings.Any())
            {
                var _random = new Random(Environment.TickCount);
                var buidings = context.Buidings.Take(15).ToList();
                var employees = context.Employees.Take(15).ToList();

                A.Configure<JoinBuiding>()
                        .Fill(c => c.WorkDate).WithinRange(0, 1000)
                        .Fill(c => c.BuidingId, () => { return buidings[_random.Next(0, buidings.Count)].Id; })
                        .Fill(c => c.EmployeeId, () => { return employees[_random.Next(0, employees.Count)].Id; });
                var joinBuidings = A.ListOf<JoinBuiding>(22);
                context.JoinBuidings.AddRange(joinBuidings);
            }
        }
    }
}
