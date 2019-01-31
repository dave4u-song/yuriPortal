namespace yuriPortal.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DelDescInAppPaths : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AppPath", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AppPath", "Description", c => c.String(maxLength: 1000));
        }
    }
}
