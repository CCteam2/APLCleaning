namespace CleaningSupplies.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Masters",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        QuantityInStock = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedByDateTime = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedByDatetime = c.DateTimeOffset(nullable: false, precision: 7),
                        CreatedById_Id = c.String(maxLength: 128),
                        ModifiedById_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedById_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ModifiedById_Id)
                .Index(t => t.CreatedById_Id)
                .Index(t => t.ModifiedById_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Usages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Item = c.String(nullable: false),
                        Quantity_modified = c.Int(nullable: false),
                        CreatedByDateTime = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedByDatetime = c.DateTimeOffset(nullable: false, precision: 7),
                        CreatedById_Id = c.String(maxLength: 128),
                        GetMasterT_ID = c.Int(),
                        ModifiedById_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedById_Id)
                .ForeignKey("dbo.Masters", t => t.GetMasterT_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ModifiedById_Id)
                .Index(t => t.CreatedById_Id)
                .Index(t => t.GetMasterT_ID)
                .Index(t => t.ModifiedById_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usages", "ModifiedById_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Usages", "GetMasterT_ID", "dbo.Masters");
            DropForeignKey("dbo.Usages", "CreatedById_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Masters", "ModifiedById_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Masters", "CreatedById_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Usages", new[] { "ModifiedById_Id" });
            DropIndex("dbo.Usages", new[] { "GetMasterT_ID" });
            DropIndex("dbo.Usages", new[] { "CreatedById_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Masters", new[] { "ModifiedById_Id" });
            DropIndex("dbo.Masters", new[] { "CreatedById_Id" });
            DropTable("dbo.Usages");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Masters");
        }
    }
}
