

using BankApplicationProject.DTO;
using BankApplicationProject.Models;

namespace BankApplicationProject.Repository
{
    public interface ITransactionRepository
    {
        public Task<bool> Withdraw(WithdrawDTO withdrawDTO);
        public Task<bool> Deposit(DepositDTO depositDTO);
        public Task<List<Transaction>> GetAllTransactions();
    }
}
