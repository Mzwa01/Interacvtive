using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataLayer.Models
{
    public partial class Person
    {
        public Person()
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
