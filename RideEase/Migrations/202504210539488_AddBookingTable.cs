namespace RideEase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookingTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VehicleId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        CustomerName = c.String(),
                        CustomerEmail = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vehicles", t => t.VehicleId, cascadeDelete: true)
                .Index(t => t.VehicleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "VehicleId", "dbo.Vehicles");
            DropIndex("dbo.Bookings", new[] { "VehicleId" });
            DropTable("dbo.Bookings");
        }
    }
}
