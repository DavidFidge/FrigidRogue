using System;

using FrigidRogue.MonoGame.Core.Services;
using FrigidRogue.TestInfrastructure;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FrigidRogue.MonoGame.Core.Tests.Services
{
    [TestClass]
    public class GameTurnServiceTests : BaseTest
    {
        private GameTurnService _gameTurnService;

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();

            _gameTurnService = SetupBaseComponent(new GameTurnService());
        }

        [TestMethod]
        public void Should_Create_With_Turn_Number_1()
        {
            // Assert
            Assert.AreEqual(1, _gameTurnService.TurnNumber);
        }

        [TestMethod]
        public void Reset_Should_Set_Turn_Number_To_1()
        {
            // Arrange
            _gameTurnService.NextTurn();

            // Act
            _gameTurnService.Reset();

            // Assert
            Assert.AreEqual(1, _gameTurnService.TurnNumber);
        }

        [TestMethod]
        public void Should_Create_With_Sequence_Number_0()
        {
            // Assert
            Assert.AreEqual(0, _gameTurnService.SequenceNumber);
        }

        [TestMethod]
        public void Reset_Should_Set_Sequence_Number_To_0()
        {
            // Arrange
            _gameTurnService.NextSequenceNumber();

            // Act
            _gameTurnService.Reset();

            // Assert
            Assert.AreEqual(0, _gameTurnService.SequenceNumber);
        }

        [TestMethod]
        public void NextTurn_Should_Increment_TurnNumber()
        {
            // Act
            var nextTurn = _gameTurnService.NextTurn();

            // Assert
            Assert.AreEqual(2, nextTurn);
            Assert.AreEqual(2, _gameTurnService.TurnNumber);
        }

        [TestMethod]
        public void NextSequenceNumber_Should_Increment_SequenceNumber()
        {
            // Act
            var nextSequenceNumber = _gameTurnService.NextSequenceNumber();

            // Assert
            Assert.AreEqual(1, nextSequenceNumber);
            Assert.AreEqual(1, _gameTurnService.SequenceNumber);
        }

        [TestMethod]
        public void GetSequenceNumberRangeForTurn_Should_Get_Sequence_Numbers_Used_While_Turn_Was_Active()
        {
            // Arrange
            _gameTurnService.NextSequenceNumber();
            _gameTurnService.NextSequenceNumber();
            _gameTurnService.NextTurn();
            _gameTurnService.NextTurn();
            _gameTurnService.NextSequenceNumber();
            _gameTurnService.NextTurn();
            _gameTurnService.NextSequenceNumber();
            _gameTurnService.NextSequenceNumber();
            _gameTurnService.NextSequenceNumber();
            _gameTurnService.NextTurn();

            // Act
            var sequenceNumberRangeTurn1 = _gameTurnService.GetSequenceNumberRangeForTurn(1);
            var sequenceNumberRangeTurn2 = _gameTurnService.GetSequenceNumberRangeForTurn(2);
            var sequenceNumberRangeTurn3 = _gameTurnService.GetSequenceNumberRangeForTurn(3);
            var sequenceNumberRangeTurn4 = _gameTurnService.GetSequenceNumberRangeForTurn(4);
            var sequenceNumberRangeTurn5 = _gameTurnService.GetSequenceNumberRangeForTurn(5);

            // Assert
            Assert.AreEqual(1, sequenceNumberRangeTurn1.Min);
            Assert.AreEqual(2, sequenceNumberRangeTurn1.Max);
            Assert.AreEqual(0, sequenceNumberRangeTurn2.Min);
            Assert.AreEqual(0, sequenceNumberRangeTurn2.Max);
            Assert.AreEqual(3, sequenceNumberRangeTurn3.Min);
            Assert.AreEqual(3, sequenceNumberRangeTurn3.Max);
            Assert.AreEqual(4, sequenceNumberRangeTurn4.Min);
            Assert.AreEqual(6, sequenceNumberRangeTurn4.Max);
            Assert.AreEqual(0, sequenceNumberRangeTurn5.Min);
            Assert.AreEqual(0, sequenceNumberRangeTurn5.Max);
        }

        [TestMethod]
        public void GetTurnNumberFromSequenceNumber_Should_Return_TurnNumber_That_Was_Present_When_Sequence_Number_Was_Retrieved()
        {
            // Arrange
            _gameTurnService.NextSequenceNumber();
            _gameTurnService.NextSequenceNumber();
            _gameTurnService.NextTurn();
            _gameTurnService.NextTurn();
            _gameTurnService.NextSequenceNumber();
            _gameTurnService.NextTurn();
            _gameTurnService.NextSequenceNumber();
            _gameTurnService.NextSequenceNumber();
            _gameTurnService.NextSequenceNumber();
            _gameTurnService.NextTurn();

            // Act
            var turnNumber1 = _gameTurnService.GetTurnNumberFromSequenceNumber(1);
            var turnNumber2 = _gameTurnService.GetTurnNumberFromSequenceNumber(2);
            var turnNumber3 = _gameTurnService.GetTurnNumberFromSequenceNumber(3);
            var turnNumber4 = _gameTurnService.GetTurnNumberFromSequenceNumber(4);
            var turnNumber5 = _gameTurnService.GetTurnNumberFromSequenceNumber(5);
            var turnNumber6 = _gameTurnService.GetTurnNumberFromSequenceNumber(6);

            // Assert
            Assert.AreEqual(1, turnNumber1);
            Assert.AreEqual(1, turnNumber2);
            Assert.AreEqual(3, turnNumber3);
            Assert.AreEqual(4, turnNumber4);
            Assert.AreEqual(4, turnNumber5);
            Assert.AreEqual(4, turnNumber6);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(3)]
        public void GetSequenceNumberRangeForTurn_Should_Throw_Exception_For_Turn_Number_Not_Used(int turnNumber)
        {
            // Arrange
            _gameTurnService.NextTurn();

            // Act
            var exception = Assert.ThrowsException<ArgumentException>(
                () => _gameTurnService.GetSequenceNumberRangeForTurn(turnNumber)
            );

            // Assert
            Assert.AreEqual($"Turn number {turnNumber} has not yet been reached (Parameter 'turnNumber')", exception.Message);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(2)]
        public void GetTurnNumberFromSequenceNumber_Should_Throw_Exception_For_Sequence_Number_Not_Used(int sequenceNumber)
        {
            // Arrange
            _gameTurnService.NextSequenceNumber();

            // Act
            var exception = Assert.ThrowsException<ArgumentException>(
                () => _gameTurnService.GetTurnNumberFromSequenceNumber(sequenceNumber)
            );

            // Assert
            Assert.AreEqual($"Sequence number {sequenceNumber} has not yet been reached (Parameter 'sequenceNumber')", exception.Message);
        }

        [TestMethod]
        public void GetSequenceNumberRangeForTurn_On_First_Turn_Should_Return_Zero()
        {
            // Act
            var range = _gameTurnService.GetSequenceNumberRangeForTurn(1);

            // Assert
            Assert.AreEqual(0, range.Min);
            Assert.AreEqual(0, range.Max);
        }

        [TestMethod]
        public void GetTurnNumberFromSequenceNumber_On_First_Turn_Should_Throw_Exception()
        {
            // Arrange
            _gameTurnService.NextSequenceNumber();

            // Act
            var exception = Assert.ThrowsException<ArgumentException>(() => _gameTurnService.GetTurnNumberFromSequenceNumber(0));

            // Assert
            Assert.AreEqual($"Sequence number 0 has not yet been reached (Parameter 'sequenceNumber')", exception.Message);
        }
    }
}
