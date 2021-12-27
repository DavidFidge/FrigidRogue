using System;
using System.IO;

using FrigidRogue.MonoGame.Core.Components;
using FrigidRogue.MonoGame.Core.Interfaces.Components;
using FrigidRogue.MonoGame.Core.Services;
using FrigidRogue.TestInfrastructure;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FrigidRogue.MonoGame.Core.Tests.Services
{
    [TestClass]
    public class SaveGameStoreTests : BaseTest
    {
        private SaveGameStore _saveGameStore;
        private string _saveGameName;

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();

            _saveGameName = $"TestSave{DateTime.Now:yyyy-M-d.HH.m.s.fffff}";
            _saveGameStore = SetupBaseComponent(new SaveGameStore());
        }

        [TestCleanup]
        public override void TearDown()
        {
            var saveGameFolder = _saveGameStore.GetSaveGamePath();
            var saveGameFile = Path.Combine(saveGameFolder, $"{_saveGameName}.sav");

            File.Delete(saveGameFile);
        }

        [TestMethod]
        public void Should_Save_To_And_Load_From_Store()
        {
            // Arrange
            var testCommand = new TestCommand();
            testCommand.TestProperty = 1;
            testCommand.TestProperty2 = "hello";


            // Act
            _saveGameStore.SaveToStore(testCommand.GetState());
            var result = _saveGameStore.SaveStoreToFile(_saveGameName, false);
            _saveGameStore.LoadStoreFromFile(_saveGameName);

            var loadedTestData = _saveGameStore.GetFromStore<TestData>();

            // Assert
            Assert.AreEqual(testCommand.TestProperty, loadedTestData.State.TestProperty);
            Assert.AreEqual(testCommand.TestProperty2, loadedTestData.State.TestProperty2);
            Assert.IsFalse(result.RequiresOverwrite);
            Assert.IsNull(result.ErrorMessage);
            Assert.AreEqual(SaveGameResult.Success, result);
        }

        [TestMethod]
        public void SaveStoreToFile_Should_Return_Overwrite_Result_If_File_Exists()
        {
            // Arrange
            var testCommand = new TestCommand();
            testCommand.TestProperty = 1;
            testCommand.TestProperty2 = "hello";
            _saveGameStore.SaveToStore(testCommand.GetState());
            _saveGameStore.SaveStoreToFile(_saveGameName, false);

            // Act
            var result = _saveGameStore.SaveStoreToFile(_saveGameName, false);

            // Assert
            Assert.IsTrue(result.RequiresOverwrite);
            Assert.AreEqual(SaveGameResult.Overwrite, result);
        }

        [TestMethod]
        public void CanSaveStoreToFile_Should_Return_Overwrite_Result_If_File_Exists()
        {
            // Arrange
            var testCommand = new TestCommand();
            testCommand.TestProperty = 1;
            testCommand.TestProperty2 = "hello";
            _saveGameStore.SaveToStore(testCommand.GetState());
            _saveGameStore.SaveStoreToFile(_saveGameName, false);

            // Act
            var result = _saveGameStore.CanSaveStoreToFile(_saveGameName);

            // Assert
            Assert.IsTrue(result.RequiresOverwrite);
            Assert.AreEqual(SaveGameResult.Overwrite, result);
        }

        [TestMethod]
        public void CanSaveStoreToFile_Should_Return_Success_Result_If_File_Does_Not_Exist()
        {
            // Act
            var result = _saveGameStore.CanSaveStoreToFile(_saveGameName);

            // Assert
            Assert.IsFalse(result.RequiresOverwrite);
            Assert.AreEqual(SaveGameResult.Success, result);
        }

        [TestMethod]
        public void SaveStoreToFile_Should_Save_If_Overwrite_Is_True()
        {
            // Arrange
            var testCommand = new TestCommand();
            testCommand.TestProperty = 1;
            testCommand.TestProperty2 = "hello";
            _saveGameStore.SaveToStore(testCommand.GetState());
            _saveGameStore.SaveStoreToFile(_saveGameName, false);

            // Act
            var result = _saveGameStore.SaveStoreToFile(_saveGameName, true);

            // Assert
            Assert.IsFalse(result.RequiresOverwrite);
            Assert.IsNull(result.ErrorMessage);
            Assert.AreEqual(SaveGameResult.Success, result);
        }

        private class TestData
        {
            public int TestProperty { get; set; }
            public string TestProperty2 { get; set; }
        }

        private class TestCommand : BaseGameActionCommand<TestData>
        {
            public int TestProperty { get; set; }
            public string TestProperty2 { get; set; }

            public override IMemento<TestData> GetState()
            {
                return new Memento<TestData>(new TestData { TestProperty = TestProperty, TestProperty2 = TestProperty2});
            }

            public override void SetState(IMemento<TestData> state)
            {
                TestProperty = state.State.TestProperty;
                TestProperty2 = state.State.TestProperty2;
            }

            public override void Execute()
            {
                throw new NotImplementedException();
            }

            public override void Undo()
            {
                throw new NotImplementedException();
            }
        }
    }
}
