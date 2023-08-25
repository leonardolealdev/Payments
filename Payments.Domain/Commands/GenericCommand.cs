using Payments.Domain.Commands.Contract;

namespace Payments.Domain.Commands
{
    public class GenericCommand : ICommandResult
    {
        public GenericCommand() { }
        public GenericCommand(bool success, string message, object data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }    
}
