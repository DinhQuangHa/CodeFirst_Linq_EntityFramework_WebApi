namespace QuanLyNhanVienDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Buidings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BuidingName = c.String(nullable: false, maxLength: 2000),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Address = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        CreateBy = c.Guid(),
                        UpdateDate = c.DateTime(),
                        UpdatedBy = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.JoinBuidings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        WorkDate = c.Int(nullable: false),
                        BuidingId = c.Guid(nullable: false),
                        EmployeeId = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        CreateBy = c.Guid(),
                        UpdateDate = c.DateTime(),
                        UpdatedBy = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Buidings", t => t.BuidingId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.BuidingId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 250),
                        Birthday = c.DateTime(nullable: false),
                        Address = c.String(),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Phone = c.String(),
                        Gmail = c.String(),
                        Gender = c.String(),
                        Status = c.Int(nullable: false),
                        DepartmentId = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        CreateBy = c.Guid(),
                        UpdateDate = c.DateTime(),
                        UpdatedBy = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DepartmentName = c.String(maxLength: 365),
                        ManagerPerson = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        CreateBy = c.Guid(),
                        UpdateDate = c.DateTime(),
                        UpdatedBy = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JoinBuidings", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.JoinBuidings", "BuidingId", "dbo.Buidings");
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            DropIndex("dbo.JoinBuidings", new[] { "EmployeeId" });
            DropIndex("dbo.JoinBuidings", new[] { "BuidingId" });
            DropTable("dbo.Departments");
            DropTable("dbo.Employees");
            DropTable("dbo.JoinBuidings");
            DropTable("dbo.Buidings");
        }
    }
}
