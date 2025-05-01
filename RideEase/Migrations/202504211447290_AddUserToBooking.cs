namespace RideEase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserToBooking : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Bookings", "UserId");
            AddForeignKey("dbo.Bookings", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Bookings", new[] { "UserId" });
            DropColumn("dbo.Bookings", "UserId");
        }
    }
}
