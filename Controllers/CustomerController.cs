using BankApplicationProject.DTO;
using BankApplicationProject.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankApplicationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpPost("AddCustomer")]
        public async Task<IActionResult> AddCustomer([FromBody] AddCustomerDTO customerDTO)
        {
            int isAdded = await _customerRepository.AddCustomer(customerDTO);

            if (isAdded == 1)
            {
                return Ok("Customer Added");
            }
            return BadRequest("Something Went Wrong");
        }

        [HttpDelete("{aadharNumber}")]
        public async Task<ActionResult> DeleteCustomer(string aadharNumber)
        {
            var result = await _customerRepository.DeleteCustomer(aadharNumber);
            if (result)
                return Ok("success");
            else
                return BadRequest("Something Went Wrong");
        }


        [HttpGet("GetAllCustomers")]

        public async Task<ActionResult<List<CustomerDTO>>> GetAllCustomers()
        {
            var customers = await _customerRepository.GetAllCustomers();
            return Ok(customers);
        }

        [HttpGet("{aadharNumber}")]
        public async Task<IActionResult> GetCustomerByAadharNumber(string aadharNumber)
        {
            var customerDTO = await _customerRepository.GetCustomerByAadharNumber(aadharNumber);
            if (customerDTO == null)
                return NotFound("Customer not found");

            return Ok(customerDTO);
        }

        [HttpPut("{aadharNumber}")]
        public async Task<IActionResult> UpdateCustomerByAadhar(string aadharNumber, [FromBody] UpdateCustomerDTO updateCustomerDto)
        {
            bool result = await _customerRepository.UpdateCustomerByAadhar(aadharNumber, updateCustomerDto);
            if (result)
            {
                return Ok("Customer updated successfully.");
            }
            else
            {
                return NotFound("Customer not found.");
            }
        }

    }
}
