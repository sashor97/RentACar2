namespace RentACar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeYearProizvoditel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Proizvoditels", "Year");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Proizvoditels", "Year", c => c.Int(nullable: false));
        }
    }
}
