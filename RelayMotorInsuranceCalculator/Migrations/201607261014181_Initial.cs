namespace RelayMotorInsuranceCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Claims",
                c => new
                {
                    Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
                    ClaimDate = c.DateTimeOffset(nullable: false, precision: 7),
                    DriverId = c.Guid(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Drivers", t => t.DriverId, cascadeDelete: true)
                .Index(t => t.DriverId);

            CreateTable(
                "dbo.Drivers",
                c => new
                {
                    Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
                    FirstName = c.String(nullable: false, maxLength: 30),
                    LastName = c.String(nullable: false, maxLength: 30),
                    Occupation = c.Int(nullable: false),
                    DateOfBirth = c.DateTimeOffset(nullable: false, precision: 7),
                    PolicyId = c.Guid(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Policies", t => t.PolicyId, cascadeDelete: true)
                .Index(t => t.PolicyId);
            Sql("ALTER TABLE [dbo].[Drivers] ADD [FullName] AS [FirstName] + ' ' + [LastName]");
            CreateTable(
                "dbo.Policies",
                c => new
                {
                    Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
                    StartDate = c.DateTimeOffset(nullable: false, precision: 7),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Claims", "DriverId", "dbo.Drivers");
            DropForeignKey("dbo.Drivers", "PolicyId", "dbo.Policies");
            DropIndex("dbo.Drivers", new[] { "PolicyId" });
            DropIndex("dbo.Claims", new[] { "DriverId" });
            DropTable("dbo.Policies");
            DropTable("dbo.Drivers");
            DropTable("dbo.Claims");
        }
    }
}
