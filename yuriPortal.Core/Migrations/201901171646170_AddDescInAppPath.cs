namespace yuriPortal.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDescInAppPath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppPath", "Description", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppPath", "Description");
        }
    }
}
