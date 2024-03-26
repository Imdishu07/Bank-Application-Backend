using System;
using System.Collections.Generic;

namespace BankApplicationProject.Models
{
    public partial class Account
    {
        public Account()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int AccountNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public decimal? Balance { get; set; }
        public string? Status { get; set; } = null!;
        public string? AccountType { get; set; }
        public int? CustomerId { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
