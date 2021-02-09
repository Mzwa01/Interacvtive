using Microsoft.EntityFrameworkCore;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataLayer.Models
{
    public partial class Account
    {
        public Account()
        {
            Transactions = new HashSet<Transaction>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Code { get; set; }
        public int PersonCode { get; set; }
        [Unique]
        public string AccountNumber { get; set; }
        public decimal OutstandingBalance { get; set; }

        public virtual Person PersonCodeNavigation { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
