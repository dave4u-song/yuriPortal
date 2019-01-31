namespace yuriPortal.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCostomerColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerNote", "NoteContents", c => c.String());
            AddColumn("dbo.Customer", "CustomerType", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customer", "CustomerType");
            DropColumn("dbo.CustomerNote", "NoteContents");
        }
    }
}
