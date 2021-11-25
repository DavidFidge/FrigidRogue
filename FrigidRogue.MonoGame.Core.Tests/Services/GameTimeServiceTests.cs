using System;

using FrigidRogue.MonoGame.Core.Interfaces.Components;
using FrigidRogue.MonoGame.Core.Interfaces.Services;
using FrigidRogue.MonoGame.Core.Messages;
using FrigidRogue.MonoGame.Core.Services;
using FrigidRogue.TestInfrastructure;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;

using NSubstitute;

namespace FrigidRogue.MonoGame.Core.Tests.Services
{
    [TestClass]
    public class GameTimeServiceTests : BaseTest
    {
        private GameTimeService _gameTimeService;
        private FakeStopwatchProvider _fakeStopwatchProvider;

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();

            _fakeStopwatchProvider = new FakeStopwatchProvider();
            _gameTimeService = SetupBaseComponent(new GameTimeService(_fakeStopwatchProvider));

            _gameTimeService.Start();
        }

        [TestMethod]
        public void Reset_Should_Reset_GameTime()
        {
            // Arrange
            _fakeStopwatchProvider.Elapsed = TimeSpan.FromSeconds(1);

            _gameTimeService.IncreaseGameSpeed();
            _gameTimeService.PauseGame();
            _gameTimeService.Update(new GameTime());

            // Act
            _gameTimeService.Reset();

            // Assert
            Assert.AreEqual(TimeSpan.Zero, _fakeStopwatchProvider.Elapsed);
            Assert.AreEqual(TimeSpan.Zero, _gameTimeService.GameTime.ElapsedGameTime);
            Assert.AreEqual(TimeSpan.Zero, _gameTimeService.GameTime.TotalGameTime);
            Assert.AreEqual(TimeSpan.Zero, _gameTimeService.GameTime.ElapsedRealTime);
            Assert.AreEqual(TimeSpan.Zero, _gameTimeService.GameTime.TotalRealTime);
            Assert.IsFalse(_gameTimeService.IsPaused);
        }

        [TestMethod]
        public void Increase_Game_Speed_Then_Reset_Then_Update_With_One_Second_Elapsed_Should_Increment_By_One_Second()
        {
            // Arrange
            _gameTimeService.IncreaseGameSpeed();
            _gameTimeService.Reset();
            _fakeStopwatchProvider.Elapsed = TimeSpan.FromSeconds(1);

            // Act
            _gameTimeService.Update(new GameTime());

            // Assert
            Assert.AreEqual(TimeSpan.FromSeconds(1), _gameTimeService.GameTime.ElapsedGameTime);
            Assert.AreEqual(TimeSpan.FromSeconds(1), _gameTimeService.GameTime.TotalGameTime);
            Assert.AreEqual(TimeSpan.FromSeconds(1), _gameTimeService.GameTime.ElapsedRealTime);
            Assert.AreEqual(TimeSpan.FromSeconds(1), _gameTimeService.GameTime.TotalRealTime);
        }

        [TestMethod]
        public void Reset_Should_Restart_Stopwatch()
        {
            // Arrange
            _fakeStopwatchProvider = Substitute.For<FakeStopwatchProvider>();
            _gameTimeService = SetupBaseComponent(new GameTimeService(_fakeStopwatchProvider));

            // Act
            _gameTimeService.Reset();

            // Assert
            _fakeStopwatchProvider.Received().Restart();
        }

        [TestMethod]
        public void Start_Should_Reset_GameTime()
        {
            // Arrange
            _fakeStopwatchProvider.Elapsed = TimeSpan.FromSeconds(1);

            _gameTimeService.IncreaseGameSpeed();
            _gameTimeService.PauseGame();
            _gameTimeService.Update(new GameTime());

            // Act
            _gameTimeService.Start();

            // Assert
            Assert.AreEqual(TimeSpan.Zero, _fakeStopwatchProvider.Elapsed);
            Assert.AreEqual(TimeSpan.Zero, _gameTimeService.GameTime.ElapsedGameTime);
            Assert.AreEqual(TimeSpan.Zero, _gameTimeService.GameTime.TotalGameTime);
            Assert.AreEqual(TimeSpan.Zero, _gameTimeService.GameTime.ElapsedRealTime);
            Assert.AreEqual(TimeSpan.Zero, _gameTimeService.GameTime.TotalRealTime);
            Assert.IsFalse(_gameTimeService.IsPaused);
        }

        [TestMethod]
        public void LoadGame_Should_Load_GameTime_Data_From_Memento()
        {
            // Arrange
            var memento = new Memento<GameTimeServiceSaveData>(
                new GameTimeServiceSaveData
                {
                    TotalGameTime = TimeSpan.FromSeconds(2)
                });

            var saveGameStore = Substitute.For<ISaveGameStore>();
            saveGameStore.GetFromStore<GameTimeServiceSaveData>().Returns(memento);

            // Act
            _gameTimeService.LoadGame(saveGameStore);

            // Assert
            Assert.AreEqual(TimeSpan.Zero, _gameTimeService.GameTime.ElapsedGameTime);
            Assert.AreEqual(TimeSpan.FromSeconds(2), _gameTimeService.GameTime.TotalGameTime);
            Assert.AreEqual(TimeSpan.Zero, _gameTimeService.GameTime.ElapsedRealTime);
            Assert.AreEqual(TimeSpan.Zero, _gameTimeService.GameTime.TotalRealTime);
        }

        [TestMethod]
        public void SaveGame_Should_Return_Memento_With_GameTime()
        {
            // Arrange
            _fakeStopwatchProvider.Elapsed = TimeSpan.FromSeconds(2);
            _gameTimeService.Update(new GameTime());

            var saveGameStore = Substitute.For<ISaveGameStore>();

            // Act
            _gameTimeService.SaveGame(saveGameStore);


            // Assert
            saveGameStore
                .Received()
                .SaveToStore(
                    Arg.Is<IMemento<GameTimeServiceSaveData>>(m => 
                        m.State.TotalGameTime == _gameTimeService.GameTime.TotalGameTime
                    )
                );
        }

        [TestMethod]
        public void Update_Should_Advance_GameTime()
        {
            // Arrange
            _fakeStopwatchProvider.Elapsed = TimeSpan.FromSeconds(1);

            // Act
            _gameTimeService.Update(new GameTime());

            // Assert
            Assert.AreEqual(TimeSpan.FromSeconds(1), _gameTimeService.GameTime.ElapsedGameTime);
            Assert.AreEqual(TimeSpan.FromSeconds(1), _gameTimeService.GameTime.TotalGameTime);
            Assert.AreEqual(TimeSpan.FromSeconds(1), _gameTimeService.GameTime.ElapsedRealTime);
            Assert.AreEqual(TimeSpan.FromSeconds(1), _gameTimeService.GameTime.TotalRealTime);
        }

        [TestMethod]
        public void Update_Should_Do_Nothing_If_Stopped()
        {
            // Arrange
            _gameTimeService.Stop();
            _fakeStopwatchProvider.Elapsed = TimeSpan.FromSeconds(1);

            // Act
            _gameTimeService.Update(new GameTime());

            // Assert
            Assert.AreEqual(TimeSpan.FromSeconds(0), _gameTimeService.GameTime.ElapsedGameTime);
            Assert.AreEqual(TimeSpan.FromSeconds(0), _gameTimeService.GameTime.TotalGameTime);
            Assert.AreEqual(TimeSpan.FromSeconds(0), _gameTimeService.GameTime.ElapsedRealTime);
            Assert.AreEqual(TimeSpan.FromSeconds(0), _gameTimeService.GameTime.TotalRealTime);

            _gameTimeService
                .Mediator
                .DidNotReceive()
                .Publish(Arg.Any<GameTimeUpdateNotification>()
                );
        }

        [TestMethod]
        public void Update_Should_Publish_GameTimeUpdateNotification()
        {
            // Arrange
            _fakeStopwatchProvider.Elapsed = TimeSpan.FromSeconds(1);

            // Act
            _gameTimeService.Update(new GameTime());

            // Assert
            _gameTimeService
                .Mediator
                .Received()
                .Publish(Arg.Is<GameTimeUpdateNotification>(m => m.GameTime == _gameTimeService.GameTime));
        }

        [TestMethod]
        public void Update_Should_Advance_GameTime_By_Elapsed_Amount_Given_2_Different_Updates()
        {
            // Arrange
            _fakeStopwatchProvider.Elapsed = TimeSpan.FromSeconds(1);

            // Act
            _gameTimeService.Update(new GameTime());

            _fakeStopwatchProvider.Elapsed = TimeSpan.FromSeconds(3);

            _gameTimeService.Update(new GameTime());

            // Assert
            Assert.AreEqual(TimeSpan.FromSeconds(2), _gameTimeService.GameTime.ElapsedGameTime);
            Assert.AreEqual(TimeSpan.FromSeconds(2), _gameTimeService.GameTime.ElapsedRealTime);
            Assert.AreEqual(TimeSpan.FromSeconds(3), _gameTimeService.GameTime.TotalGameTime);
            Assert.AreEqual(TimeSpan.FromSeconds(3), _gameTimeService.GameTime.TotalRealTime);
        }

        [TestMethod]
        public void PauseGame_Should_Not_Advance_GameTime_When_Updates_Occur()
        {
            // Arrange
            _gameTimeService.PauseGame();

            _fakeStopwatchProvider.Elapsed = TimeSpan.FromSeconds(1);

            // Act
            _gameTimeService.Update(new GameTime());

            // Assert
            Assert.AreEqual(TimeSpan.Zero, _gameTimeService.GameTime.ElapsedGameTime);
            Assert.AreEqual(TimeSpan.FromSeconds(1), _gameTimeService.GameTime.ElapsedRealTime);
            Assert.AreEqual(TimeSpan.Zero, _gameTimeService.GameTime.TotalGameTime);
            Assert.AreEqual(TimeSpan.FromSeconds(1), _gameTimeService.GameTime.TotalRealTime);
            Assert.IsTrue(_gameTimeService.IsPaused);
        }

        [TestMethod]
        public void ResumeGame_Should_Advance_GameTime_When_Updates_Occur()
        {
            // Arrange
            _gameTimeService.PauseGame();

            _fakeStopwatchProvider.Elapsed = TimeSpan.FromSeconds(1);

            _gameTimeService.Update(new GameTime());

            _fakeStopwatchProvider.Elapsed = TimeSpan.FromSeconds(3);

            // Act
            _gameTimeService.ResumeGame();
            _gameTimeService.Update(new GameTime());

            // Assert
            Assert.AreEqual(TimeSpan.FromSeconds(2), _gameTimeService.GameTime.ElapsedGameTime);
            Assert.AreEqual(TimeSpan.FromSeconds(2), _gameTimeService.GameTime.ElapsedRealTime);
            Assert.AreEqual(TimeSpan.FromSeconds(2), _gameTimeService.GameTime.TotalGameTime);
            Assert.AreEqual(TimeSpan.FromSeconds(3), _gameTimeService.GameTime.TotalRealTime);
            Assert.IsFalse(_gameTimeService.IsPaused);
        }

        [TestMethod]
        [DataRow(1, 2000)]
        [DataRow(2, 4000)]
        [DataRow(3, 8000)]
        [DataRow(4, 8000)]
        public void IncreaseGameSpeed_Should_Advance_GameTime_At_Faster_Rate_Than_RealTime(
            int numberOfTimes,
            double expectedMilliseconds)
        {
            // Arrange
            _fakeStopwatchProvider.Elapsed = TimeSpan.FromSeconds(1);

            // Act
            for (int i = 0; i < numberOfTimes; i++)
                _gameTimeService.IncreaseGameSpeed();

            _gameTimeService.Update(new GameTime());

            // Assert
            Assert.AreEqual(TimeSpan.FromMilliseconds(expectedMilliseconds), _gameTimeService.GameTime.ElapsedGameTime);
            Assert.AreEqual(TimeSpan.FromMilliseconds(expectedMilliseconds), _gameTimeService.GameTime.TotalGameTime);
            Assert.AreEqual(TimeSpan.FromSeconds(1), _gameTimeService.GameTime.ElapsedRealTime);
            Assert.AreEqual(TimeSpan.FromSeconds(1), _gameTimeService.GameTime.TotalRealTime);
        }

        [TestMethod]
        [DataRow(1, 500)]
        [DataRow(2, 250)]
        public void DecreaseGameSpeed_Should_Advance_GameTime_At_Slower_Rate_Than_RealTime(
            int numberOfTimes,
            double expectedMilliseconds)
        {
            // Arrange
            _fakeStopwatchProvider.Elapsed = TimeSpan.FromSeconds(1);

            // Act
            for (int i = 0; i < numberOfTimes; i++)
                _gameTimeService.DecreaseGameSpeed();

            _gameTimeService.Update(new GameTime());

            // Assert
            Assert.AreEqual(TimeSpan.FromMilliseconds(expectedMilliseconds), _gameTimeService.GameTime.ElapsedGameTime);
            Assert.AreEqual(TimeSpan.FromMilliseconds(expectedMilliseconds), _gameTimeService.GameTime.TotalGameTime);
            Assert.AreEqual(TimeSpan.FromSeconds(1), _gameTimeService.GameTime.ElapsedRealTime);
            Assert.AreEqual(TimeSpan.FromSeconds(1), _gameTimeService.GameTime.TotalRealTime);
        }

        [TestMethod]
        public void DecreaseGameSpeed_Should_Pause_Game_At_Slowest_Rate()
        {
            // Arrange
            _fakeStopwatchProvider.Elapsed = TimeSpan.FromSeconds(1);

            // Act
            for (int i = 0; i < 4; i++)
                _gameTimeService.DecreaseGameSpeed();

            _gameTimeService.Update(new GameTime());

            // Assert
            Assert.IsTrue(_gameTimeService.IsPaused);
            Assert.AreEqual(TimeSpan.Zero, _gameTimeService.GameTime.ElapsedGameTime);
            Assert.AreEqual(TimeSpan.Zero, _gameTimeService.GameTime.TotalGameTime);
            Assert.AreEqual(TimeSpan.FromSeconds(1), _gameTimeService.GameTime.ElapsedRealTime);
            Assert.AreEqual(TimeSpan.FromSeconds(1), _gameTimeService.GameTime.TotalRealTime);
        }

        [TestMethod]
        public void IncreaseGameSpeed_Should_Resume_Game_At_Same_Speed_When_Paused()
        {
            // Arrange
            _fakeStopwatchProvider.Elapsed = TimeSpan.FromSeconds(1);

            // Act
            _gameTimeService.PauseGame();
            _gameTimeService.IncreaseGameSpeed();
            _gameTimeService.Update(new GameTime());

            // Assert
            Assert.IsFalse(_gameTimeService.IsPaused);
            Assert.AreEqual(TimeSpan.FromSeconds(1), _gameTimeService.GameTime.ElapsedGameTime);
            Assert.AreEqual(TimeSpan.FromSeconds(1), _gameTimeService.GameTime.TotalGameTime);
            Assert.AreEqual(TimeSpan.FromSeconds(1), _gameTimeService.GameTime.ElapsedRealTime);
            Assert.AreEqual(TimeSpan.FromSeconds(1), _gameTimeService.GameTime.TotalRealTime);
        }
    }
}
