namespace BankManagementProject.Repository
{
    public interface IAccount
    {
        public string AccType { get; }
        Task<(decimal, decimal, string)> CalculateInterest(decimal amount);
    }
}
