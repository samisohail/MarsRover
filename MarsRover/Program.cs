using System;
using System.ComponentModel.Design;
using System.Drawing;
using MarsRover.Models;
using MarsRover.Services;
using MarsRover.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MarsRover
{
    public class Program
    {        
        public static void Main(string[] args)
        {
            //DI set up
            var serviceProvider = ConfigureServices();            

            Console.WriteLine("Enter command or type exit.");
            try
            {
                CommandPad(serviceProvider);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                CommandPad(serviceProvider);
            }
        }

        private static ServiceProvider ConfigureServices()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging(config => config.AddConsole())
                .AddSingleton<IVehicle, Models.Rover>()
                .AddSingleton<IPlateau, Plateau>()
                .AddSingleton<ICommandCentre, CommandCentre>()
                .BuildServiceProvider();

            return serviceProvider;
        }

        private static void CommandPad(IServiceProvider serviceProvider)
        {
            var commandCentre = new CommandCentre(serviceProvider);
            var command = Console.ReadLine();

            while (command != "exit")
            {
                commandCentre.Dispatch(command);
                command = Console.ReadLine();
            }
        }
      
    }
}
