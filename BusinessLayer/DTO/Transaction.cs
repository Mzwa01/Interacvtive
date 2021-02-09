using BusinessLayer.Tools.CustomDataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace BusinessLayer.DTO
{
    public partial class TransactionDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Code { get; set; }
        public int AccountCode { get; set; }
        [CurrentDate(ErrorMessage = "Date must be after or equal to current date")]
        public DateTime TransactionDate { get; set; }
        public DateTime CaptureDate { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }

        public virtual Account AccountCodeNavigation { get; set; }
    }
}
