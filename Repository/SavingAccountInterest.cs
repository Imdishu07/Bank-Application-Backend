using BankApplicationProject.Models;
using BankManagementProject.Repository;
using Microsoft.EntityFrameworkCore;

namespace BankApplicationProject.Repository
{
    public class SavingAccountInterest : IAccount
    {
        private BankManagementContext _Context;

        public string AccType { get { return "Saving"; } }
        public SavingAccountInterest(BankManagementContext Context)
        {
            _Context = Context;
        }

        public async Task<(decimal, decimal,string)> CalculateInterest(decimal amount)
        {
            var interestRate = await _Context.Interests.FirstOrDefaultAsync(x => x.TypeName == "Saving");
            var interestAmount = (decimal)interestRate.Interest1 * amount / 100;
            return ((decimal)interestRate.Interest1, interestAmount,"Saving");
        }
    }
}