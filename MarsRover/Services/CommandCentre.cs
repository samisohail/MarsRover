using System;
using MarsRover.Services.Contracts;


namespace MarsRover.Services
{
    public class CommandCentre : ICommandCentre
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandCentre(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public void Dispatch(string command)
        {
            var executor = CommandExecutorFactory.GetCommandExecutor(command, _serviceProvider);
            executor?.Execute(command);
        }
    }
}
