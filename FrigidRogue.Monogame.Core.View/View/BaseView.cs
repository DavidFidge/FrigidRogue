using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

using FrigidRogue.MonoGame.Core.Components;
using FrigidRogue.MonoGame.Core.Interfaces.Services;
using FrigidRogue.MonoGame.Core.Messages;
using FrigidRogue.MonoGame.Core.UserInterface;
using FrigidRogue.MonoGame.Core.View.Extensions;
using FrigidRogue.MonoGame.Core.View.Interfaces;
using GeonBit.UI.Entities;
using InputHandlers.Keyboard;
using InputHandlers.Mouse;

using MediatR;
using Entity = GeonBit.UI.Entities.Entity;

namespace FrigidRogue.MonoGame.Core.View
{
    public abstract class BaseView<TViewModel, TData> : BaseGameComponent, IView<Entity>, IRequestHandler<NotifyViewModelChangedRequest<TData>>
         where TViewModel : BaseViewModel<TData>
         where TData : new()
    {
        protected readonly TViewModel _viewModel;

        protected List<IView> _components = new List<IView>();
        protected ViewType _viewType;

        public IRootPanel<Entity> RootPanel { get; set; }
        public IKeyboardHandler KeyboardHandler { get; set; }
        public IMouseHandler MouseHandler { get; set; }
        public TData Data => _viewModel.Data;
        public IGameInputService GameInputService { get; set; }
        protected bool IsVisible => IsInitialised && RootPanel.Visible;

        protected bool IsInitialised;

        protected BaseView(TViewModel viewModel)
        {
            _viewType = ViewType.Root;
            _viewModel = viewModel;
        }

        public void Initialize()
        {
            if (IsInitialised)
                return;

            IsInitialised = true;

            _viewModel.Initialize();

            RootPanel.Initialize(GetType().Name);

            InitializeInternal();
        }

        protected virtual void InitializeInternal()
        {
        }

        public virtual void Show()
        {
            RootPanel.Visible = true;

            if (_viewType == ViewType.Root)
                GameInputService?.ChangeInput(MouseHandler, KeyboardHandler);
            else
                GameInputService?.AddToCurrentGroup(MouseHandler, KeyboardHandler);

            foreach (var component in _components)
            {
                component.Show();
            }
        }

        public virtual void Hide()
        {
            RootPanel.Visible = false;

            foreach (var component in _components)
                component.Hide();

            if (_viewType == ViewType.Root)
                GameInputService?.RevertInputUpToAndIncluding(MouseHandler, KeyboardHandler);
            else
                GameInputService?.RemoveFromCurrentGroup(MouseHandler, KeyboardHandler);
        }

        public string LabelFor<T>(Expression<Func<T>> expression)
        {
            if (!(expression.Body is MemberExpression memberExpression))
                return string.Empty;

            var displayAttribute = memberExpression.Member.GetCustomAttribute<DisplayAttribute>();

            if (displayAttribute != null)
                return displayAttribute.Name;

            return DeriveLabelName(memberExpression.Member.Name);
        }

        private string DeriveLabelName(string nameToDerive)
        {
            var matches = Regex.Matches(
                nameToDerive,
                "[A-Z][^A-Z]*"
            );

            var values = new List<string>();

            for (int i = 0; i < matches.Count; i++)
                values.Add(matches[i].Value);

            return string.Join(" ", values);
        }

        protected virtual void ViewModelChanged()
        {
        }

        public Task<Unit> Handle(NotifyViewModelChangedRequest<TData> notifyViewModelChangedRequest, CancellationToken cancellationToken)
        {
            ViewModelChanged();
            return Unit.Task;
        }

        public virtual void Draw()
        {
        }

        public virtual void Update()
        {
        }

        protected void SetupChildPanelWithButton<TRequest, TChildViewModel, TChildData>(Panel parentPanel, string buttonLabel, BaseView<TChildViewModel, TChildData> childView)
            where TRequest : IRequest, new()
            where TChildViewModel : BaseViewModel<TChildData>
            where TChildData : new()
        {
            new Button(buttonLabel)
                .SendOnClick<TRequest>(Mediator)
                .AddTo(parentPanel);

            SetupChildPanel(childView);
        }

        protected void SetupSharedChildPanelWithButton<TRequest, TChildViewModel, TChildData>(Panel parentPanel, string buttonLabel, BaseView<TChildViewModel, TChildData> childView)
            where TRequest : IRequest, new()
            where TChildViewModel : BaseViewModel<TChildData>
            where TChildData : new()
        {
            new Button(buttonLabel)
                .SendOnClick<TRequest>(Mediator)
                .AddTo(parentPanel);

            // Only initialize, do not add as child to root panel - this is done when handling the message
            // as it is a view which is shared with another view.
            childView.Initialize();
        }

        protected Task<Unit> ShowChildView<TChildViewModel, TChildData>(BaseView<TChildViewModel, TChildData> childView, Panel panel)
            where TChildViewModel : BaseViewModel<TChildData>
            where TChildData : new()
        {
            childView.Show();
            panel.Visible = false;
            return Unit.Task;
        }

        protected Task<Unit> ShowChildViewWithRootSwap<TChildViewModel, TChildData>(BaseView<TChildViewModel, TChildData> childView, Panel panel)
            where TChildViewModel : BaseViewModel<TChildData>
            where TChildData : new()
        {
            childView.RootPanel.ClearParent();
            RootPanel.AddChild(childView.RootPanel);
            childView.Show();
            panel.Visible = false;
            return Unit.Task;
        }

        protected Task<Unit> HideChildView<TChildViewModel, TChildData>(BaseView<TChildViewModel, TChildData> childView, Panel panel)
            where TChildViewModel : BaseViewModel<TChildData>
            where TChildData : new()
        {
            if (RootPanel.HasChild(childView.RootPanel))
            {
                childView.Hide();
                panel.Visible = true;
            }

            return Unit.Task;
        }

        protected void SetupChildPanel<TChildViewModel, TChildData>(BaseView<TChildViewModel, TChildData> childView)
            where TChildViewModel : BaseViewModel<TChildData>
            where TChildData : new()
        {
            childView.Initialize();

            RootPanel.AddChild(childView.RootPanel);
        }
    }
}