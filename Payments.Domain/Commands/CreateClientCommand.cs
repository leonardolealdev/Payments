using FluentValidation;
using MediatR;
using System.Text.RegularExpressions;

namespace Payments.Domain.Commands
{
    public class CreateClientCommand : IRequest<GenericCommand>
    {
        public CreateClientCommand(string cpfCnpj, string name, string contractNumber, string city, string state, decimal grossIncome) 
        { 
            CpfCnpj = cpfCnpj;
            Name = name;
            ContractNumber = contractNumber;
            City = city;
            State = state;
            GrossIncome = grossIncome;
        }

        public string CpfCnpj { get; set; }
        public string Name { get; set; }
        public string ContractNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public decimal GrossIncome { get; set; }
    }

    public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
    {
        public CreateClientCommandValidator()
        {
            RuleFor(model => model.CpfCnpj).NotEmpty().WithMessage("CPF/CNPJ é obrigatório.");
            RuleFor(model => model.CpfCnpj).Must(IsCpfCnpjValid).WithMessage("CPF/CNPJ não é válido.");
            RuleFor(model => model.Name).NotEmpty().WithMessage("Nome é obrigatório.");
            RuleFor(model => model.ContractNumber).NotEmpty().WithMessage("Número do contrato é obrigatório.");
            RuleFor(model => model.City).NotEmpty().WithMessage("Cidade é obrigatória.");
            RuleFor(model => model.State).NotEmpty().WithMessage("Estado é obrigatório.");
            RuleFor(model => model.GrossIncome).GreaterThan(0).WithMessage("Renda bruta deve ser maior que zero.");
        }

        private bool IsCpfCnpjValid(string cpfCnpj)
        {
            var texto = RemoveSpecialCaracter(cpfCnpj);
            if (texto.Length == 11)
                return IsCpfValid(texto);
            if (texto.Length == 14)
                return IsCnpjValid(texto);
            return false;
        }

        private string RemoveSpecialCaracter(string input)
        {
            return Regex.Replace(input, "[^0-9]", "");
        }

        
        public bool IsCpfValid(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        public bool IsCnpjValid(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }
    }   

}
