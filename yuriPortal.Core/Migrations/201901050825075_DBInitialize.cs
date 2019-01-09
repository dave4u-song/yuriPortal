namespace yuriPortal.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBInitialize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppPath",
                c => new
                    {
                        AppPathId = c.String(nullable: false, maxLength: 128),
                        AppPathName = c.String(nullable: false, maxLength: 100),
                        PagePath = c.String(maxLength: 500),
                        Parameters = c.String(maxLength: 500),
                        IsDelete = c.Int(nullable: false),
                        CreateDt = c.DateTime(nullable: false),
                        CreateId = c.String(maxLength: 128),
                        UpdateDt = c.DateTime(nullable: false),
                        UpdateId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.AppPathId);
            
            CreateTable(
                "dbo.AttachFile",
                c => new
                    {
                        FileId = c.String(nullable: false, maxLength: 128),
                        FileType = c.String(maxLength: 50),
                        OriginFileName = c.String(maxLength: 128),
                        SaveAsFileName = c.String(maxLength: 128),
                        FileExtension = c.String(maxLength: 15),
                        FilePath = c.String(maxLength: 1000),
                        IsDelete = c.Int(nullable: false),
                        CreateDt = c.DateTime(nullable: false),
                        CreateId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.FileId);
            
            CreateTable(
                "dbo.BoardAttach",
                c => new
                    {
                        PostId = c.Int(nullable: false),
                        FileId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.PostId, t.FileId })
                .ForeignKey("dbo.BoardPost", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.BoardComment",
                c => new
                    {
                        CommetId = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        CommentLevel = c.Int(nullable: false),
                        ParentId = c.Int(),
                        CommentContent = c.String(),
                        CreateDt = c.DateTime(nullable: false),
                        CreateId = c.String(maxLength: 128),
                        UpdateDt = c.DateTime(nullable: false),
                        UpdateId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CommetId)
                .ForeignKey("dbo.BoardPost", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.BoardPost",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        Board_id = c.Int(nullable: false),
                        PostSubject = c.String(maxLength: 500),
                        PostContent = c.String(),
                        Status = c.String(maxLength: 50),
                        CreateDt = c.DateTime(nullable: false),
                        CreateId = c.String(maxLength: 128),
                        UpdateDt = c.DateTime(nullable: false),
                        UpdateId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.Board", t => t.Board_id, cascadeDelete: true)
                .Index(t => t.Board_id);
            
            CreateTable(
                "dbo.Board",
                c => new
                    {
                        Board_id = c.Int(nullable: false, identity: true),
                        Board_name = c.String(maxLength: 50),
                        IsAttach = c.Int(nullable: false),
                        IsComment = c.Int(nullable: false),
                        IsLike = c.Int(nullable: false),
                        IsDelete = c.Int(nullable: false),
                        CreateDt = c.DateTime(nullable: false),
                        CreateId = c.String(maxLength: 128),
                        UpdateDt = c.DateTime(nullable: false),
                        UpdateId = c.String(maxLength: 128),
                        Description = c.String(maxLength: 1000),
                        DefaultUI = c.String(maxLength: 6),
                    })
                .PrimaryKey(t => t.Board_id);
            
            CreateTable(
                "dbo.BoardLike",
                c => new
                    {
                        LikeId = c.Int(nullable: false),
                        PostId = c.Int(nullable: false),
                        CreateId = c.String(nullable: false, maxLength: 18),
                        CreateDt = c.DateTime(nullable: false),
                        IsLike = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LikeId, t.PostId, t.CreateId })
                .ForeignKey("dbo.BoardPost", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.CodeGroup",
                c => new
                    {
                        CodeGroupID = c.String(nullable: false, maxLength: 15),
                        CodeGroupText = c.String(maxLength: 100),
                        Description = c.String(maxLength: 1000),
                        CreateDt = c.DateTime(nullable: false),
                        CreateId = c.String(maxLength: 128),
                        UpdateDt = c.DateTime(nullable: false),
                        UpdateId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CodeGroupID);
            
            CreateTable(
                "dbo.CommCode",
                c => new
                    {
                        Code_ID = c.String(nullable: false, maxLength: 10),
                        Code_Name = c.String(nullable: false, maxLength: 50),
                        CodeGroupID = c.String(nullable: false, maxLength: 15),
                        Code_Value = c.String(maxLength: 50),
                        Description = c.String(maxLength: 1000),
                        IsDelete = c.Int(),
                        CreateDt = c.DateTime(),
                        CreateId = c.String(maxLength: 128),
                        UpdateDt = c.DateTime(),
                        UpdateId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Code_ID, t.Code_Name, t.CodeGroupID });
            
            CreateTable(
                "dbo.Language",
                c => new
                    {
                        LangId = c.String(nullable: false, maxLength: 50),
                        LangCode = c.String(nullable: false, maxLength: 50),
                        DefaultText = c.String(nullable: false),
                        DefaultTrans = c.String(nullable: false),
                        LangTrans = c.String(),
                        CreateDt = c.DateTime(nullable: false),
                        CreateId = c.String(maxLength: 128),
                        UpdateDt = c.DateTime(nullable: false),
                        UpdateId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LangId, t.LangCode });
            
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        MenuId = c.String(nullable: false, maxLength: 50),
                        MenuName = c.String(nullable: false, maxLength: 100),
                        MenuLevel = c.Int(nullable: false),
                        OrderNo = c.Int(nullable: false),
                        ParentMenuId = c.String(maxLength: 50),
                        AppPathId = c.String(maxLength: 50),
                        IsDelete = c.Int(),
                        CreateDt = c.DateTime(nullable: false),
                        CreateId = c.String(maxLength: 128),
                        UpdateDt = c.DateTime(nullable: false),
                        UpdateId = c.String(maxLength: 128),
                        Menu_MenuId = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.MenuId)
                .ForeignKey("dbo.Menu", t => t.Menu_MenuId)
                .Index(t => t.Menu_MenuId);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.RoleId)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(maxLength: 100),
                        MiddleName = c.String(maxLength: 100),
                        LastName = c.String(maxLength: 100),
                        Street = c.String(maxLength: 256),
                        City = c.String(maxLength: 50),
                        Province = c.String(maxLength: 50),
                        Country = c.String(maxLength: 50),
                        PostalCode = c.String(maxLength: 50),
                        Gender = c.String(maxLength: 50),
                        LanguageCd = c.String(maxLength: 50),
                        UserPic = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LastAccessDt = c.DateTime(),
                        IsDelete = c.Int(),
                        AccessIP = c.String(maxLength: 50),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.UserId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UserClaim",
                c => new
                    {
                        UserClaimId = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.UserClaimId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogin",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRole", "UserId", "dbo.User");
            DropForeignKey("dbo.UserLogin", "UserId", "dbo.User");
            DropForeignKey("dbo.UserClaim", "UserId", "dbo.User");
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Role");
            DropForeignKey("dbo.Menu", "Menu_MenuId", "dbo.Menu");
            DropForeignKey("dbo.BoardAttach", "PostId", "dbo.BoardPost");
            DropForeignKey("dbo.BoardLike", "PostId", "dbo.BoardPost");
            DropForeignKey("dbo.BoardComment", "PostId", "dbo.BoardPost");
            DropForeignKey("dbo.BoardPost", "Board_id", "dbo.Board");
            DropIndex("dbo.UserLogin", new[] { "UserId" });
            DropIndex("dbo.UserClaim", new[] { "UserId" });
            DropIndex("dbo.User", "UserNameIndex");
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.Role", "RoleNameIndex");
            DropIndex("dbo.Menu", new[] { "Menu_MenuId" });
            DropIndex("dbo.BoardLike", new[] { "PostId" });
            DropIndex("dbo.BoardPost", new[] { "Board_id" });
            DropIndex("dbo.BoardComment", new[] { "PostId" });
            DropIndex("dbo.BoardAttach", new[] { "PostId" });
            DropTable("dbo.UserLogin");
            DropTable("dbo.UserClaim");
            DropTable("dbo.User");
            DropTable("dbo.UserProfile");
            DropTable("dbo.UserRole");
            DropTable("dbo.Role");
            DropTable("dbo.Menu");
            DropTable("dbo.Language");
            DropTable("dbo.CommCode");
            DropTable("dbo.CodeGroup");
            DropTable("dbo.BoardLike");
            DropTable("dbo.Board");
            DropTable("dbo.BoardPost");
            DropTable("dbo.BoardComment");
            DropTable("dbo.BoardAttach");
            DropTable("dbo.AttachFile");
            DropTable("dbo.AppPath");
        }
    }
}
