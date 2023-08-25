using AutoMapper;
using Payments.Domain.Commands;
using Payments.Domain.Entities;

namespace Payments.Infra.Profiles
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile() 
        {
            CreateMap<CreatePaymentCommand, Payment>();
        }
    }
}
