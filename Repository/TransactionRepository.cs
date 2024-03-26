using AutoMapper;
using BankApplicationProject.DTO;
using BankApplicationProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApplicationProject.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BankManagementContext _context;
        private readonly IMapper _mapper;
        public TransactionRepository(BankManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> Withdraw(WithdrawDTO withdrawDTO)
        {
            try
            {
                var account = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNo == withdrawDTO.AccountNo);

                if (account == null)
                    return false;

                if (account.Balance < withdrawDTO.Amount)
                    return false;

                account.Balance -= withdrawDTO.Amount;

                var transaction = _mapper.Map<Transaction>(withdrawDTO);
                transaction.Time = DateTime.Now;
                transaction.Type = "Withdrawal";
                transaction.AccountId = account.AccountNo;
                transaction.AvailableBalance = account.Balance;

                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }


        public async Task<bool> Deposit(DepositDTO depositDTO)
        {
            try
            {
                var account = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNo == depositDTO.AccountNo);
                if (account == null)
                    return false;

                account.Balance += depositDTO.Amount;

                var transaction = _mapper.Map<Transaction>(depositDTO);
                transaction.Time = DateTime.Now;
                transaction.Type = "Deposit";
                transaction.AccountId = account.AccountNo;
                transaction.AvailableBalance = account.Balance;

                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Transaction>> GetAllTransactions()
        {
            try
            {
                List<Transaction> transactions = await _context.Transactions.ToListAsync();
                return transactions;
            }
            catch
            {
                throw;
            }
        }

    }
}
