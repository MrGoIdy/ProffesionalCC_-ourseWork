using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProfessionConsultantCenter.Models
{
    public class Role : IRole
    {
        [StringLength(600)]
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }

        [Key]
        [Column(Order = 0)]
        public long Id { get; set; }

        string IRole<string>.Id
        {
            get
            {
                return Name;
            }
        }

        public Role()
        {
            Users = new List<User>();
        }
    }
}