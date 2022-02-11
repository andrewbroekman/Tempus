using MediatR;
using Tempus.Abstractions.Commands;

namespace Tempus.Dispatch.MediatR.Commands.Bindings
{
    public class TempusMediatRCommandWrapper<TCommand> : IRequest where TCommand : ICommand
    {
        public TCommand Command { get; init; }
    }
}
