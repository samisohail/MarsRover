using MarsRover.Models;
using MarsRover.Services;
using MarsRover.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using FluentAssertions;

namespace MarsRover_Tests
{
    [TestFixture()]
    public class ExecutorFactoryTests
    {
        private ServiceProvider _serviceProvider;
        [SetUp]
        public void Setup()
        {
            // Arrange
            _serviceProvider = new ServiceCollection()
                .AddSingleton<IVehicle, Rover>()
                .AddSingleton<IPlateau, Plateau>()
                .AddSingleton<ICommandCentre, CommandCentre>()
                .BuildServiceProvider();
        }


        [Test]
        [TestCase("5 5")]
        public void GetCommandExecutor_PlateauCommand_Expect_PlateauCommandExecutor(string command)
        {           
            // Act
            var executor = CommandExecutorFactory.GetCommandExecutor(command, _serviceProvider);
            
           // Assert
            executor.Should().BeOfType<PlateauCommandExecutor>();
        }


        [Test]
        [TestCase("1 2 N")]
        [TestCase("MMRMMRMRRM")]
        public void GetCommandExecutor_RoverCommands_Expect_RoverCommandExecutor(string command)
        {           
            // Act
            var executor = CommandExecutorFactory.GetCommandExecutor(command, _serviceProvider);

            // Assert
            executor.Should().BeOfType<RoverCommandExecutor>();
        }

        [Test]
        [TestCase("1 2 N E W")]
        [TestCase("")]
        public void GetCommandExecutor_UnidentifiedCommand_Expect_Null(string command)
        {           
            // Act
            var executor = CommandExecutorFactory.GetCommandExecutor(command, _serviceProvider);

            // Assert
            executor.Should().BeNull();
        }
    }
}
