namespace CleaningSupplies.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedTheDateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Masters", "CreatedByDateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Masters", "ModifiedByDatetime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Usages", "CreatedByDateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Usages", "ModifiedByDatetime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Usages", "ModifiedByDatetime", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.Usages", "CreatedByDateTime", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.Masters", "ModifiedByDatetime", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.Masters", "CreatedByDateTime", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
    }
}
