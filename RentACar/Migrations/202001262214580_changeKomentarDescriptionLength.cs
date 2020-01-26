namespace RentACar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeKomentarDescriptionLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Komentars", "Description", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Komentars", "Description", c => c.String(nullable: false, maxLength: 15));
        }
    }
}
