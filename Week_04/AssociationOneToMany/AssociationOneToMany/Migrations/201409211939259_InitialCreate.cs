namespace AssociationOneToMany.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Manufacturers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Country = c.String(nullable: false, maxLength: 50),
                        YearStarted = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Model = c.String(nullable: false, maxLength: 50),
                        Trim = c.String(nullable: false, maxLength: 50),
                        ModelYear = c.Int(nullable: false),
                        MSRP = c.Int(nullable: false),
                        Manufacturer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Manufacturers", t => t.Manufacturer_Id)
                .Index(t => t.Manufacturer_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vehicles", "Manufacturer_Id", "dbo.Manufacturers");
            DropIndex("dbo.Vehicles", new[] { "Manufacturer_Id" });
            DropTable("dbo.Vehicles");
            DropTable("dbo.Manufacturers");
        }
    }
}
