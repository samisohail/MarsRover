using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using MarsRover.Models;
using MarsRover.Models.Enums;
using MarsRover.Services.Contracts;

namespace MarsRover.Services
{
    public static class CommandExecutorFactory
    {        
        private static readonly Regex MoveCommandPattern = new Regex("^[LMR]+$");
        private static readonly Regex PlateauGridSizeCommandPattern = new Regex("^\\d+ \\d+$");
        private static readonly Regex RoverPositionDirectionCommand = new Regex("^\\d+ \\d+ [NSWE]$");

        public static ICommandExecutor GetCommandExecutor(string command, IServiceProvider serviceProvider)
        {
            var commandType = GetCommandType(command);
            switch (commandType)
            {
                case CommandType.MoveRover:
                case CommandType.SetRoverPosition:
                    return new RoverCommandExecutor(commandType, serviceProvider);

                case CommandType.DefineSurface:
                    return new PlateauCommandExecutor(serviceProvider);
                    
                default:
                    return null;
            }
        }

        private static CommandType GetCommandType(string command)
        {
            if (RoverPositionDirectionCommand.IsMatch(command)) return CommandType.SetRoverPosition;
            if (MoveCommandPattern.IsMatch(command)) return CommandType.MoveRover;
            if (PlateauGridSizeCommandPattern.IsMatch(command)) return CommandType.DefineSurface;
            return CommandType.Unknown;
        }
    }
}
