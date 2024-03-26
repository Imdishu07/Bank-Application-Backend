using AutoMapper;
using BankApplicationProject.DTO;
using BankApplicationProject.Helper;
using BankApplicationProject.Models;
using BankManagementProject.Repository;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace BankApplicationProject.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankManagementContext _Context;
        private readonly IMapper _mapper;
        //private readonly InterestHelper _interestHelper;
        private readonly IEnumerable<IAccount> _accounts;

        public AccountRepository(BankManagementContext Context, IMapper mapper, IEnumerable<IAccount> accounts)
        {
            _Context = Context;
            _mapper = mapper;
            _accounts= accounts;
        }

        //public async Task<bool> DeleteAccount(int AccountNo)
        //{
        //    try
        //    {
        //        var account = await _Context.Accounts.FirstOrDefaultAsync(a => a.AccountNo == AccountNo);
        //        if (account == null)
        //        {
        //            return false;
        //        }

        //        _Context.Accounts.Remove(account);
        //        await _Context.SaveChangesAsync();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
            
        //}

        public async Task<bool> UpdateAccountStatusByAccountNumber(UpdateStatusDTO updateStatusDTO)
        {
            try
            {
                var account = await _Context.Accounts.FirstOrDefaultAsync(acc => acc.AccountNo == updateStatusDTO.AccountNo);
                if (account != null)
                {
                    account.Status = updateStatusDTO.newStatus;
                    await _Context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateAccountStatusByAccountNumber method: {ex.Message}");
                throw;
            }
            return false;
        }

        public async Task<(decimal, decimal, string)> GetInterest(long accountno)
        {
            try
            {
                var account = await _Context.Accounts.FirstOrDefaultAsync(x => x.AccountNo == accountno);
                if (account != null)
                {
                    var acctype = account.AccountType;
                    var accInstance = _accounts.FirstOrDefault(x => x.AccType == acctype);
                    return await accInstance.CalculateInterest((decimal)account.Balance);
                }
                return (0, 0, " ");
            }catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Account>> GetAccounts()
        {
            try
            { 
                var accounts = await _Context.Accounts.ToListAsync();
                return accounts;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAccounts method: {ex.Message}");
                throw;
            }
        }

        public async Task<Account> GetAccountByAccountNumber(long accountNumber)
        {
            try
            {
                var account = await _Context.Accounts.FirstOrDefaultAsync(a => a.AccountNo == accountNumber);
                return account;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAccountByAccountNumber method: {ex.Message}");
                throw;
            }
        }

    }
}
