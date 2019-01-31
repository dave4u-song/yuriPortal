namespace yuriPortal.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customer", "CreateDt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Customer", "CreateId", c => c.String(maxLength: 128));
            AddColumn("dbo.Customer", "UpdateDt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Customer", "UpdateId", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customer", "UpdateId");
            DropColumn("dbo.Customer", "UpdateDt");
            DropColumn("dbo.Customer", "CreateId");
            DropColumn("dbo.Customer", "CreateDt");
        }
    }
}
