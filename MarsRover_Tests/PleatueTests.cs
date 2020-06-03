using MarsRover.Models;
using MarsRover.Services;
using MarsRover.Services.Contracts;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;
namespace Tests
{
    [TestFixture]
    public class Tests
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
        [TestCase("3 6")]
        [TestCase("5 10")]
        [TestCase("2 2")]
        public void Execute_CreatePlateauSetSize_ValidInputs_Success(string command)
        {
            // Arrange
            var commandItems = command.Split(' ');
            var expectedWidth = int.Parse(commandItems[0]) + 1;
            var expectedHeight = int.Parse(commandItems[1]) + 1;

            var commandCentre = new CommandCentre(_serviceProvider);
            
            // Act
            commandCentre.Dispatch(command);

            // Assert
            var plateau = _serviceProvider.GetService<IPlateau>();
            plateau.Should().NotBeNull();
            plateau.Size.Should().NotBeNull();
            plateau.Size.Should().NotBeNull();
            plateau.Size.Width.Should().Be(expectedWidth);
            plateau.Size.Height.Should().Be(expectedHeight);
        }

        [Test]
        [TestCase("5 -1")]
        [TestCase("-1 2")]
        [TestCase("-1 a")]

        public void Execute_CreatePlateau_InValidInputs_DoesnotCreatePlateau(string command)
        {
            // Arrange          
            var commandCentre = new CommandCentre(_serviceProvider);

            // Act
            commandCentre.Dispatch(command);            

            // Assert
            var plateau = _serviceProvider.GetService<IPlateau>();            
            plateau.Size.Should().BeNull();            
        }

    }
}
