namespace yuriPortal.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHirerarychyCodeTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HierarchyCode",
                c => new
                    {
                        HrerarchyCd = c.String(nullable: false, maxLength: 50),
                        CodeName = c.String(nullable: false, maxLength: 100),
                        HireLevel = c.Int(nullable: false),
                        OrderNo = c.Int(nullable: false),
                        ParentHrerarchyCd = c.String(maxLength: 50),
                        IsDelete = c.Int(),
                        CreateDt = c.DateTime(nullable: false),
                        CreateId = c.String(maxLength: 128),
                        UpdateDt = c.DateTime(nullable: false),
                        UpdateId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.HrerarchyCd);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HierarchyCode");
        }
    }
}
