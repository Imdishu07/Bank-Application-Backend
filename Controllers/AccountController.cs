using BankApplicationProject.DTO;
using BankApplicationProject.Models;
using BankApplicationProject.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankApplicationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;

        }

        //[HttpDelete("AccountNo")]
        //public async Task<IActionResult> DeleteAccount(int AccountNo)
        //{
        //    var result = await _accountRepository.DeleteAccount(AccountNo);
        //    if (result)
        //    {
        //        return Ok("Account Deleted Successfully");
        //    }
        //    else
        //    {
        //        return BadRequest("Account Not Found");
        //    }
        //}

        [HttpPut("UpdateStatus")]
        public async Task<ActionResult> UpdateAccountStatusByAccountNumber([FromBody] UpdateStatusDTO updateStatusDTO)
        {
            var account = await _accountRepository.GetAccountByAccountNumber(updateStatusDTO.AccountNo);

            if (account == null)
            {
                return NotFound("Account Not Exist");
            }

            var result = await _accountRepository.UpdateAccountStatusByAccountNumber(updateStatusDTO);

            if (result)
            {
                return Ok("Successfully Updated");
            }
            else
            {
                return BadRequest("Failed to change status");
            }
        }



        [HttpGet("GetInterest/{accountno:int}")]

        public async Task<ActionResult> GetInterest(int accountno)
        {
            var account = await _accountRepository.GetAccountByAccountNumber(accountno);

            if (account == null)
            {
                return NotFound("Account Not Exist");
            }
            var (interestRate, interestAmount, acctype) = await _accountRepository.GetInterest(accountno);
            var result = new {InterestRate = interestRate,InterestAmmount =  interestAmount,AccountType = acctype};
            return Ok(result);
        }

        [HttpGet("GetAccounts")]
        public async Task<IActionResult> GetAccounts()
        {
            try
            {
                var accounts = await _accountRepository.GetAccounts();
                if (accounts == null)
                {
                    return NotFound();
                }
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return BadRequest("Accounts Not Found");
            }
        }
    }
}
