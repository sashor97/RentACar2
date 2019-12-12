namespace RentACar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeKorisnik : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Korisniks", "email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Korisniks", "email");
        }
    }
}
