using AutoMapper;
using Moq;
using Payments.Domain.Commands;
using Payments.Domain.Entities;
using Payments.Domain.Enum;
using Payments.Domain.Handlers;
using Payments.Domain.Interfaces;
using Xunit;

namespace Payments.Tests.HandlerTests
{
    public class PaymentCommandHandlerTest
    {
        [Fact]
        public async Task CreatePaymentHandle_Should_Return_Successful_Response()
        {
            // Arrange
            var clientRepositoryMock = new Mock<IClientRepository>();
            var repositoryMock = new Mock<IPaymentRepository>();
            var mapperMock = new Mock<IMapper>();
            var existingClient = new Client("123.456.789-10", "teste", "12345", "Petrolina", "PE", 1000);
            var payment = new Payment("12345", 1, 100, PaymentStatus.Late);
            decimal amount = 500;

            clientRepositoryMock.Setup(repo => repo.GetByContractNumber(It.IsAny<string>())).Returns(Task.FromResult(existingClient)); 
            repositoryMock.Setup(repo => repo.GetTotalOpenAmount(It.IsAny<string>())).Returns(Task.FromResult(amount));
            repositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Payment>())).Returns(Task.FromResult(payment)); 

            var handler = new PaymentCommandHandler(repositoryMock.Object, clientRepositoryMock.Object, mapperMock.Object);

            var request = new CreatePaymentCommand
            {
            ContractNumber = "12345",
            Quota = 1,
            Value = 100,
            PaymentStatus = PaymentStatus.Late
            };

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Pagamento salvo com sucesso", result.Message);
        }

        [Fact]
        public async Task CreatePaymentHandle_Should_Return_Error_Response_When_Client_Not_Found()
        {
            // Arrange
            var clientRepositoryMock = new Mock<IClientRepository>();
            var repositoryMock = new Mock<IPaymentRepository>();
            var mapperMock = new Mock<IMapper>();

            clientRepositoryMock.Setup(repo => repo.GetByContractNumber(It.IsAny<string>())).Returns(Task.FromResult((Client)null)); 

            var handler = new PaymentCommandHandler(repositoryMock.Object, clientRepositoryMock.Object, mapperMock.Object);

            var request = new CreatePaymentCommand
            {
                ContractNumber = "12345",
                Quota = 1,
                Value = 100,
                PaymentStatus = PaymentStatus.Late
            };

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Nenhum cliente encontrado para esse número de contrato", result.Message);
        }

        [Fact]
        public async Task CreatePaymentHandle_Should_Return_Error_Response_When_Open_Payment_Exceeds_Gross_Income()
        {
            // Arrange
            var clientRepositoryMock = new Mock<IClientRepository>();
            var repositoryMock = new Mock<IPaymentRepository>();
            var mapperMock = new Mock<IMapper>();
            var existingClient = new Client("123.456.789-10", "teste", "12345", "Petrolina", "PE", 1000);
            decimal amount = 9500;

            clientRepositoryMock.Setup(repo => repo.GetByContractNumber(It.IsAny<string>())).Returns(Task.FromResult(existingClient)); 
            repositoryMock.Setup(repo => repo.GetTotalOpenAmount(It.IsAny<string>())).Returns(Task.FromResult(amount)); 

            var handler = new PaymentCommandHandler(repositoryMock.Object, clientRepositoryMock.Object, mapperMock.Object);

            var request = new CreatePaymentCommand
            {
                ContractNumber = "12345",
                Quota = 1,
                Value = 100,
                PaymentStatus = PaymentStatus.Late
            };

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("A soma dos pagamentos em aberto não podem exceder a renda bruta", result.Message);
        }

        [Fact]
        public async Task UpdatePaymentHandle_Should_Return_Successful_Response()
        {
            // Arrange
            var repositoryMock = new Mock<IPaymentRepository>();
            var mapperMock = new Mock<IMapper>();

            var payment = new Payment("12345", 1, 100, PaymentStatus.Late);
            repositoryMock.Setup(repo => repo.GetById(1)).Returns(Task.FromResult(payment)); 
            repositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Payment>())).Returns(Task.FromResult(payment)); 

            var handler = new PaymentCommandHandler(repositoryMock.Object, null, mapperMock.Object);

            var request = new UpdatePaymentCommand
            {
                Id = 1,
                ContractNumber = "12345",
                Quota = 1,
                Value = 100,
                PaymentStatus = PaymentStatus.Late
            };

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Pagamento alterado com sucesso", result.Message);
        }

        [Fact]
        public async Task UpdatePaymentHandle_Should_Return_Error_Response_When_Payment_Not_Found()
        {
            // Arrange
            var repositoryMock = new Mock<IPaymentRepository>();

            repositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(Task.FromResult((Payment)null));

            var handler = new PaymentCommandHandler(repositoryMock.Object, null, null);

            var request = new UpdatePaymentCommand
            {
                Id = 1,
                ContractNumber = "12345",
                Quota = 1,
                Value = 100,
                PaymentStatus = PaymentStatus.Late
            };

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Nenhum Pagamento encontrado", result.Message);
        }

        [Fact]
        public async Task DeletePaymentHandle_Should_Return_Successful_Response()
        {
            // Arrange
            var repositoryMock = new Mock<IPaymentRepository>();
            var payment = new Payment("12345", 1, 100, PaymentStatus.Late);

            repositoryMock.Setup(repo => repo.GetById(It.IsAny<long>())).Returns(Task.FromResult(payment));
            repositoryMock.Setup(repo => repo.Delete(It.IsAny<Payment>())).Returns(Task.CompletedTask);

            var handler = new PaymentCommandHandler(repositoryMock.Object, null, null);

            var request = new DeletePaymentCommand(); 

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Pagamento excluído com sucesso", result.Message);
        }

        [Fact]
        public async Task DeletePaymentHandle_Should_Return_Error_Response_When_Payment_Not_Found()
        {
            // Arrange
            var repositoryMock = new Mock<IPaymentRepository>();

            repositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(Task.FromResult((Payment)null));

            var handler = new PaymentCommandHandler(repositoryMock.Object, null, null);

            var request = new DeletePaymentCommand(); 

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Nenhum Pagamento encontrado", result.Message);
        }
    }
}
