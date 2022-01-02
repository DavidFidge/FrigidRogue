using System;
using System.IO;
using System.Linq;
using AutoMapper;
using FrigidRogue.MonoGame.Core.Components;
using FrigidRogue.MonoGame.Core.Interfaces.Components;
using FrigidRogue.MonoGame.Core.Interfaces.Services;
using FrigidRogue.MonoGame.Core.Services;
using FrigidRogue.TestInfrastructure;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

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
            _saveGameStore.SaveToStore(testCommand.GetSaveState(Mapper));
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
            _saveGameStore.SaveToStore(testCommand.GetSaveState(Mapper));
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
            _saveGameStore.SaveToStore(testCommand.GetSaveState(Mapper));
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
            _saveGameStore.SaveToStore(testCommand.GetSaveState(Mapper));
            _saveGameStore.SaveStoreToFile(_saveGameName, false);

            // Act
            var result = _saveGameStore.SaveStoreToFile(_saveGameName, true);

            // Assert
            Assert.IsFalse(result.RequiresOverwrite);
            Assert.IsNull(result.ErrorMessage);
            Assert.AreEqual(SaveGameResult.Success, result);
        }

        [TestMethod]
        public void GetLoadGameList_Should_Populate_LoadGameDetails()
        {
            // Arrange
            var dateTimeNow = DateTime.Now;

            var testClass = new TestClass
            {
                LoadGameDetail = "Load Details"
            };

            var testData2 = new TestData2
            {
                LoadGameDetail = testClass.LoadGameDetail
            };

            _saveGameStore.SaveToStore(new Memento<TestData2> { State = testData2 });
            _saveGameStore.SaveStoreToFile(_saveGameName, false);

            // Act
            var loadGameList = _saveGameStore.GetLoadGameList();

            // Assert
            var gameToLoad = loadGameList.Single(l => l.Filename == _saveGameName);

            Assert.AreEqual(_saveGameName, gameToLoad.Filename);
            Assert.IsTrue(dateTimeNow <= gameToLoad.DateTime);
        }

        private class TestData
        {
            public int TestProperty { get; set; }
            public string TestProperty2 { get; set; }
        }

        private class TestData2 : ILoadGameDetail
        {
            public string LoadGameDetail { get; set; }
        }

        private class TestClass : ILoadGameDetail, ISaveable
        {
            public int TestProperty { get; set; }
            public string LoadGameDetail { get; set; }

            public void SaveState(ISaveGameStore saveGameStore)
            {
                throw new NotImplementedException();
            }

            public void LoadState(ISaveGameStore saveGameStore)
            {
                throw new NotImplementedException();
            }
        }

        private class TestCommand : BaseGameActionCommand<TestData>
        {
            public int TestProperty { get; set; }
            public string TestProperty2 { get; set; }

            public override IMemento<TestData> GetSaveState(IMapper mapper)
            {
                return new Memento<TestData>(new TestData { TestProperty = TestProperty, TestProperty2 = TestProperty2 });
            }

            protected override CommandResult ExecuteInternal()
            {
                throw new NotImplementedException();
            }

            protected override void UndoInternal()
            {
                throw new NotImplementedException();
            }

            public override void SetLoadState(IMemento<TestData> memento, IMapper mapper)
            {
                base.SetLoadState(memento, mapper);
                TestProperty = memento.State.TestProperty;
                TestProperty2 = memento.State.TestProperty2;
            }
        }
    }
}
