namespace ProfessionConsultantCenter.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ProfessionConsultantDB : DbContext
    {
        
        public ProfessionConsultantDB()
            : base("name=ProfessionConsultantDB")
        {
        }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Consultation> Consultations { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}