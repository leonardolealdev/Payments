using FluentValidation;
using MediatR;
using Payments.Domain.Enum;

namespace Payments.Domain.Commands
{
    public class CreatePaymentCommand : IRequest<GenericCommand>
    {
        public string ContractNumber { get; set; }
        public int Quota { get; set; }
        public decimal Value { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }

    public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentCommandValidator()
        {
            RuleFor(payment => payment.ContractNumber).NotEmpty().WithMessage("Número do contrato é obrigatório.");
            RuleFor(payment => payment.Quota).GreaterThan(0).WithMessage("Parcela é obrigatória.");
            RuleFor(payment => payment.Value).GreaterThan(0).WithMessage("Valor é obrigatório.");
            RuleFor(payment => payment.PaymentStatus).IsInEnum().WithMessage("Status do pagamento inválido.");
        }        
    }   

}
