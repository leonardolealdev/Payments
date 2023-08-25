using AutoMapper;
using MediatR;
using Payments.Domain.Commands;
using Payments.Domain.Entities;
using Payments.Domain.Interfaces;
using Payments.Domain.Query;

namespace Payments.Domain.Handlers
{
    public class PaymentCommandHandler :
        IRequestHandler<CreatePaymentCommand, GenericCommand>,
        IRequestHandler<UpdatePaymentCommand, GenericCommand>,
        IRequestHandler<DeletePaymentCommand, GenericCommand>,
        IRequestHandler<TotalPaymentQuery, GenericCommand>,
        IRequestHandler<StatePaymentSummariesQuery, GenericCommand>,
        IRequestHandler<StatePaymentReportQuery, GenericCommand>,
        IRequestHandler<StateGrossIncomeReportQuery, GenericCommand>
    {
        private readonly IPaymentRepository _repository;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public PaymentCommandHandler(IPaymentRepository repository, IClientRepository clientRepository, IMapper mapper)
        {
            _repository = repository;
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<GenericCommand> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetByContractNumber(request.ContractNumber);
            if (client == null)
                return new GenericCommand(false, $"Nenhum cliente encontrado para esse número de contrato: {request.ContractNumber}", null);

            if (request.PaymentStatus != Enum.PaymentStatus.Paid)
            {
                var totalOpenAmount = await _repository.GetTotalOpenAmount(request.ContractNumber);
                if ((totalOpenAmount + request.Value) > client.GrossIncome)
                    return new GenericCommand(false, $"A soma dos pagamentos em aberto não podem exceder a renda bruta", null);
            }

            var payment = _mapper.Map<Payment>(request);
            return new GenericCommand(true, "Pagamento salvo com sucesso", await _repository.CreateAsync(payment));
        }

        public async Task<GenericCommand> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _repository.GetById(request.Id);
            if (payment == null)
                return new GenericCommand(false, "Nenhum Pagamento encontrado", null);

            payment.Update(request);
            await _repository.UpdateAsync(payment);
            return new GenericCommand(true, "Pagamento alterado com sucesso", null);
        }

        public async Task<GenericCommand> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _repository.GetById(request.Id);
            if (payment == null)
                return new GenericCommand(false, "Nenhum Pagamento encontrado", null);

            await _repository.Delete(payment);
            return new GenericCommand(true, "Pagamento excluído com sucesso", null);
        }

        public async Task<GenericCommand> Handle(TotalPaymentQuery query, CancellationToken cancellationToken)
        {
            var totalPayment = await _repository.TotalPayment();
            return new GenericCommand(true, "Total de pagamento gerado com sucesso", totalPayment);
        }

        public async Task<GenericCommand> Handle(StatePaymentSummariesQuery query, CancellationToken cancellationToken)
        {
            return new GenericCommand(true, "Total de pagamento por estado gerado com sucesso", await _repository.GetStatePaymentSummaries());
        }

        public async Task<GenericCommand> Handle(StatePaymentReportQuery query, CancellationToken cancellationToken)
        {
            return new GenericCommand(true, "Total de pagamento por estado e status de pagamento gerado com sucesso", await _repository.GetStatePaymentReports());
        }

        public async Task<GenericCommand> Handle(StateGrossIncomeReportQuery query, CancellationToken cancellationToken)
        {
            return new GenericCommand(true, "Relatório de média de renda bruta por estado de pagamento gerado com sucesso", await _repository.GetStateGrossIncomeReports());
        }
    }
}
