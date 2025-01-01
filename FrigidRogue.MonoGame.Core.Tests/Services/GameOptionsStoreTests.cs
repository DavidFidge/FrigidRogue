using System.IO;
using System.Reflection;
using FrigidRogue.MonoGame.Core.Services;
using FrigidRogue.TestInfrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FrigidRogue.MonoGame.Core.Tests.Services
{
    [TestClass]
    public class GameOptionsStoreTests : BaseTest
    {
        private GameOptionsStore _gameOptionsStore;

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();

            _gameOptionsStore = new GameOptionsStore();
        }

        [TestCleanup]
        public override void TearDown()
        {
            var optionsFile = GetGameOptionsFilePath<GameOptionsStoreTestData>();

            File.Delete(optionsFile);
        }

        [TestMethod]
        public void Should_Save_To_Correct_File()
        {
            // Arrange
            var testData = new GameOptionsStoreTestData();

            var optionsFile = GetGameOptionsFilePath<GameOptionsStoreTestData>();

            // Act
            _gameOptionsStore.SaveToStore(new Memento<GameOptionsStoreTestData>(testData));

            // Assert
            Assert.IsTrue(File.Exists(optionsFile));
        }

        [TestMethod]
        public void Should_Return_Null_If_No_File_Found()
        {
            // Act
            var memento = _gameOptionsStore.GetFromStore<GameOptionsStoreTestData>();

            // Assert
            Assert.IsNull(memento);
        }

        [TestMethod]
        public void Should_Save_And_Load_Successfully()
        {
            // Arrange
            var testData = new GameOptionsStoreTestData();
            testData.ListProperty[1].IntProperty = 2;

            // Act
            _gameOptionsStore.SaveToStore(new Memento<GameOptionsStoreTestData>(testData));

            var loadedData = _gameOptionsStore.GetFromStore<GameOptionsStoreTestData>().State;

            // Assert
            Assert.AreEqual("Test", loadedData.StringProperty);
            Assert.AreEqual(2, loadedData.ListProperty.Count);
            Assert.AreEqual(1, loadedData.ListProperty[0].IntProperty);
            Assert.AreEqual(2, loadedData.ListProperty[1].IntProperty);
        }

        [TestMethod]
        public void Should_Load_Using_Cache()
        {
            // Arrange
            var testData = new GameOptionsStoreTestData();
            testData.ListProperty[1].IntProperty = 2;

            _gameOptionsStore.SaveToStore(new Memento<GameOptionsStoreTestData>(testData));
            _gameOptionsStore.GetFromStore<GameOptionsStoreTestData>(); // Load from file, next call will load from cache

            // Act
            var optionsFile = GetGameOptionsFilePath<GameOptionsStoreTestData>();

            // Lock the file, ensuring it cannot be opened elsewhere
            using var file = File.Open(optionsFile, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            var loadedData = _gameOptionsStore.GetFromStore<GameOptionsStoreTestData>().State;
            file.Close();

            // Assert
            Assert.AreEqual("Test", loadedData.StringProperty);
            Assert.AreEqual(2, loadedData.ListProperty.Count);
            Assert.AreEqual(1, loadedData.ListProperty[0].IntProperty);
            Assert.AreEqual(2, loadedData.ListProperty[1].IntProperty);
        }

        [TestMethod]
        public void Second_Save_Should_Overwrite_Existing_Data()
        {
            // Arrange
            var testData = new GameOptionsStoreTestData();
            testData.ListProperty[1].IntProperty = 2;

            var testData2 = new GameOptionsStoreTestData();
            testData2.ListProperty[1].IntProperty = 3;

            _gameOptionsStore.SaveToStore(new Memento<GameOptionsStoreTestData>(testData));
            _gameOptionsStore.SaveToStore(new Memento<GameOptionsStoreTestData>(testData2));

            // Act
            var loadedData = _gameOptionsStore.GetFromStore<GameOptionsStoreTestData>().State;

            // Assert
            Assert.AreEqual("Test", loadedData.StringProperty);
            Assert.AreEqual(2, loadedData.ListProperty.Count);
            Assert.AreEqual(1, loadedData.ListProperty[0].IntProperty);
            Assert.AreEqual(3, loadedData.ListProperty[1].IntProperty);
        }

        [TestMethod]
        public void Second_Save_Should_Not_Update_Cache_With_Reference_Of_Object_Being_Saved()
        {
            // Arrange
            var testData = new GameOptionsStoreTestData();
            testData.ListProperty[1].IntProperty = 2;

            var testData2 = new GameOptionsStoreTestData();
            testData2.ListProperty[1].IntProperty = 3;

            _gameOptionsStore.SaveToStore(new Memento<GameOptionsStoreTestData>(testData));
            _gameOptionsStore.GetFromStore<GameOptionsStoreTestData>(); // Load from file, next call will load from cache
            _gameOptionsStore.SaveToStore(new Memento<GameOptionsStoreTestData>(testData2));

            // Act
            testData2.ListProperty[1].IntProperty = 4; // Update object which was saved.  Loading from cache should not return this updated value
            var loadedData = _gameOptionsStore.GetFromStore<GameOptionsStoreTestData>().State;

            // Assert
            Assert.AreEqual("Test", loadedData.StringProperty);
            Assert.AreEqual(2, loadedData.ListProperty.Count);
            Assert.AreEqual(1, loadedData.ListProperty[0].IntProperty);
            Assert.AreEqual(3, loadedData.ListProperty[1].IntProperty);
        }

        private string GetGameOptionsFilePath<T>()
        {
            var localFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var gameFolder = Path.Combine(localFolderPath, Assembly.GetEntryAssembly().GetName().Name);

            if (!Directory.Exists(gameFolder))
                Directory.CreateDirectory(gameFolder);

            var optionsFolder = Path.Combine(gameFolder, "Options");

            if (!Directory.Exists(optionsFolder))
                Directory.CreateDirectory(optionsFolder);

            var optionsFile = Path.Combine(optionsFolder, $"{typeof(T).Name}.txt");

            return optionsFile;
        }

        public class GameOptionsStoreTestData
        {
            public string StringProperty { get; set; } = "Test";

            public IList<ChildGameOptionsStoreTestData> ListProperty { get; set; }

            public GameOptionsStoreTestData()
            {
                ListProperty = new List<ChildGameOptionsStoreTestData>
                {
                    new ChildGameOptionsStoreTestData
                    {
                        IntProperty = 1
                    },
                    new ChildGameOptionsStoreTestData
                    {
                        IntProperty = 2
                    }
                };
            }
        }

        public class ChildGameOptionsStoreTestData
        {
            public int IntProperty { get; set; } = 1;
        }
    }
}