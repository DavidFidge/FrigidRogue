using FrigidRogue.MonoGame.Core.Interfaces.UserInterface;
using FrigidRogue.MonoGame.Core.UserInterface;
using FrigidRogue.TestInfrastructure;
using InputHandlers.Keyboard;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework.Input;
using NSubstitute;

namespace FrigidRogue.MonoGame.Core.Tests.UserInterface
{
    [TestClass]
    public class ActionMapTests : BaseTest
    {
        private IActionMapStore _actionMapStore;
        private ActionMap _actionMap;

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();

            _actionMapStore = Substitute.For<IActionMapStore>();
            _actionMap = new ActionMap(_actionMapStore);
        }

        [TestMethod]
        public void ActionIs_Should_Return_False_If_Action_Store_Does_Not_Contain_Action()
        {
            // Arrange
            var keyCombinations = new Dictionary<string, KeyCombination>
            {
                {
                    "TestMapOther", new KeyCombination(Keys.A)
                }
            };

            _actionMapStore.GetKeyMap().Returns(keyCombinations);

            // Act
            var result = _actionMap.ActionIs<TestAction>(new KeyCombination(Keys.A));

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ActionIs_Should_Throw_Exception_If_Action_Is_Not_An_Attribute_On_Generic_Class()
        {
            // Arrange
            var keyCombinations = new Dictionary<string, KeyCombination>();

            _actionMapStore.GetKeyMap().Returns(keyCombinations);

            // Act
            var result = Assert.ThrowsException<Exception>(() => _actionMap.ActionIs<TestNoActionMapAttribute>(new KeyCombination(Keys.A)));

            // Assert
            Assert.AreEqual("No ActionMapAttribute found on class TestNoActionMapAttribute", result.Message);
        }

        [TestMethod]
        public void ActionIs_Should_Return_True_If_Key_And_Modifier_Match_Store()
        {
            // Arrange
            var keyCombinations = new Dictionary<string, KeyCombination>
            {
                {
                    "TestMap1", new KeyCombination(Keys.A, KeyboardModifier.Alt)
                }
            };

            _actionMapStore.GetKeyMap().Returns(keyCombinations);

            // Act
            var result = _actionMap.ActionIs<TestAction>(new KeyCombination(Keys.A, KeyboardModifier.Alt));

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ActionIs_Should_Return_False_If_Key_Does_Not_Match_Store()
        {
            // Arrange
            var keyCombinations = new Dictionary<string, KeyCombination>
            {
                {
                    "TestMap1", new KeyCombination(Keys.A)
                }
            };

            _actionMapStore.GetKeyMap().Returns(keyCombinations);

            // Act
            var result = _actionMap.ActionIs<TestAction>(new KeyCombination(Keys.B));

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow(KeyboardModifier.Ctrl)]
        [DataRow(KeyboardModifier.Alt | KeyboardModifier.Ctrl)]
        [DataRow(KeyboardModifier.None)]
        public void ActionIs_Should_Return_False_If_Modifier_Does_Not_Match_Store(KeyboardModifier keyboardModifier)
        {
            // Arrange
            var keyCombinations = new Dictionary<string, KeyCombination>
            {
                {
                    "TestMap1", new KeyCombination(Keys.A, KeyboardModifier.Alt)
                }
            };

            _actionMapStore.GetKeyMap().Returns(keyCombinations);

            // Act
            var result = _actionMap.ActionIs<TestAction>(new KeyCombination(Keys.A, keyboardModifier));

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ActionIs_Should_Return_True_When_Selector_Is_Used()
        {
            // Arrange
            var keyCombinations = new Dictionary<string, KeyCombination>
            {
                {
                    "TestMap1", new KeyCombination(Keys.A, KeyboardModifier.Alt)
                },
                {
                    "TestMap2", new KeyCombination(Keys.B)
                }
            };

            _actionMapStore.GetKeyMap().Returns(keyCombinations);

            // Act
            var result = _actionMap.ActionIs<TestActionMultiple>(new KeyCombination(Keys.A, KeyboardModifier.Alt), "TestMap1");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ActionIs_Should_Return_True_When_Second_Selector_Is_Used()
        {
            // Arrange
            var keyCombinations = new Dictionary<string, KeyCombination>
            {
                {
                    "TestMap1", new KeyCombination(Keys.A, KeyboardModifier.Alt)
                },
                {
                    "TestMap2", new KeyCombination(Keys.B)
                }
            };

            _actionMapStore.GetKeyMap().Returns(keyCombinations);

            // Act
            var result = _actionMap.ActionIs<TestActionMultiple>(new KeyCombination(Keys.B), "TestMap2");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ActionName_Should_Return_Action_Name_Of_Key_Pressed()
        {
            // Arrange
            var keyCombinations = new Dictionary<string, KeyCombination>
            {
                {
                    "TestMap1", new KeyCombination(Keys.A, KeyboardModifier.Alt)
                },
                {
                    "TestMap2", new KeyCombination(Keys.B)
                }
            };

            _actionMapStore.GetKeyMap().Returns(keyCombinations);

            // Act
            var result = _actionMap.ActionName<TestActionMultiple>(Keys.B, KeyboardModifier.None);

            // Assert
            Assert.AreEqual("TestMap2", result);
        }

        [TestMethod]
        public void ActionIs_Should_Return_False_When_Selector_Is_Used_And_Key_Does_Not_Match()
        {
            // Arrange
            var keyCombinations = new Dictionary<string, KeyCombination>
            {
                {
                    "TestMap1", new KeyCombination(Keys.A)
                },
                {
                    "TestMap2", new KeyCombination(Keys.B)
                }
            };

            _actionMapStore.GetKeyMap().Returns(keyCombinations);

            // Act
            var result = _actionMap.ActionIs<TestActionMultiple>(new KeyCombination(Keys.A), "TestMap2");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ActionIs_Should_Throw_Exception_If_Action_Is_Not_An_Attribute_On_Generic_Class_When_Using_Selector()
        {
            // Arrange
            var keyCombinations = new Dictionary<string, KeyCombination>();

            _actionMapStore.GetKeyMap().Returns(keyCombinations);

            // Act
            var result = Assert.ThrowsException<Exception>(() => _actionMap.ActionIs<TestActionMultiple>(new KeyCombination(Keys.A), "DoesNotExist"));

            // Assert
            Assert.AreEqual("No ActionMapAttribute with name DoesNotExist found on class TestActionMultiple", result.Message);
        }

        [ActionMap(Name = "TestMap1")]
        public class TestAction
        {
        }

        [ActionMap(Name = "TestMap1")]
        [ActionMap(Name = "TestMap2")]
        public class TestActionMultiple
        {
        }

        public class TestNoActionMapAttribute
        {
        }
    }
}
