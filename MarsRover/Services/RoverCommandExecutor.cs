using System;
using System.Collections.Generic;
using System.Text;
using MarsRover.Models;
using MarsRover.Models.Enums;
using MarsRover.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace MarsRover.Services
{
    public class RoverCommandExecutor : ICommandExecutor
    {
        private readonly IVehicle _rover;
        private readonly CommandType _commandType;
        
        public RoverCommandExecutor(CommandType commandType, IServiceProvider serviceProvider)
        {            
            _commandType = commandType;
            _rover = serviceProvider.GetService<IVehicle>();
        }
        public void Execute(string command)
        {
            switch (_commandType)
            {
                case CommandType.MoveRover:
                    MoveRover(command, _rover);
                    break;
                case CommandType.SetRoverPosition:
                    SetPosition(command, _rover);
                    break;                
            }
        }

        private void MoveRover(string command, IVehicle rover)
        {
            foreach (var move in command)
            {
                var movement = Enum.Parse<Movement>(move.ToString());
                rover.Move(movement);
            }
            ReportPosition(rover);
        }

        private void SetPosition(string command, IVehicle rover)
        {
            ParseCommand(command, out var x, out var y, out var cardinal);
            rover.Deploy(x, y, cardinal);
        }

        private void ParseCommand(string command, out int x, out int y, out Cardinal cardinal)
        {
            var splitCommand = command.Split(' ');
            x = int.Parse(splitCommand[0]);
            y = int.Parse(splitCommand[1]);
            cardinal = Enum.Parse<Cardinal>(splitCommand[2]);
        }

        private void ReportPosition(IVehicle rover)
        {
            Console.WriteLine(rover.ReportPosition());
        }
    }
}
