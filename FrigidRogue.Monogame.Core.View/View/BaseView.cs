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
using FrigidRogue.MonoGame.Core.View.Interfaces;
using GeonBit.UI.Entities;
using InputHandlers.Keyboard;
using InputHandlers.Mouse;

using MediatR;

namespace FrigidRogue.MonoGame.Core.View
{
    public abstract class BaseView<TViewModel, TData> : BaseComponent, IView<IEntity>, IRequestHandler<UpdateViewRequest<TData>>
         where TViewModel : BaseViewModel<TData>
         where TData : new()
    {
        private readonly TViewModel _viewModel;

        protected List<IView> _components = new List<IView>();
        protected ViewType _viewType;

        public IRootPanel<IEntity> RootPanel { get; set; }
        public IKeyboardHandler KeyboardHandler { get; set; }
        public IMouseHandler MouseHandler { get; set; }
        public TData Data => _viewModel.Data;
        public IGameInputService GameInputService { get; set; }

        protected BaseView(TViewModel viewModel)
        {
            _viewType = ViewType.Root;
            _viewModel = viewModel;
        }

        public void Initialize()
        {
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

        public void Hide()
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

        protected virtual void UpdateView()
        {
        }

        public Task<Unit> Handle(UpdateViewRequest<TData> request, CancellationToken cancellationToken)
        {
            UpdateView();
            return Unit.Task;
        }
    }
}