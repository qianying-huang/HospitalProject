namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JobDepartment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "DeptId", c => c.Int(nullable: false));
            CreateIndex("dbo.Jobs", "DeptId");
            AddForeignKey("dbo.Jobs", "DeptId", "dbo.Departments", "DeptId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "DeptId", "dbo.Departments");
            DropIndex("dbo.Jobs", new[] { "DeptId" });
            DropColumn("dbo.Jobs", "DeptId");
        }
    }
}
