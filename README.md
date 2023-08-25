# Payments

API de criação de pagamentos desenvolvida com .NET 6

## tecnologias contidas:

- CQRS pattern com MediatR
- DependencyInjection
- EntityFrameworkCore
- AutoMapper
- Swagger Documentation
- Identity
- ApiVersioning
- Mensageria com RabbitMq


## Author

- [@leonardolealdev](https://github.com/leonardolealdev)

# Para rodar o projeto siga as intruções abaixo:

## 1. Abrir o Package Manager Console do Visual Studio(Tools -> NuGet Package Manager -> Package Manager Console)

## 2. Apontar o Default Project para Payments.API

## 3. Rodar comando "update-database -context ApplicationDbContext

## 4. Apontar o Default Project para Payments.Infra

## 5. Rodar comando "update-database -context PaymentDbContext

## 6. Na Solution explorer do Visual Studio selecione Payments.API como Startup project e rode o projeto

## 7. Projeto funcionando!

Obs 1: Necessário ter PostgreSql intalado com versão igual ou superior a 10

Obs2: Criei uma controller Producer para disparar uma mensagem de criação de cliente ou de pagamento para a fila




