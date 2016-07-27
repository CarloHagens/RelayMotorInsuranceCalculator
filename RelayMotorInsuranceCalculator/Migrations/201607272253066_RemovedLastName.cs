namespace RelayMotorInsuranceCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedLastName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Drivers", "Name", c => c.String(nullable: false, maxLength: 30));
            DropColumn("dbo.Drivers", "FullName");
            DropColumn("dbo.Drivers", "FirstName");
            DropColumn("dbo.Drivers", "LastName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Drivers", "LastName", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.Drivers", "FirstName", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.Drivers", "FullName", c => c.String());
            DropColumn("dbo.Drivers", "Name");
        }
    }
}
