using System;
using FluentAssertions;
using MarsRover.Models;
using MarsRover.Services;
using MarsRover.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace MarsRover_Tests
{
    [TestFixture]
    public class CommandCentreTests
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
        [TestCase("5 5", "1 2 E", "MMLRLM")]
        public void Execute_MoveRoverPositionInPlateauGrid_Expect_HeadingNorth(string gridSize, string deploy,
            string moves)
        {
            // Arrange          
            var commandCentre = new CommandCentre(_serviceProvider);
            commandCentre.Dispatch(gridSize);
            commandCentre.Dispatch(deploy);
            var rover = _serviceProvider.GetService<IVehicle>();

            // Act
            commandCentre.Dispatch(moves);

            // Assert
            rover.Should().NotBeNull();
            rover.ReportPosition().Should().Be("3 3 N");
        }

        [Test]
        [TestCase("5 5", "5 6 E")]
        public void Execute_OutOfGridBoundInput_Throws_Exception(string gridSize, string deploy)
        {
            // Arrange           
            var commandCentre = new CommandCentre(_serviceProvider);
            commandCentre.Dispatch(gridSize);

            // Act
            Action act = () => commandCentre.Dispatch(deploy);

            // Assert
            act.Should().ThrowExactly<Exception>();
        }

        [Test]
        [TestCase("5 5", "5 5 E", "MRMRMRMR")]
        [TestCase("10 10", "5 6 N", "MRMRMRLR")]
        public void Execute_IfGridNotDefinedLandRover_Throws_Exception(string gridSize, string deploy, string moves)
        {
            // Arrange           
            var commandCentre = new CommandCentre(_serviceProvider);
            
            // Act
            Action act = () => commandCentre.Dispatch(deploy);
            
            // Assert
            act.Should().Throw<Exception>().WithMessage("Plateau surface size not defined yet.");
        }
    }
}
