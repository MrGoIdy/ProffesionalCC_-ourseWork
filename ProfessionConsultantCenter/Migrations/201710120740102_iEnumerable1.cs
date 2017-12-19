namespace ProfessionConsultantCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class iEnumerable1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Consultations", "Consultant_Id", "dbo.Users");
            DropIndex("dbo.Consultations", new[] { "Consultant_Id" });
            DropColumn("dbo.Consultations", "Consultant_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Consultations", "Consultant_Id", c => c.Long());
            CreateIndex("dbo.Consultations", "Consultant_Id");
            AddForeignKey("dbo.Consultations", "Consultant_Id", "dbo.Users", "Id");
        }
    }
}
