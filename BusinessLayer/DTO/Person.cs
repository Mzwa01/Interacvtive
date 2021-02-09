using DataLayer.Models;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace BusinessLayer.DTO
{
    public partial class PersonDTO
    {
        public PersonDTO()
        {
            Accounts = new HashSet<Account>();
        }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Code { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [Unique]
        public string IdNumber { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
