namespace CodeFirstDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 500),
                        CreateTime = c.DateTime(nullable: false),
                        CreatorId = c.Int(nullable: false),
                        LastModifierId = c.Int(),
                        LastModifyTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 500),
                        Url = c.String(maxLength: 500, unicode: false),
                        SourcePath = c.String(nullable: false, maxLength: 1000, unicode: false),
                        State = c.Int(nullable: false),
                        MenuLevel = c.Int(nullable: false),
                        Sort = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        CreatorId = c.Int(nullable: false),
                        LastModifierId = c.Int(),
                        LastModifyTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Account = c.String(nullable: false, maxLength: 100, unicode: false),
                        Password = c.String(nullable: false, maxLength: 100, unicode: false),
                        Email = c.String(maxLength: 200, unicode: false),
                        Mobile = c.String(maxLength: 30, unicode: false),
                        CompanyId = c.Int(),
                        CompanyName = c.String(maxLength: 500),
                        State = c.Int(nullable: false),
                        UserType = c.Int(nullable: false),
                        LastLoginTime = c.DateTime(),
                        CreateTime = c.DateTime(nullable: false),
                        CreatorId = c.Int(nullable: false),
                        LastModifierId = c.Int(),
                        LastModifyTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserMenuMapping",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        MenuId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserMenuMapping");
            DropTable("dbo.User");
            DropTable("dbo.Menu");
            DropTable("dbo.Company");
        }
    }
}
