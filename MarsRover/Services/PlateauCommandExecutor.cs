using System;
using System.Collections.Generic;
using System.Text;
using MarsRover.Models;
using MarsRover.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace MarsRover.Services
{
    public class PlateauCommandExecutor : ICommandExecutor
    {
        private readonly IPlateau _plateau;
        public PlateauCommandExecutor(IServiceProvider serviceProvider)
        {
            _plateau = serviceProvider.GetService<IPlateau>();
        }
        public void Execute(string command)
        {
            ParseCommand(command, out var width, out var height);
            _plateau.DefineSurface(width, height);
        }
        private static void ParseCommand(string command, out int width, out int height)
        {
            var splitCommand = command.Split(' ');
            width = int.Parse(splitCommand[0]) + 1;
            height = int.Parse(splitCommand[1]) + 1;
        }
    }
}
