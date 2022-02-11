using MediatR;
using Tempus.Abstractions.Commands;

namespace Tempus.Dispatch.MediatR.Commands.Bindings
{
    public class MediatRCommandQueue : ICommandQueue
    {
        private readonly IMediator _mediator;
        private readonly IEnumerable<ICommandStore> _stores;

        public MediatRCommandQueue(IMediator mediator, IEnumerable<ICommandStore> stores)
        {
            _mediator = mediator;
            _stores = stores;
        }

        public void Cancel(Guid command)
        {
            throw new NotImplementedException();
        }

        public void Complete(Guid command)
        {
            throw new NotImplementedException();
        }

        public void Override<T>(Action<T> action, Guid tenant) where T : ICommand
        {
            throw new NotImplementedException();
        }

        public void Ping()
        {
            throw new NotImplementedException();
        }

        public void Schedule(ICommand command, DateTimeOffset at)
        {
            throw new NotImplementedException();
        }

        public void Send(ICommand command)
        {
            var wrapper = typeof(TempusMediatRCommandWrapper<>);
            var type = command.GetType();
            var requestType = wrapper.MakeGenericType(type);
            var request = Activator.CreateInstance(requestType);

            var commandProperty = request.GetType().GetProperty("Command");
            commandProperty.SetValue(request, command);

            var startDateTime = DateTimeOffset.UtcNow;
            _mediator.Send(request).Wait();
            var endDateTime = DateTimeOffset.UtcNow;

            foreach (var store in _stores)
            {
                ISerializedCommand serialized = store.Serialize(command);
                serialized.SendStarted = startDateTime;
                serialized.SendCompleted = endDateTime;
                serialized.SendStatus = ISerializedCommand.Status.Completed;
                store.Save(serialized, true);
            }
        }

        public void Start(Guid command)
        {
            throw new NotImplementedException();
        }

        public void Subscribe<T>(Action<T> action) where T : ICommand
        {
            throw new NotImplementedException();
        }
    }
}
