namespace BankApplicationProject.DTO
{
    public class AddCustomerDTO
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string Address { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string AadharNumber { get; set; }
        public string AccountType { get; set; }
        public string Status { get; set; } = null!;
        public decimal Balance { get; set; }

    }
}
