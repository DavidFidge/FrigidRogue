using System.IO;
using FrigidRogue.MonoGame.Core.Interfaces.Components;
using FrigidRogue.MonoGame.Core.Services;
using FrigidRogue.TestInfrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonoGame.Extended;

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

            _saveGameName = $"TestSave{DateTime.Now.Ticks}";

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
        public void Should_Save_To_And_Load_From_Store_No_Header()
        {
            // Arrange
            var testClass = new TestClass2();
            testClass.TestProperty = 1;
            testClass.TestProperty2 = "hello";

            // Act
            _saveGameService.SaveToStore(testClass.GetSaveState());
            var result = _saveGameService.SaveStoreToFile(_saveGameName, false);
            _saveGameService.LoadStoreFromFile(_saveGameName);

            var loadedTestData = _saveGameService.GetFromStore<TestData2>();

            // Assert
            Assert.AreEqual(testClass.TestProperty, loadedTestData.State.TestProperty);
            Assert.AreEqual(testClass.TestProperty2, loadedTestData.State.TestProperty2);
            Assert.IsFalse(result.RequiresOverwrite);
            Assert.IsNull(result.ErrorMessage);
            Assert.AreEqual(SaveGameResult.Success, result);
        }

        [TestMethod]
        public void Should_Save_To_And_Load_From_Store_With_Header()
        {
            // Arrange
            var testClass = new TestClass2();
            testClass.TestProperty = 1;
            testClass.TestProperty2 = "hello";

            var testHeaderClass = new TestClass1();
            testHeaderClass.LoadGameDetail = "header";

            // Act
            _saveGameService.SaveToStore(testClass.GetSaveState());
            _saveGameService.SaveHeaderToStore(testHeaderClass.GetSaveState());
            var result = _saveGameService.SaveStoreToFile(_saveGameName, false);
            _saveGameService.LoadStoreFromFile(_saveGameName);

            var loadedTestData = _saveGameService.GetFromStore<TestData2>();
            var loadedTestHeaderData = _saveGameService.GetHeaderFromStore<TestData1>();

            // Assert
            Assert.AreEqual(testClass.TestProperty, loadedTestData.State.TestProperty);
            Assert.AreEqual(testClass.TestProperty2, loadedTestData.State.TestProperty2);
            Assert.AreEqual(testHeaderClass.LoadGameDetail, loadedTestHeaderData.State.LoadGameDetail);
            Assert.IsFalse(result.RequiresOverwrite);
            Assert.IsNull(result.ErrorMessage);
            Assert.AreEqual(SaveGameResult.Success, result);
        }

        [TestMethod]
        public void Clear_Should_Empty_Game_Store_And_Header()
        {
            // Arrange
            var testClass = new TestClass2();
            testClass.TestProperty = 1;
            testClass.TestProperty2 = "hello";

            var testHeaderClass = new TestClass1();
            testHeaderClass.LoadGameDetail = "header";

            _saveGameService.SaveToStore(testClass.GetSaveState());
            _saveGameService.SaveHeaderToStore(testHeaderClass.GetSaveState());

            // Act
            _saveGameService.Clear();

            // Assert
            var exception = Assert.ThrowsException<Exception>(() => _saveGameService.GetFromStore<TestData2>());
            Assert.AreEqual($"An object was not found in the store which is or can be assigned as a type {typeof(TestData2)}", exception.Message);

            var exceptionHeader = Assert.ThrowsException<Exception>(() => _saveGameService.GetHeaderFromStore<TestData1>());
            Assert.AreEqual($"An object was not found in the store which is or can be assigned as a type {typeof(TestData1)}", exceptionHeader.Message);
        }

        [TestMethod]
        public void Ranges_Should_Save_To_And_Load_From_Store()
        {
            // Arrange
            var testClass3 = new TestClass3();
            testClass3.TestRange = new Range<int>(5, 10);

            // Act
            _saveGameService.SaveToStore(testClass3.GetSaveState());
            var result = _saveGameService.SaveStoreToFile(_saveGameName, false);
            _saveGameService.LoadStoreFromFile(_saveGameName);

            var loadedTestData = _saveGameService.GetFromStore<TestData3>();

            // Assert
            Assert.AreEqual(testClass3.TestRange, loadedTestData.State.TestRange);
            Assert.AreEqual(SaveGameResult.Success, result);
        }

        [TestMethod]
        public void SaveStoreToFile_Should_Return_Overwrite_Result_If_File_Exists()
        {
            // Arrange
            var testClass = new TestClass2();
            testClass.TestProperty = 1;
            testClass.TestProperty2 = "hello";
            _saveGameService.SaveToStore(testClass.GetSaveState());
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
            var testClass = new TestClass2();
            testClass.TestProperty = 1;
            testClass.TestProperty2 = "hello";
            _saveGameService.SaveToStore(testClass.GetSaveState());
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
            var testClass = new TestClass2();
            testClass.TestProperty = 1;
            testClass.TestProperty2 = "hello";
            _saveGameService.SaveToStore(testClass.GetSaveState());
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

            var testClass = new TestClass1
            {
                LoadGameDetail = $"Load Details{dateTimeNow.Ticks}"
            };

            _saveGameService.SaveHeaderToStore(testClass.GetSaveState());
            _saveGameService.SaveStoreToFile(_saveGameName, true);

            // Act
            var loadGameList = _saveGameService.GetLoadGameList();

            // Assert
            var gameToLoad = loadGameList.Single(l => l.Filename == _saveGameName);

            Assert.AreEqual(_saveGameName, gameToLoad.Filename);
            Assert.AreEqual(testClass.LoadGameDetail, gameToLoad.LoadGameDetail);

            // The load time should be in the vicinity of current date time. We can't do a
            // comparison by dateTimeNow <= gameToLoad as sometimes gameToLoad can be LESS THAN
            // dateTimeNow. Refer to https://docs.microsoft.com/en-us/dotnet/api/system.io.filesysteminfo.lastwritetime?redirectedfrom=MSDN&view=net-6.0#System_IO_FileSystemInfo_LastWriteTime
            // Use of LastWriteTime - "This method may return an inaccurate value because it uses native
            // functions whose values may not be continuously updated by the operating system."
            // The following assert has been commented out for now - it still fails intermittently on the github build server
            // Assert.IsTrue(Math.Abs(dateTimeNow.Ticks - gameToLoad.DateTime.Ticks) < 100000);
        }

        private class TestData1 : IHeaderSaveData
        {
            public string LoadGameDetail { get; set; }
        }

        private class TestClass1 : IHeaderSaveData, IMementoState<TestData1>
        {
            public string LoadGameDetail { get; set; }
            public IMemento<TestData1> GetSaveState()
            {
                return new Memento<TestData1>(new TestData1 { LoadGameDetail = LoadGameDetail });
            }

            public void SetLoadState(IMemento<TestData1> memento)
            {
                LoadGameDetail = memento.State.LoadGameDetail;
            }
        }

        private class TestData2
        {
            public int TestProperty { get; set; }
            public string TestProperty2 { get; set; }
        }

        private class TestClass2 : IMementoState<TestData2>
        {
            public int TestProperty { get; set; }
            public string TestProperty2 { get; set; }

            public IMemento<TestData2> GetSaveState()
            {
                return new Memento<TestData2>(new TestData2 { TestProperty = TestProperty, TestProperty2 = TestProperty2 });
            }

            public void SetLoadState(IMemento<TestData2> memento)
            {
                TestProperty = memento.State.TestProperty;
                TestProperty2 = memento.State.TestProperty2;
            }
        }

        private class TestData3
        {
            public Range<int> TestRange { get; set; }
        }

        private class TestClass3 : IMementoState<TestData3>
        {
            public Range<int> TestRange { get; set; }
            public IMemento<TestData3> GetSaveState()
            {
                return new Memento<TestData3>(new TestData3 { TestRange = TestRange });
            }

            public void SetLoadState(IMemento<TestData3> memento)
            {
                TestRange = memento.State.TestRange;
            }
        }
    }
}
