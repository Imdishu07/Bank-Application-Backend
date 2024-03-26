namespace BankApplicationProject.DTO
{
    public class TransactionDTO
    {
        public int TransactionId { get; set; }
        public DateTime Time { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public decimal AvailableBalance { get; set; }
    }
}
