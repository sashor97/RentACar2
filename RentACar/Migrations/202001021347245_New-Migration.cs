namespace RentACar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sopstveniks", "email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sopstveniks", "email");
        }
    }
}
