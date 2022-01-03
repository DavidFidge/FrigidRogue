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

namespace FrigidRogue.MonoGame.Core.Tests.Services
{
    [TestClass]
    public class SaveGameServiceTests : BaseTest
    {
        private SaveGameService _saveGameService;
        private string _saveGameName;
        private SaveGameFileWriter _saveGameWriter;

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();

            _saveGameName = $"TestSave{DateTime.Now:yyyy-M-d.HH.m.s.fffff}";

            _saveGameWriter = SetupBaseComponent(new SaveGameFileWriter());
            _saveGameService = SetupBaseComponent(new SaveGameService(_saveGameWriter));
        }

        [TestCleanup]
        public override void TearDown()
        {
            var saveGameFolder = _saveGameWriter.GetSaveGamePath();
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
            _saveGameService.SaveToStore(testCommand.GetSaveState(Mapper));
            var result = _saveGameService.SaveStoreToFile(_saveGameName, false);
            _saveGameService.LoadStoreFromFile(_saveGameName);

            var loadedTestData = _saveGameService.GetFromStore<TestData>();

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
            _saveGameService.SaveToStore(testCommand.GetSaveState(Mapper));
            _saveGameService.SaveStoreToFile(_saveGameName, false);

            // Act
            var result = _saveGameService.SaveStoreToFile(_saveGameName, false);

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
            _saveGameService.SaveToStore(testCommand.GetSaveState(Mapper));
            _saveGameService.SaveStoreToFile(_saveGameName, true);

            // Act
            var result = _saveGameService.CanSaveStoreToFile(_saveGameName);

            // Assert
            Assert.IsTrue(result.RequiresOverwrite);
            Assert.AreEqual(SaveGameResult.Overwrite, result);
        }

        [TestMethod]
        public void CanSaveStoreToFile_Should_Return_Success_Result_If_File_Does_Not_Exist()
        {
            // Act
            var result = _saveGameService.CanSaveStoreToFile(_saveGameName);

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
            _saveGameService.SaveToStore(testCommand.GetSaveState(Mapper));
            _saveGameService.SaveStoreToFile(_saveGameName, false);

            // Act
            var result = _saveGameService.SaveStoreToFile(_saveGameName, true);

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

            _saveGameService.SaveToStore(new Memento<TestData2> { State = testData2 });
            _saveGameService.SaveStoreToFile(_saveGameName, true);

            // Act
            var loadGameList = _saveGameService.GetLoadGameList();

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

            public void SaveState(ISaveGameService saveGameService)
            {
                throw new NotImplementedException();
            }

            public void LoadState(ISaveGameService saveGameService)
            {
                throw new NotImplementedException();
            }
        }

        private class TestCommand : BaseStatefulGameActionCommand<TestData>
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
