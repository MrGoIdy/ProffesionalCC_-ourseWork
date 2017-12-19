namespace ProfessionConsultantCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class iEnumerable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Consultations", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Consultation_Id", "dbo.Consultations");
            DropIndex("dbo.Consultations", new[] { "User_Id" });
            DropIndex("dbo.Users", new[] { "Consultation_Id" });
            DropColumn("dbo.Consultations", "User_Id");
            DropColumn("dbo.Users", "Consultation_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Consultation_Id", c => c.Long());
            AddColumn("dbo.Consultations", "User_Id", c => c.Long());
            CreateIndex("dbo.Users", "Consultation_Id");
            CreateIndex("dbo.Consultations", "User_Id");
            AddForeignKey("dbo.Users", "Consultation_Id", "dbo.Consultations", "Id");
            AddForeignKey("dbo.Consultations", "User_Id", "dbo.Users", "Id");
        }
    }
}
