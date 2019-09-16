namespace RentACar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDbContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kategorijas",
                c => new
                    {
                        KategorijaId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.KategorijaId);
            
            CreateTable(
                "dbo.Voziloes",
                c => new
                    {
                        VoziloId = c.Int(nullable: false, identity: true),
                        ModelName = c.String(nullable: false, maxLength: 12),
                        ImageUrl = c.String(nullable: false),
                        Location = c.String(nullable: false, maxLength: 20),
                        PriceDay = c.Double(nullable: false),
                        KategorijaId = c.Int(nullable: false),
                        ProizvoditelId = c.Int(nullable: false),
                        SopstvenikId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VoziloId)
                .ForeignKey("dbo.Kategorijas", t => t.KategorijaId, cascadeDelete: true)
                .ForeignKey("dbo.Proizvoditels", t => t.ProizvoditelId, cascadeDelete: true)
                .ForeignKey("dbo.Sopstveniks", t => t.SopstvenikId, cascadeDelete: true)
                .Index(t => t.KategorijaId)
                .Index(t => t.ProizvoditelId)
                .Index(t => t.SopstvenikId);
            
            CreateTable(
                "dbo.Komentars",
                c => new
                    {
                        SopstvenikId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 15),
                        Rating = c.Double(nullable: false),
                        VoziloId = c.Int(nullable: false),
                        KorisnikId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SopstvenikId)
                .ForeignKey("dbo.Korisniks", t => t.KorisnikId, cascadeDelete: true)
                .ForeignKey("dbo.Voziloes", t => t.VoziloId, cascadeDelete: true)
                .Index(t => t.VoziloId)
                .Index(t => t.KorisnikId);
            
            CreateTable(
                "dbo.Korisniks",
                c => new
                    {
                        KorisnikId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 12),
                        LastName = c.String(nullable: false, maxLength: 20),
                        Address = c.String(nullable: false),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.KorisnikId);
            
            CreateTable(
                "dbo.Rezervacijas",
                c => new
                    {
                        RezervacijaId = c.Int(nullable: false, identity: true),
                        denoviIznajmuvanje = c.Int(nullable: false),
                        uspesnost = c.Boolean(nullable: false),
                        plateno = c.Boolean(nullable: false),
                        total = c.Double(nullable: false),
                        VoziloId = c.Int(nullable: false),
                        KorisnikId = c.Int(nullable: false),
                        DateFrom = c.DateTime(nullable: false),
                        DateTo = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RezervacijaId)
                .ForeignKey("dbo.Korisniks", t => t.KorisnikId, cascadeDelete: true)
                .ForeignKey("dbo.Voziloes", t => t.VoziloId, cascadeDelete: true)
                .Index(t => t.VoziloId)
                .Index(t => t.KorisnikId);
            
            CreateTable(
                "dbo.Proizvoditels",
                c => new
                    {
                        ProizvoditelId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Year = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProizvoditelId);
            
            CreateTable(
                "dbo.Sopstveniks",
                c => new
                    {
                        SopstvenikId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 12),
                        LastName = c.String(nullable: false, maxLength: 20),
                        Address = c.String(nullable: false),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SopstvenikId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Voziloes", "SopstvenikId", "dbo.Sopstveniks");
            DropForeignKey("dbo.Voziloes", "ProizvoditelId", "dbo.Proizvoditels");
            DropForeignKey("dbo.Komentars", "VoziloId", "dbo.Voziloes");
            DropForeignKey("dbo.Rezervacijas", "VoziloId", "dbo.Voziloes");
            DropForeignKey("dbo.Rezervacijas", "KorisnikId", "dbo.Korisniks");
            DropForeignKey("dbo.Komentars", "KorisnikId", "dbo.Korisniks");
            DropForeignKey("dbo.Voziloes", "KategorijaId", "dbo.Kategorijas");
            DropIndex("dbo.Rezervacijas", new[] { "KorisnikId" });
            DropIndex("dbo.Rezervacijas", new[] { "VoziloId" });
            DropIndex("dbo.Komentars", new[] { "KorisnikId" });
            DropIndex("dbo.Komentars", new[] { "VoziloId" });
            DropIndex("dbo.Voziloes", new[] { "SopstvenikId" });
            DropIndex("dbo.Voziloes", new[] { "ProizvoditelId" });
            DropIndex("dbo.Voziloes", new[] { "KategorijaId" });
            DropTable("dbo.Sopstveniks");
            DropTable("dbo.Proizvoditels");
            DropTable("dbo.Rezervacijas");
            DropTable("dbo.Korisniks");
            DropTable("dbo.Komentars");
            DropTable("dbo.Voziloes");
            DropTable("dbo.Kategorijas");
        }
    }
}
