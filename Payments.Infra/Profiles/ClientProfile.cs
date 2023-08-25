using AutoMapper;
using Payments.Domain.Commands;
using Payments.Domain.Entities;
using Payments.Domain.Request;

namespace Payments.Infra.Profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile() 
        {
            CreateMap<CreateClientCommand, Client>();
            CreateMap<CreateClientMessage, CreateClientCommand>();
        }
    }
}
