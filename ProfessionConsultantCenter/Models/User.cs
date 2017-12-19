using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProfessionConsultantCenter.Models
{
    public class User
    {
        public User()
        {
            Consultations = new List<Consultation>();
        }

        [StringLength(620)]
        public string Surname{ get; set; }

        [StringLength(600)]
        public string Name { get; set; }

        [StringLength(200)]
        public string FatherName { get; set; }

        public DateTime BirthDay { get; set; }

        [StringLength(200)]
        public string Adres { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(50)]
        public string Login { get; set; }

        public virtual ICollection<Consultation> Consultations { get; set; }

        public virtual Role Role { get; set; }

        [Key]
        [Column(Order = 0)]
        public long Id { get; set; }

    }
}