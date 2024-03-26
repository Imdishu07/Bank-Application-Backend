using BankApplicationProject.DTO;
using BankApplicationProject.Models;
using BankApplicationProject.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BankApplicationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;

        public TransactionController(ITransactionRepository transactionRepository, IAccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw(WithdrawDTO withdrawDTO)
        {
            try
            {
                var account = await _accountRepository.GetAccountByAccountNumber(withdrawDTO.AccountNo);

                if (account == null)
                {
                    return NotFound("Account Not Exist");
                }

                var result = await _transactionRepository.Withdraw(withdrawDTO);
                if (!result)
                    return BadRequest("Withdraw Failed Insufficient Balance");
                else
                    return Ok("Withdraw successful");
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }


        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit(DepositDTO depositDTO)
        {
            try
            {
                var account = await _accountRepository.GetAccountByAccountNumber(depositDTO.AccountNo);

                if (account == null)
                {
                    return NotFound("Account Not Exist");
                }

                if (depositDTO.Amount > 500000)
                {
                    return BadRequest("Maximum ₹500000 Can Be Deposited");
                }

                var result = await _transactionRepository.Deposit(depositDTO);
                if (result)
                    return Ok("Deposit Successful");

                return BadRequest("Unsuccessfull Deposit");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("transactions")]
        public async Task<ActionResult<List<Transaction>>> GetAllTransactions()
        {
           var result = await _transactionRepository.GetAllTransactions();
            return Ok(result);
        }

    }
}
