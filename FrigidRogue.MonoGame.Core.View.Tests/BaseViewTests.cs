using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;

using FrigidRogue.MonoGame.Core.Interfaces.Services;
using FrigidRogue.MonoGame.Core.Messages;
using FrigidRogue.MonoGame.Core.UserInterface;
using FrigidRogue.MonoGame.Core.View;
using FrigidRogue.MonoGame.Core.View.Interfaces;
using FrigidRogue.TestInfrastructure;

using GeonBit.UI.Entities;

using InputHandlers.Keyboard;
using InputHandlers.Mouse;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using NSubstitute;

namespace FrigidRogue.MonoGame.Core.Tests.UserInterface
{
    [TestClass]
    public class BaseViewTests : BaseTest
    {
        [TestMethod]
        public void LabelFor_Should_Return_DisplayAttribute_Name()
        {
            // Arrange
            var testView = new TestView(new TestViewModel());

            // Act
            var result = testView.GetLabelForPropertyWithDisplay();

            // Assert
            Assert.AreEqual("Test Header", result);
        }

        [TestMethod]
        public void LabelFor_Should_Return_Property_Name_Split_By_Casing_If_Property_Does_Not_Have_DisplayAttribute()
        {
            // Arrange
            var testView = new TestView(new TestViewModel());

            // Act
            var result = testView.GetLabelForPropertyWithoutDisplay();

            // Assert
            Assert.AreEqual("Property Without Display", result);
        }

        [TestMethod]
        public void Initialize_Should_Create_An_Invisible_Container_Panel_With_Zero_Padding()
        {
            // Arrange
            var testViewModel = new TestViewModel();
            var rootPanel = Substitute.For<IRootPanel<IEntity>>();

            var testView = new TestView(testViewModel)
            {
                RootPanel = rootPanel
            };

            // Act
            testView.Initialize();

            // Assert
            Assert.IsTrue(testView.IsInitializeInternalCalled);
            rootPanel.Received().Initialize(Arg.Is("TestView"));
            Assert.IsNotNull(testViewModel.Data);
        }

        [TestMethod]
        public void Show_Should_Show_Panel_And_Change_Mouse_And_Keyboard_Handlers()
        {
            // Arrange
            var keyboardHandler = Substitute.For<IKeyboardHandler>();
            var mouseHandler = Substitute.For<IMouseHandler>();
            var gameInputService = Substitute.For<IGameInputService>();
            var rootPanel = Substitute.For<IRootPanel<IEntity>>();

            var testView = new TestView(new TestViewModel())
            {
                KeyboardHandler = keyboardHandler,
                MouseHandler = mouseHandler,
                GameInputService = gameInputService,
                RootPanel = rootPanel
            };

            testView.Initialize();
            rootPanel.ClearReceivedCalls();

            // Act
            testView.Show();

            // Assert
            var setVisibleCall = rootPanel.ReceivedCalls().Single();
            var rootPanelType = typeof(IRootPanel<IEntity>);
            var methodInfo = rootPanelType.GetMethod("set_Visible");

            Assert.AreEqual(methodInfo, setVisibleCall.GetMethodInfo());
            Assert.AreEqual(true, (bool)setVisibleCall.GetArguments().Single());

            gameInputService
                .Received()
                .ChangeInput(Arg.Is(mouseHandler), Arg.Is(keyboardHandler));
        }

        [TestMethod]
        public void Hide_Should_Hide_Panel_And_Revert_Input()
        {
            // Arrange
            var keyboardHandler = Substitute.For<IKeyboardHandler>();
            var mouseHandler = Substitute.For<IMouseHandler>();
            var gameInputService = Substitute.For<IGameInputService>();
            var rootPanel = Substitute.For<IRootPanel<IEntity>>();

            var testView = new TestView(new TestViewModel())
            {
                KeyboardHandler = keyboardHandler,
                MouseHandler = mouseHandler,
                GameInputService = gameInputService,
                RootPanel = rootPanel
            };

            testView.Initialize();
            testView.Show();
            gameInputService.ClearReceivedCalls();
            rootPanel.ClearReceivedCalls();

            // Act
            testView.Hide();

            // Assert
            var setVisibleCall = rootPanel.ReceivedCalls().Single();
            var rootPanelType = typeof(IRootPanel<IEntity>);
            var methodInfo = rootPanelType.GetMethod("set_Visible");

            Assert.AreEqual(methodInfo, setVisibleCall.GetMethodInfo());
            Assert.AreEqual(false, (bool)setVisibleCall.GetArguments().Single());

            gameInputService
                .Received()
                .RevertInputUpToAndIncluding(Arg.Is(mouseHandler), Arg.Is(keyboardHandler));
        }

        [TestMethod]
        public void Show_For_View_With_Component_Should_Also_Show_Component_Panel_And_Include_Component_Mouse_And_Keyboard_Handlers_()
        {
            // Arrange
            var gameInputService = Substitute.For<IGameInputService>();

            var keyboardHandlerComponent = Substitute.For<IKeyboardHandler>();
            var mouseHandlerComponent = Substitute.For<IMouseHandler>();
            var rootPanelComponent = Substitute.For<IRootPanel<IEntity>>();

            var testComponentView = new TestComponentView(new TestComponentViewModel())
            {
                KeyboardHandler = keyboardHandlerComponent,
                MouseHandler = mouseHandlerComponent,
                GameInputService = gameInputService,
                RootPanel = rootPanelComponent
            };

            var keyboardHandler = Substitute.For<IKeyboardHandler>();
            var mouseHandler = Substitute.For<IMouseHandler>();
            var rootPanel = Substitute.For<IRootPanel<IEntity>>();

            var testView = new TestViewWithComponentView(new TestViewWithComponentViewModel(), testComponentView)
            {
                KeyboardHandler = keyboardHandler,
                MouseHandler = mouseHandler,
                GameInputService = gameInputService,
                RootPanel = rootPanel
            };

            testComponentView.Initialize();
            testView.Initialize();
            rootPanel.ClearReceivedCalls();
            rootPanelComponent.ClearReceivedCalls();

            // Act
            testView.Show();

            // Assert
            var setVisibleCall = rootPanel.ReceivedCalls().Single();
            var rootPanelType = typeof(IRootPanel<IEntity>);
            var methodInfo = rootPanelType.GetMethod("set_Visible");

            Assert.AreEqual(methodInfo, setVisibleCall.GetMethodInfo());
            Assert.AreEqual(true, (bool)setVisibleCall.GetArguments().Single());

            gameInputService
                .Received()
                .ChangeInput(Arg.Is(mouseHandler), Arg.Is(keyboardHandler));

            var setVisibleCallComponent = rootPanelComponent.ReceivedCalls().Single();
            var rootPanelTypeComponent = typeof(IRootPanel<IEntity>);
            var methodInfoComponent = rootPanelTypeComponent.GetMethod("set_Visible");

            Assert.AreEqual(methodInfoComponent, setVisibleCallComponent.GetMethodInfo());
            Assert.AreEqual(true, (bool)setVisibleCallComponent.GetArguments().Single());

            gameInputService
                .Received()
                .ChangeInput(Arg.Is(mouseHandler), Arg.Is(keyboardHandler));

            gameInputService
                .Received()
                .AddToCurrentGroup(Arg.Is(mouseHandlerComponent), Arg.Is(keyboardHandlerComponent));
        }

        [TestMethod]
        public void Hide_For_View_With_Component_Should_Also_Hide_Component_Panel_And_Revert_Input()
        {
            // Arrange
            var gameInputService = Substitute.For<IGameInputService>();

            var keyboardHandlerComponent = Substitute.For<IKeyboardHandler>();
            var mouseHandlerComponent = Substitute.For<IMouseHandler>();
            var rootPanelComponent = Substitute.For<IRootPanel<IEntity>>();

            var testComponentView = new TestComponentView(new TestComponentViewModel())
            {
                KeyboardHandler = keyboardHandlerComponent,
                MouseHandler = mouseHandlerComponent,
                GameInputService = gameInputService,
                RootPanel = rootPanelComponent
            };

            var keyboardHandler = Substitute.For<IKeyboardHandler>();
            var mouseHandler = Substitute.For<IMouseHandler>();
            var rootPanel = Substitute.For<IRootPanel<IEntity>>();

            var testView = new TestViewWithComponentView(new TestViewWithComponentViewModel(), testComponentView)
            {
                KeyboardHandler = keyboardHandler,
                MouseHandler = mouseHandler,
                GameInputService = gameInputService,
                RootPanel = rootPanel
            };

            testComponentView.Initialize();
            testView.Initialize();

            testView.Show();
            gameInputService.ClearReceivedCalls();
            rootPanel.ClearReceivedCalls();
            rootPanelComponent.ClearReceivedCalls();

            // Act
            testView.Hide();

            // Assert
            var setVisibleCall = rootPanel.ReceivedCalls().Single();
            var rootPanelType = typeof(IRootPanel<IEntity>);
            var methodInfo = rootPanelType.GetMethod("set_Visible");

            Assert.AreEqual(methodInfo, setVisibleCall.GetMethodInfo());
            Assert.AreEqual(false, (bool)setVisibleCall.GetArguments().Single());

            var setVisibleCallComponent = rootPanelComponent.ReceivedCalls().Single();
            var rootPanelTypeComponent = typeof(IRootPanel<IEntity>);
            var methodInfoComponent = rootPanelTypeComponent.GetMethod("set_Visible");

            Assert.AreEqual(methodInfoComponent, setVisibleCallComponent.GetMethodInfo());
            Assert.AreEqual(false, (bool)setVisibleCallComponent.GetArguments().Single());

            gameInputService
                .Received()
                .RevertInputUpToAndIncluding(Arg.Is(mouseHandler), Arg.Is(keyboardHandler));

            gameInputService
                .Received()
                .RemoveFromCurrentGroup(Arg.Is(mouseHandlerComponent), Arg.Is(keyboardHandlerComponent));
        }

        [TestMethod]
        public void UpdateViewRequest_Should_Call_UpdateView()
        {
            // Arrange
            var testViewModel = new TestViewModel();
            var rootPanel = Substitute.For<IRootPanel<IEntity>>();

            var testView = new TestView(testViewModel)
            {
                RootPanel = rootPanel
            };

            // Act
            testView.Handle(new UpdateViewRequest<TestData>(), new CancellationToken());

            // Assert
            Assert.IsTrue(testView.UpdateViewCalled);
        }

        public class TestView : BaseView<TestViewModel, TestData>
        {
            public bool IsInitializeInternalCalled { get; private set; }
            public bool UpdateViewCalled { get; private set; }

            public TestView(TestViewModel viewModel) : base(viewModel)
            {
            }

            protected override void InitializeInternal()
            {
                IsInitializeInternalCalled = true;
            }

            public string GetLabelForPropertyWithDisplay()
            {
                return LabelFor(() => Data.PropertyWithDisplay);
            }

            public string GetLabelForPropertyWithoutDisplay()
            {
                return LabelFor(() => Data.PropertyWithoutDisplay);
            }

            protected override void UpdateView()
            {
                UpdateViewCalled = true;
            }
        }

        public class TestData
        {
            [Display(Name = "Test Header")]
            public string PropertyWithDisplay { get; set; }

            public string PropertyWithoutDisplay { get; set; }
        }

        public class TestViewModel : BaseViewModel<TestData>
        {
        }

        public class TestViewWithComponentView : BaseView<TestViewWithComponentViewModel, TestViewWithComponentData>
        {
            public TestViewWithComponentView(
                TestViewWithComponentViewModel viewModel,
                TestComponentView testComponentView) : base(viewModel)
            {
                _components.Add(testComponentView);
            }
        }

        public class TestViewWithComponentViewModel : BaseViewModel<TestViewWithComponentData>
        {
        }

        public class TestViewWithComponentData
        {
        }

        public class TestComponentView : BaseView<TestComponentViewModel, TestComponentData>
        {
            public TestComponentView(TestComponentViewModel viewModel) : base(viewModel)
            {
                _viewType = ViewType.Component;
            }
        }

        public class TestComponentViewModel : BaseViewModel<TestComponentData>
        {
        }

        public class TestComponentData
        {
        }
    }
}
