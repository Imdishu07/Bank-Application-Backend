using BankApplicationProject.DTO;

namespace BankApplicationProject.Repository
{
    public interface ICustomerRepository
    {
        Task<int> AddCustomer(AddCustomerDTO cusDto);
        public Task<bool> DeleteCustomer(string aadharNumber);
        public Task<List<CustomerDTO>> GetAllCustomers();

        public Task<CustomerDTO> GetCustomerByAadharNumber(string aadharNumber);
        public Task<bool> UpdateCustomerByAadhar(string aadharNumber, UpdateCustomerDTO updateCustomerDto);
    }
}
