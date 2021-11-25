using DavidFidge.MonoGame.Core.Interfaces.Components;
using DavidFidge.MonoGame.Core.Interfaces.Services;
using DavidFidge.MonoGame.Core.Services;
using DavidFidge.TestInfrastructure;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using NSubstitute;

namespace DavidFidge.MonoGame.Core.Tests.Services
{
    [TestClass]
    public class SaveGameServiceTests : BaseTest
    {
        private SaveGameService _saveGameService;

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();

            _saveGameService = new SaveGameService();
        }

        [TestMethod]
        public void Should_Call_LoadGame_On_All_Registered_SaveableComponents()
        {
            // Arrange
            var saveableComponent = Substitute.For<ISaveable>();
            var saveGameStore = Substitute.For<ISaveGameStore>();

            _saveGameService.Register(saveableComponent);

            // Act
            _saveGameService.LoadGame(saveGameStore);

            // Assert
            saveableComponent.Received().LoadGame(Arg.Is(saveGameStore));

        }

        [TestMethod]
        public void Should_Call_SaveGame_On_All_Registered_SaveableComponents()
        {
            // Arrange
            var saveableComponent = Substitute.For<ISaveable>();
            var saveGameStore = Substitute.For<ISaveGameStore>();

            _saveGameService.Register(saveableComponent);

            // Act
            _saveGameService.SaveGame(saveGameStore);

            // Assert
            saveableComponent.Received().SaveGame(Arg.Is(saveGameStore));
        }
    }
}