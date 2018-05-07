namespace CleaningSupplies.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotesField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Masters", "Notes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Masters", "Notes");
        }
    }
}
