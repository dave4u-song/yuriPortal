namespace yuriPortal.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerNote",
                c => new
                    {
                        NoteId = c.Int(nullable: false, identity: true),
                        CustomerId = c.String(maxLength: 128),
                        NoteStatus = c.String(maxLength: 50),
                        CreateDt = c.DateTime(nullable: false),
                        CreateId = c.String(maxLength: 128),
                        UpdateDt = c.DateTime(nullable: false),
                        UpdateId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.NoteId);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        CustomerId = c.String(nullable: false, maxLength: 128),
                        CustomerName = c.String(maxLength: 100),
                        CustEmail = c.String(maxLength: 500),
                        CustPhoneNumber = c.String(maxLength: 100),
                        CustMobileNumber = c.String(maxLength: 100),
                        CustFaxNumber = c.String(maxLength: 100),
                        Street = c.String(maxLength: 256),
                        City = c.String(maxLength: 50),
                        Province = c.String(maxLength: 50),
                        Country = c.String(maxLength: 50),
                        PostalCode = c.String(maxLength: 50),
                        IsDelete = c.Int(nullable: false),
                        OwnerId = c.String(maxLength: 128),
                        CustomerPic = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.CustTask",
                c => new
                    {
                        CustTaskId = c.Int(nullable: false, identity: true),
                        CustomerId = c.String(maxLength: 128),
                        TaskType = c.String(maxLength: 50),
                        TaskSubject = c.String(maxLength: 1000),
                        TaskContent = c.String(),
                        TaskStatus = c.String(maxLength: 50),
                        TaskBeginDt = c.DateTime(nullable: false),
                        TaskEndDt = c.DateTime(nullable: false),
                        DurationMin = c.Int(nullable: false),
                        TaskCompleteDt = c.DateTime(nullable: false),
                        CreateDt = c.DateTime(nullable: false),
                        CreateId = c.String(maxLength: 128),
                        UpdateDt = c.DateTime(nullable: false),
                        UpdateId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CustTaskId);
            
            CreateTable(
                "dbo.TaskAssigned",
                c => new
                    {
                        CustTaskId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        TaskStatus = c.String(maxLength: 50),
                        AssignedDt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CustTaskId);
            
            CreateTable(
                "dbo.TaskAttach",
                c => new
                    {
                        CustTaskId = c.Int(nullable: false),
                        FileId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.CustTaskId, t.FileId });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TaskAttach");
            DropTable("dbo.TaskAssigned");
            DropTable("dbo.CustTask");
            DropTable("dbo.Customer");
            DropTable("dbo.CustomerNote");
        }
    }
}
