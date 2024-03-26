using BankApplicationProject.Repository;
using BankManagementProject.Repository;
using Microsoft.Extensions.DependencyInjection; // Ensure you have this namespace imported
using System;
using System.Collections.Generic;

namespace BankApplicationProject
{
    public class InterestHelper
    {
        private readonly IServiceProvider _serviceProvider;

        public InterestHelper(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IAccount DetectAccount(string accountType)
        {
            return accountType.ToLower() switch
            {
                "saving" => _serviceProvider.GetRequiredService<SavingAccountInterest>(),
                "current" => _serviceProvider.GetRequiredService<CurrentAccountInterest>(),
                _ => throw new InvalidOperationException("Invalid account type"),
            };
        }
    }
}
