using AutoMapper;
using BankApplicationProject.DTO;
using BankApplicationProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankApplicationProject.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly BankManagementContext _Context;
        private readonly IMapper _mapper;

        public CustomerRepository(BankManagementContext Context, IMapper mapper)
        {
            _Context = Context;
            _mapper = mapper;
        }

        public async Task<int> AddCustomer(AddCustomerDTO cusDto)
        {
            try
            {
                if (cusDto != null)
                {
                    if (cusDto.Balance <= 0)
                    {
                        Console.WriteLine("Balance should be greater than 0.");
                        return -1;
                    }

                    string balanceString = cusDto.Balance.ToString();
                    if (balanceString.StartsWith("0"))
                    {
                        Console.WriteLine("Balance should not start with 0.");
                        return -1;
                    }

                    var customer = _mapper.Map<Customer>(cusDto);
                    _Context.Customers.Add(customer);
                    await _Context.SaveChangesAsync();

                    var account = new Account()
                    {
                        AccountType = cusDto.AccountType,
                        Balance = cusDto.Balance,
                        CustomerId = customer.CustomerId,
                        Status = cusDto.Status
                    };

                    _Context.Accounts.Add(account);
                    await _Context.SaveChangesAsync();
                    return 1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AddCustomer method: {ex.Message}");
                throw;
            }
        }


        public async Task<bool> DeleteCustomer(string aadharNumber)
        {
            try
            {
                var customer = await _Context.Customers.FirstOrDefaultAsync(c => c.AadharNumber == aadharNumber);
                if (customer == null)
                {
                    Console.WriteLine($"Customer with Aadhar number '{aadharNumber}' not found.");
                    return false;
                }

                _Context.Customers.Remove(customer);
                await _Context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteCustomer method: {ex.Message}");
                throw;
            }
        }


        public async Task<List<CustomerDTO>> GetAllCustomers()
        {
            try
            {
                var customerlist = new List<CustomerDTO>();
                var customers = await _Context.Customers.ToListAsync();
                var accounts = await _Context.Accounts.ToListAsync();
                customers.ForEach(c =>
                {
                    var acc = accounts.FirstOrDefault(x => x.CustomerId == c.CustomerId);
                    var custAcc = _mapper.Map<CustomerDTO>(c);
                    if (acc != null)
                    {
                        custAcc.AccountNo = acc.AccountNo;
                        custAcc.Balance = (decimal)acc.Balance;
                        custAcc.AccountType = acc.AccountType;
                    }
                    customerlist.Add(custAcc);
                });
                return customerlist;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAllCustomers method: {ex.Message}");
                throw;
            }
        }

        public async Task<CustomerDTO> GetCustomerByAadharNumber(string aadharNumber)
        {
            try
            {
                var customerDTO = new CustomerDTO();

                var customer = await _Context.Customers.FirstOrDefaultAsync(c => c.AadharNumber == aadharNumber);
                if (customer == null)
                    return null;

                var account = await _Context.Accounts.FirstOrDefaultAsync(acc => acc.CustomerId == customer.CustomerId);

                customerDTO = _mapper.Map<CustomerDTO>(customer);
                if (account != null)
                {
                    customerDTO.AccountNo = account.AccountNo;
                    customerDTO.Balance = (decimal)account.Balance;
                    customerDTO.AccountType = account.AccountType;
                }

                return customerDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetCustomerByAadharNumber method: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateCustomerByAadhar(string aadharNumber, UpdateCustomerDTO updateCustomerDto)
        {
            try
            {
                var customer = await _Context.Customers.FirstOrDefaultAsync(c => c.AadharNumber == aadharNumber);
                if (customer == null)
                    return false;

                _mapper.Map(updateCustomerDto, customer);

                await _Context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateCustomerByAadhar method: {ex.Message}");
                throw;
            }
        }
    }
}
