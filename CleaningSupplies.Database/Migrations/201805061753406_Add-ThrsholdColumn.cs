namespace CleaningSupplies.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddThrsholdColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Masters", "MinimumValue", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Masters", "MinimumValue");
        }
    }
}
