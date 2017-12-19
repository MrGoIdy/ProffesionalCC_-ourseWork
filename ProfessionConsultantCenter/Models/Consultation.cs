using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProfessionConsultantCenter.Models
{
    public class Consultation
    {
        public Consultation()
        {
            Users = new List<User>();
        }
        public DateTime Date { get; set; }

        [StringLength(600)]
        public string Name { get; set; }

        public int Places { get; set; }

        [StringLength(200)]
        public string Adres { get; set; }

        [StringLength(15)]
        public string Time { get; set; }

        public virtual ICollection<User> Users { get; set; }

        [Key]
        [Column(Order = 0)]
        public long Id { get; set; }
    }
}