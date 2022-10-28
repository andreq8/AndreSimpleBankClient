using AutoMapper;
using OpenBankClient.Data.Models;
using OpenBankClient.Data.Services.Base;

namespace OpenBankClient
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountResponse>().ReverseMap();
            CreateMap<AccountDetails, GetAccountResponse>().ReverseMap();
            CreateMap<Data.Models.Movim, Data.Services.Base.Movim>().ReverseMap();
            CreateMap<CreateAccount, AccountRequest>();
            CreateMap<Transfer, TransferRequest>();
            CreateMap<CreateUserRequest, CreateUser>().ReverseMap();
            CreateMap<User, LoginUserRequest>().ReverseMap();
        }

    }
}
