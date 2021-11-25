using System.Linq;

using FrigidRogue.MonoGame.Core.Services;
using FrigidRogue.TestInfrastructure;

using InputHandlers.Keyboard;
using InputHandlers.Mouse;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework.Input;

using NSubstitute;

namespace FrigidRogue.MonoGame.Core.Tests.Services
{
    [TestClass]
    public class GameInputServiceTests : BaseTest
    {
        private IMouseInput _mouseInput;
        private IKeyboardInput _keyboardInput;
        private GameInputService _gameInputService;

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();

            _mouseInput = Substitute.For<IMouseInput>();
            _keyboardInput = Substitute.For<IKeyboardInput>();

            _gameInputService = new GameInputService(_mouseInput, _keyboardInput);
        }

        [TestMethod]
        public void ChangeInput_Should_Subscribe_Mouse_And_Keyboard_Handlers()
        {
            // Arrange
            var mouseHandler = Substitute.For<IMouseHandler>();
            var keyboardHandler = Substitute.For<IKeyboardHandler>();

            // Act
            _gameInputService.ChangeInput(mouseHandler, keyboardHandler);
            
            // Assert
            _mouseInput.Received().Subscribe(Arg.Is(mouseHandler));
            _keyboardInput.Received().Subscribe(Arg.Is(keyboardHandler));
        }

        [TestMethod]
        public void AddToCurrentGroup_Should_Subscribe_Mouse_And_Keyboard_Handlers_And_Add_To_Current_Group()
        {
            // Arrange
            var mouseHandlerRoot = Substitute.For<IMouseHandler>();
            var keyboardHandlerRoot = Substitute.For<IKeyboardHandler>();

            _gameInputService.ChangeInput(mouseHandlerRoot, keyboardHandlerRoot);

            _mouseInput.ClearReceivedCalls();
            _keyboardInput.ClearReceivedCalls();

            var mouseHandlerAppend = Substitute.For<IMouseHandler>();
            var keyboardHandlerAppend = Substitute.For<IKeyboardHandler>();
            
            // Act
            _gameInputService.AddToCurrentGroup(mouseHandlerAppend, keyboardHandlerAppend);

            // Assert
            _mouseInput.Received().Subscribe(Arg.Is(mouseHandlerAppend));
            _keyboardInput.Received().Subscribe(Arg.Is(keyboardHandlerAppend));
        }

        [TestMethod]
        public void AddToCurrentGroup_Then_Add_And_Remove_Should_Resubscribe_Group()
        {
            // Arrange
            var mouseHandlerRoot = Substitute.For<IMouseHandler>();
            var keyboardHandlerRoot = Substitute.For<IKeyboardHandler>();

            _gameInputService.ChangeInput(mouseHandlerRoot, keyboardHandlerRoot);

            var mouseHandlerAppend = Substitute.For<IMouseHandler>();
            var keyboardHandlerAppend = Substitute.For<IKeyboardHandler>();

            _gameInputService.AddToCurrentGroup(mouseHandlerAppend, keyboardHandlerAppend);

            var mouseHandlerNewRoot = Substitute.For<IMouseHandler>();
            var keyboardHandlerNewRoot = Substitute.For<IKeyboardHandler>();

            _mouseInput.ClearReceivedCalls();
            _keyboardInput.ClearReceivedCalls();

            // Act and Assert
            _gameInputService.ChangeInput(mouseHandlerNewRoot, keyboardHandlerNewRoot);

            _mouseInput.Received().Unsubscribe(Arg.Is(mouseHandlerRoot));
            _keyboardInput.Received().Unsubscribe(Arg.Is(keyboardHandlerRoot));

            _mouseInput.Received().Unsubscribe(Arg.Is(mouseHandlerAppend));
            _keyboardInput.Received().Unsubscribe(Arg.Is(keyboardHandlerAppend));

            _mouseInput.Received().Subscribe(Arg.Is(mouseHandlerNewRoot));
            _keyboardInput.Received().Subscribe(Arg.Is(keyboardHandlerNewRoot));

            _mouseInput.ClearReceivedCalls();
            _keyboardInput.ClearReceivedCalls();

            _gameInputService.RevertInputUpToAndIncluding(mouseHandlerNewRoot, keyboardHandlerNewRoot);

            _mouseInput.Received().Unsubscribe(Arg.Is(mouseHandlerNewRoot));
            _keyboardInput.Received().Unsubscribe(Arg.Is(keyboardHandlerNewRoot));

            _mouseInput.Received().Subscribe(Arg.Is(mouseHandlerRoot));
            _keyboardInput.Received().Subscribe(Arg.Is(keyboardHandlerRoot));

            _mouseInput.Received().Subscribe(Arg.Is(mouseHandlerAppend));
            _keyboardInput.Received().Subscribe(Arg.Is(keyboardHandlerAppend));
        }

        [TestMethod]
        public void AddToCurrentGroup_Then_Add_Then_Remove_Original_Group_Should_Revert_Every_Handler()
        {
            // Arrange
            var mouseHandlerRoot = Substitute.For<IMouseHandler>();
            var keyboardHandlerRoot = Substitute.For<IKeyboardHandler>();

            _gameInputService.ChangeInput(mouseHandlerRoot, keyboardHandlerRoot);

            var mouseHandlerAppend = Substitute.For<IMouseHandler>();
            var keyboardHandlerAppend = Substitute.For<IKeyboardHandler>();

            _gameInputService.AddToCurrentGroup(mouseHandlerAppend, keyboardHandlerAppend);

            var mouseHandlerNewRoot = Substitute.For<IMouseHandler>();
            var keyboardHandlerNewRoot = Substitute.For<IKeyboardHandler>();

            _gameInputService.ChangeInput(mouseHandlerNewRoot, keyboardHandlerNewRoot);

            _mouseInput.ClearReceivedCalls();
            _keyboardInput.ClearReceivedCalls();

            // Act
            _gameInputService.RevertInputUpToAndIncluding(mouseHandlerRoot, keyboardHandlerRoot);

            // Assert
            _mouseInput.Received().Unsubscribe(Arg.Is(mouseHandlerRoot));
            _keyboardInput.Received().Unsubscribe(Arg.Is(keyboardHandlerRoot));

            _mouseInput.Received().Unsubscribe(Arg.Is(mouseHandlerAppend));
            _keyboardInput.Received().Unsubscribe(Arg.Is(keyboardHandlerAppend));

            _mouseInput.Received().Unsubscribe(Arg.Is(mouseHandlerNewRoot));
            _keyboardInput.Received().Unsubscribe(Arg.Is(keyboardHandlerNewRoot));
        }

        [TestMethod]
        public void RemoveFromCurrentGroup_Should_Unsubscribe_Mouse_And_Keyboard_Handlers_From_Current_Group()
        {
            // Arrange
            var mouseHandlerRoot = Substitute.For<IMouseHandler>();
            var keyboardHandlerRoot = Substitute.For<IKeyboardHandler>();

            _gameInputService.ChangeInput(mouseHandlerRoot, keyboardHandlerRoot);

            var mouseHandlerAppend = Substitute.For<IMouseHandler>();
            var keyboardHandlerAppend = Substitute.For<IKeyboardHandler>();

            _gameInputService.AddToCurrentGroup(mouseHandlerAppend, keyboardHandlerAppend);

            _mouseInput.ClearReceivedCalls();
            _keyboardInput.ClearReceivedCalls();

            // Act
            _gameInputService.RemoveFromCurrentGroup(mouseHandlerAppend, keyboardHandlerAppend);

            // Assert
            _mouseInput.Received().Unsubscribe(Arg.Is(mouseHandlerAppend));
            _keyboardInput.Received().Unsubscribe(Arg.Is(keyboardHandlerAppend));
        }

        [TestMethod]
        public void ChangeInput_Should_Unsubscribe_Mouse_And_Keyboard_Handlers()
        {
            // Arrange
            var mouseHandlerSubscribed = Substitute.For<IMouseHandler>();
            var keyboardHandlerSubscribed = Substitute.For<IKeyboardHandler>();

            var mouseHandlerNew = Substitute.For<IMouseHandler>();
            var keyboardHandlerNew = Substitute.For<IKeyboardHandler>();

            _gameInputService.ChangeInput(mouseHandlerSubscribed, keyboardHandlerSubscribed);

            _mouseInput.ClearReceivedCalls();
            _keyboardInput.ClearReceivedCalls();

            // Act
            _gameInputService.ChangeInput(mouseHandlerNew, keyboardHandlerNew);

            // Assert
            _mouseInput.Received().Subscribe(Arg.Is(mouseHandlerNew));
            _keyboardInput.Received().Subscribe(Arg.Is(keyboardHandlerNew));

            _mouseInput.Received().Unsubscribe(Arg.Is(mouseHandlerSubscribed));
            _keyboardInput.Received().Unsubscribe(Arg.Is(keyboardHandlerSubscribed));
        }

        [TestMethod]
        public void RevertInput_Should_Subscribe_Previous_Mouse_And_Keyboard_Handlers()
        {
            // Arrange
            var mouseHandlerToRevertTo = Substitute.For<IMouseHandler>();
            var keyboardHandlerToRevertTo = Substitute.For<IKeyboardHandler>();

            var mouseHandlerNew = Substitute.For<IMouseHandler>();
            var keyboardHandlerNew = Substitute.For<IKeyboardHandler>();

            _gameInputService.ChangeInput(mouseHandlerToRevertTo, keyboardHandlerToRevertTo);
            _gameInputService.ChangeInput(mouseHandlerNew, keyboardHandlerNew);

            _mouseInput.ClearReceivedCalls();
            _keyboardInput.ClearReceivedCalls();

            // Act
            _gameInputService.RevertInputUpToAndIncluding(mouseHandlerNew, keyboardHandlerNew);

            // Assert
            _mouseInput.Received().Subscribe(Arg.Is(mouseHandlerToRevertTo));
            _keyboardInput.Received().Subscribe(Arg.Is(keyboardHandlerToRevertTo));

            _mouseInput.Received().Unsubscribe(Arg.Is(mouseHandlerNew));
            _keyboardInput.Received().Unsubscribe(Arg.Is(keyboardHandlerNew));
        }

        [TestMethod]
        public void RevertInput_Should_Do_Nothing_If_No_Subscriptions()
        {
            // Act
            _gameInputService.RevertInputUpToAndIncluding(Substitute.For<IMouseHandler>(), Substitute.For<IKeyboardHandler>());

            // Assert
            Assert.AreEqual(0, _mouseInput.ReceivedCalls().Count());
            Assert.AreEqual(0, _keyboardInput.ReceivedCalls().Count());
        }
        
        [TestMethod]
        public void Poll_Should_Poll_Both_Keyboard_And_Mouse_Handlers()
        {
            // Act
            _gameInputService.Poll();

            // Assert
            _mouseInput.Received().Poll(Arg.Any<MouseState>());
            _keyboardInput.Received().Poll(Arg.Any<KeyboardState>());
        }
        
        [TestMethod]
        public void Reset_Should_Reset_Both_Keyboard_And_Mouse_Handlers()
        {
            // Act
            _gameInputService.Reset();

            // Assert
            _mouseInput.Received().Reset();
            _keyboardInput.Received().Reset();
        }

        [TestMethod]
        public void RevertInput_Should_Revert_Everything_And_Not_Subscribe_Anything_If_Handlers_Not_Found()
        {
            // Arrange
            var unregisteredMouseHandler = Substitute.For<IMouseHandler>();
            var unregisteredKeyboardHandler = Substitute.For<IKeyboardHandler>();

            var firstMouseHandler = Substitute.For<IMouseHandler>();
            var firstKeyboardHandler = Substitute.For<IKeyboardHandler>();

            var secondMouseHandler = Substitute.For<IMouseHandler>();
            var secondKeyboardHandler = Substitute.For<IKeyboardHandler>();

            _gameInputService.ChangeInput(firstMouseHandler, firstKeyboardHandler);
            _gameInputService.ChangeInput(secondMouseHandler, secondKeyboardHandler);

            _mouseInput.ClearReceivedCalls();
            _keyboardInput.ClearReceivedCalls();

            // Act
            _gameInputService.RevertInputUpToAndIncluding(unregisteredMouseHandler, unregisteredKeyboardHandler);

            // Assert
            _mouseInput.Received().Unsubscribe(Arg.Is(firstMouseHandler));
            _keyboardInput.Received().Unsubscribe(Arg.Is(firstKeyboardHandler));

            _mouseInput.Received().Unsubscribe(Arg.Is(secondMouseHandler));
            _keyboardInput.Received().Unsubscribe(Arg.Is(secondKeyboardHandler));
        }
    }
}