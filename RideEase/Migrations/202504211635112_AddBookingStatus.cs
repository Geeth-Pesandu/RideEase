namespace RideEase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookingStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bookings", "Status");
        }
    }
}
