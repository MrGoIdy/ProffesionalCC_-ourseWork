namespace ProfessionConsultantCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class iCollection : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserConsultations",
                c => new
                    {
                        User_Id = c.Long(nullable: false),
                        Consultation_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Consultation_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Consultations", t => t.Consultation_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Consultation_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserConsultations", "Consultation_Id", "dbo.Consultations");
            DropForeignKey("dbo.UserConsultations", "User_Id", "dbo.Users");
            DropIndex("dbo.UserConsultations", new[] { "Consultation_Id" });
            DropIndex("dbo.UserConsultations", new[] { "User_Id" });
            DropTable("dbo.UserConsultations");
        }
    }
}
