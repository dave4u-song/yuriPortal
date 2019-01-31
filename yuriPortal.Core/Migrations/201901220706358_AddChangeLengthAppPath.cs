namespace yuriPortal.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddChangeLengthAppPath : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Menu", "AppPathId", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Menu", "AppPathId", c => c.String(maxLength: 50));
        }
    }
}
