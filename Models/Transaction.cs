using System;
using System.Collections.Generic;

namespace BankApplicationProject.Models
{
    public partial class Transaction
    {
        public int TransactionId { get; set; }
        public DateTime? Time { get; set; }
        public string? Type { get; set; }
        public decimal? Amount { get; set; }
        public decimal? AvailableBalance { get; set; }
        public int? AccountId { get; set; }

        public virtual Account? Account { get; set; }
    }
}
