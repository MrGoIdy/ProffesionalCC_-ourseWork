namespace ProfessionConsultantCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Consultations",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Name = c.String(maxLength: 600),
                        Places = c.Int(nullable: false),
                        Adres = c.String(maxLength: 200),
                        Time = c.String(maxLength: 15),
                        User_Id = c.Long(),
                        Consultant_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Users", t => t.Consultant_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Consultant_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Surname = c.String(maxLength: 620),
                        Name = c.String(maxLength: 600),
                        FatherName = c.String(maxLength: 200),
                        BirthDay = c.DateTime(nullable: false),
                        Adres = c.String(maxLength: 200),
                        Phone = c.String(maxLength: 50),
                        Password = c.String(maxLength: 50),
                        Login = c.String(maxLength: 50),
                        Role_Id = c.Long(),
                        Consultation_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.Role_Id)
                .ForeignKey("dbo.Consultations", t => t.Consultation_Id)
                .Index(t => t.Role_Id)
                .Index(t => t.Consultation_Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 600),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Consultation_Id", "dbo.Consultations");
            DropForeignKey("dbo.Consultations", "Consultant_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.Consultations", "User_Id", "dbo.Users");
            DropIndex("dbo.Users", new[] { "Consultation_Id" });
            DropIndex("dbo.Users", new[] { "Role_Id" });
            DropIndex("dbo.Consultations", new[] { "Consultant_Id" });
            DropIndex("dbo.Consultations", new[] { "User_Id" });
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Consultations");
        }
    }
}
