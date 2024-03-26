using AutoMapper;
using BankApplicationProject.DTO;
using BankApplicationProject.Models;

namespace BankApplicationProject.Helper
{
    public class ApplicationModelMapping : Profile
    {
        public ApplicationModelMapping()
        {
            CreateMap<AddCustomerDTO, Customer>().ReverseMap();
            CreateMap<AddCustomerDTO, Account>().ReverseMap();
            CreateMap<Customer, CustomerDTO>();
            CreateMap<UpdateCustomerDTO, Customer>().ReverseMap();
            CreateMap<WithdrawDTO, Transaction>().ReverseMap();
            CreateMap<DepositDTO, Transaction>().ReverseMap();
            CreateMap<Transaction, TransactionDTO>().ReverseMap();
            //CreateMap<Transaction, TransactionDTO>()
            //  .ForMember(dest => dest.Account, opt => opt.MapFrom(src => new AccountDTO { AccountNo = src.Account.AccountNo }));
        }
    }
}
