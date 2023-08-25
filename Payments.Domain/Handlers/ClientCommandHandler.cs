using AutoMapper;
using MediatR;
using Payments.Domain.Commands;
using Payments.Domain.Entities;
using Payments.Domain.Interfaces;

namespace Payments.Domain.Handlers
{
    public class ClientCommandHandler :
        IRequestHandler<CreateClientCommand, GenericCommand>,
        IRequestHandler<UpdateClientCommand, GenericCommand>,
        IRequestHandler<DeleteClientCommand, GenericCommand>        
    {
        private readonly IClientRepository _repository;
        private readonly IMapper _mapper;

        public ClientCommandHandler(IClientRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GenericCommand> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var existsClient = await _repository.CheckExistingCpfCnpj(request.CpfCnpj, 0);
            if (existsClient)
                return new GenericCommand(false, "Já existe cliente com o mesmo CPF/CNPJ", null);
            var client = _mapper.Map<Client>(request);
            return new GenericCommand(true, "Cliente salvo com sucesso", await _repository.CreateAsync(client));
        }

        public async Task<GenericCommand> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _repository.GetById(request.Id);
            if (client == null)
                return new GenericCommand(false, "Nenhum cliente encontrado", null);

            var existsClient = await _repository.CheckExistingCpfCnpj(request.CpfCnpj, request.Id);
            if (existsClient)
                return new GenericCommand(false, "Já existe cliente com o mesmo CPF/CNPJ", null);

            client.Update(request);
            await _repository.UpdateAsync(client);
            return new GenericCommand(true, "Cliente alterado com sucesso", null);
        }

        public async Task<GenericCommand> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _repository.GetById(request.Id);
            if (client == null)
                return new GenericCommand(false, "Nenhum cliente encontrado", null);

            await _repository.Delete(client);
            return new GenericCommand(true, "Cliente excluído com sucesso", null);
        }
    }
}
