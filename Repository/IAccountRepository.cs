using BankApplicationProject.DTO;
using BankApplicationProject.Models;

namespace BankApplicationProject.Repository
{
    public interface IAccountRepository
    {
        //public Task<bool> DeleteAccount(int AccountNo);
        public Task<bool> UpdateAccountStatusByAccountNumber(UpdateStatusDTO updateStatusDTO);
        public Task<(decimal, decimal, string)> GetInterest(long accountno);

        public Task<List<Account>> GetAccounts();

        public Task<Account> GetAccountByAccountNumber(long accountNumber);
    }
}
