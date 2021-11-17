namespace BloodBank.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DonorAppointment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AppointmentTime = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        DonorID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Donor", t => t.DonorID, cascadeDelete: true)
                .Index(t => t.DonorID);
            
            CreateTable(
                "dbo.Donor",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        BloodType = c.Int(nullable: false),
                        FullName = c.String(),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BloodTable", t => t.BloodType, cascadeDelete: true)
                .Index(t => t.BloodType);
            
            CreateTable(
                "dbo.BloodTable",
                c => new
                    {
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Type);
            
            CreateTable(
                "dbo.BloodBag",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Volume = c.Double(nullable: false),
                        ExtractionDate = c.DateTime(nullable: false),
                        BloodType = c.Int(nullable: false),
                        DaysStored = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BloodTable", t => t.BloodType, cascadeDelete: true)
                .Index(t => t.BloodType);
            
            CreateTable(
                "dbo.BloodOrder",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Amount = c.Double(nullable: false),
                        BloodType = c.Int(nullable: false),
                        PatientID = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Patient", t => t.PatientID, cascadeDelete: true)
                .Index(t => t.PatientID);
            
            CreateTable(
                "dbo.Patient",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        BloodType = c.Int(nullable: false),
                        FullName = c.String(),
                        CheckInDate = c.DateTime(nullable: false),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BloodTable", t => t.BloodType, cascadeDelete: true)
                .Index(t => t.BloodType);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.BloodOrder", "PatientID", "dbo.Patient");
            DropForeignKey("dbo.Patient", "BloodType", "dbo.BloodTable");
            DropForeignKey("dbo.BloodBag", "BloodType", "dbo.BloodTable");
            DropForeignKey("dbo.DonorAppointment", "DonorID", "dbo.Donor");
            DropForeignKey("dbo.Donor", "BloodType", "dbo.BloodTable");
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.Patient", new[] { "BloodType" });
            DropIndex("dbo.BloodOrder", new[] { "PatientID" });
            DropIndex("dbo.BloodBag", new[] { "BloodType" });
            DropIndex("dbo.Donor", new[] { "BloodType" });
            DropIndex("dbo.DonorAppointment", new[] { "DonorID" });
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.Patient");
            DropTable("dbo.BloodOrder");
            DropTable("dbo.BloodBag");
            DropTable("dbo.BloodTable");
            DropTable("dbo.Donor");
            DropTable("dbo.DonorAppointment");
        }
    }
}
