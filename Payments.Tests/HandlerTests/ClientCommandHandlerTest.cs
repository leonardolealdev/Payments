using AutoMapper;
using Moq;
using Payments.Domain.Commands;
using Payments.Domain.Entities;
using Payments.Domain.Handlers;
using Payments.Domain.Interfaces;
using Xunit;

namespace Payments.Tests.HandlerTests
{
    public class ClientCommandHandlerTest
    {
        [Fact]
        public async Task CreateClientHandle_Should_Return_Successful_Response()
        {
            // Arrange
            var repositoryMock = new Mock<IClientRepository>();
            var mapperMock = new Mock<IMapper>();
            var request = new CreateClientCommand("123.456.789-10", "teste", "12345", "Petrolina", "PE", 1000);

            repositoryMock.Setup(repo => repo.CheckExistingCpfCnpj(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(false); 
            mapperMock.Setup(mapper => mapper.Map<Client>(It.IsAny<CreateClientCommand>())).Returns(new Client("123.456.789-10", "teste", "12345", "Petrolina", "PE", 1000)); 
            repositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Client>())).ReturnsAsync(new Client("123.456.789-10", "teste", "12345", "Petrolina", "PE", 1000)); 

            var handler = new ClientCommandHandler(repositoryMock.Object, mapperMock.Object);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Cliente salvo com sucesso", result.Message);
        }

        [Fact]
        public async Task CreateClientHandle_Should_Return_Error_Response_When_Client_Exists()
        {
            // Arrange
            var request = new CreateClientCommand("123.456.789-10", "teste", "12345", "Petrolina", "PE", 1000);
            var repositoryMock = new Mock<IClientRepository>();
            var mapperMock = new Mock<IMapper>();

            repositoryMock.Setup(repo => repo.CheckExistingCpfCnpj("123.456.789-10", 0)).Returns(Task.FromResult(true));

            var handler = new ClientCommandHandler(repositoryMock.Object, mapperMock.Object);


            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Já existe cliente com o mesmo CPF/CNPJ", result.Message);
        }

        [Fact]
        public async Task UpdateClientHandle_Should_Return_Successful_Response()
        {
            // Arrange
            var repositoryMock = new Mock<IClientRepository>();
            var existingClient = new Client("123.456.789-10", "teste", "12345", "Petrolina", "PE", 1000);
            var request = new UpdateClientCommand(1, "123.456.789-10", "teste", "12345", "Petrolina", "PE", 1000);

            repositoryMock.Setup(repo => repo.GetById(1)).Returns(Task.FromResult(existingClient));
            repositoryMock.Setup(repo => repo.CheckExistingCpfCnpj("123.456.789-10", 0)).Returns(Task.FromResult(false));
            repositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Client>())).Returns(Task.CompletedTask); 

            var handler = new ClientCommandHandler(repositoryMock.Object, null);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Cliente alterado com sucesso", result.Message);
        }

        [Fact]
        public async Task UpdateClientHandle_Should_Return_Error_Response_When_Client_Not_Found()
        {
            // Arrange
            var repositoryMock = new Mock<IClientRepository>();
            var request = new UpdateClientCommand(1, "123.456.789-10", "teste", "12345", "Petrolina", "PE", 1000);

            repositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(Task.FromResult((Client)null)); 

            var handler = new ClientCommandHandler(repositoryMock.Object, null);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Nenhum cliente encontrado", result.Message);
        }

        [Fact]
        public async Task DeleteClientHandle_Should_Return_Successful_Response()
        {
            // Arrange
            var repositoryMock = new Mock<IClientRepository>();

            var existingClient = new Client("123.456.789-10", "teste", "12345", "Petrolina", "PE", 1000);
            repositoryMock.Setup(repo => repo.GetById(1)).Returns(Task.FromResult(existingClient));
            repositoryMock.Setup(repo => repo.Delete(It.IsAny<Client>())).Returns(Task.CompletedTask); 

            var handler = new ClientCommandHandler(repositoryMock.Object, null);
            var request = new DeleteClientCommand(1); 

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Cliente excluído com sucesso", result.Message);
        }

        [Fact]
        public async Task DeleteClientHandle_Should_Return_Error_Response_When_Client_Not_Found()
        {
            // Arrange
            var repositoryMock = new Mock<IClientRepository>();

            repositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(Task.FromResult((Client)null));

            var handler = new ClientCommandHandler(repositoryMock.Object, null);
            var request = new DeleteClientCommand(1); 

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Nenhum cliente encontrado", result.Message);
        }
    }
}
