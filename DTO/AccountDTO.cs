namespace BankApplicationProject.DTO
{
    public class AccountDTO
    {
        public decimal Balance { get; set; }
        public string Status { get; set; } = null!;
        public string AccountType { get; set; }
        public int AccountNo { get; set; }
    }
}
