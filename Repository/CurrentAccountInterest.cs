using BankApplicationProject.Models;
using BankManagementProject.Repository;
using Microsoft.EntityFrameworkCore;

namespace BankApplicationProject.Repository
{
    public class CurrentAccountInterest : IAccount
    {
        public string AccType { get { return "Current"; } }
        private BankManagementContext _Context;
        public CurrentAccountInterest(BankManagementContext context)
        {
            _Context = context;
        }

        public async Task<(decimal, decimal,string)> CalculateInterest(decimal amount)
        {
            var interestRate = await _Context.Interests.FirstOrDefaultAsync(x => x.TypeName == "Current");
            var interestAmount = (decimal)interestRate.Interest1 * amount / 100;
            return ((decimal)interestRate.Interest1, interestAmount, "Current");
        }
    }
}