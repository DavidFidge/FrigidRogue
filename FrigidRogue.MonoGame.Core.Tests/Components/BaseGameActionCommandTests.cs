using FrigidRogue.MonoGame.Core.Components;
using FrigidRogue.MonoGame.Core.Interfaces.Components;
using FrigidRogue.MonoGame.Core.Interfaces.Services;
using FrigidRogue.MonoGame.Core.Services;
using FrigidRogue.TestInfrastructure;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using NSubstitute;

namespace FrigidRogue.MonoGame.Core.Tests.Components
{
    [TestClass]
    public class BaseGameActionCommandTests : BaseTest
    {
        private TestGameActionCommand _testGameActionCommand;

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();

            _testGameActionCommand = SetupBaseComponent(new TestGameActionCommand());
            _testGameActionCommand.GameTurnService = Substitute.For<IGameTurnService>();
        }

        [TestMethod]
        public void Execute_Should_Call_GameTurnService_When_Not_Created_Via_Memento()
        {
            // Act
            _testGameActionCommand.Execute();

            // Assert
            _testGameActionCommand.GameTurnService.Received().NextSequenceNumber();
            _testGameActionCommand.GameTurnService.Received().Populate(Arg.Is(_testGameActionCommand.TurnDetails));
        }

        /// <summary>
        /// The command only gets assigned a turn number and sequence number the first time it executes
        /// </summary>
        [TestMethod]
        public void Subsequent_Execute_Calls_Should_Not_Call_GameTurnService()
        {
            // Arrange
            _testGameActionCommand.Execute();
            _testGameActionCommand.GameTurnService.ClearReceivedCalls();

            // Act
            _testGameActionCommand.Execute();

            // Assert
            _testGameActionCommand.GameTurnService.DidNotReceive().NextSequenceNumber();
            _testGameActionCommand.GameTurnService.DidNotReceive().Populate(Arg.Any<ITurnNumber>());
        }

        private class TestGameActionCommand : BaseStatefulGameActionCommand<TestData>
        {
            public override void SetLoadState(IMemento<TestData> memento)
            {
            }

            public override IMemento<TestData> GetSaveState()
            {
                return new Memento<TestData>(new TestData());
            }

            protected override CommandResult ExecuteInternal()
            {
                return CommandResult.Success(this);
            }

            protected override void UndoInternal()
            {
            }
        }

        private class TestData
        {
        }
    }
}