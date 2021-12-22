using System;
using System.Collections.Generic;
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
            // Arrange
            var testData = new GameOptionsStoreTestData();

            var gameOptionsFolder = GetGameOptionsFilePath<GameOptionsStoreTestData>();

            var nonExistantFolder = Path.Combine(gameOptionsFolder, "NonExistantFolder");

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