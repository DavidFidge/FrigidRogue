using FrigidRogue.MonoGame.Core.Components.Mediator;
using GeonBit.UI.Entities;

namespace FrigidRogue.MonoGame.Core.View.Extensions
{
    public static class ButtonExtensions
    {
        public static Button SendOnClick<T>(this Button button, Action<T> afterCreateRequest, IMediator mediator)
            where T : IRequest, new()
        {
            button.OnClick = entity =>
            {
                var request = new T();
                afterCreateRequest(request);

                mediator.Send(request);
            };

            return button;
        }

        public static Button SendOnClick<TFirst, TSecond>(this Button button, Action<TFirst> firstAfterCreateRequest, Action<TSecond> secondAfterCreateRequest, IMediator mediator)
            where TFirst : IRequest, new()
            where TSecond : IRequest, new()
        {
            button.OnClick = entity =>
            {
                var firstRequest = new TFirst();
                firstAfterCreateRequest(firstRequest);

                var secondRequest = new TSecond();
                secondAfterCreateRequest(secondRequest);

                mediator.Send(firstRequest);
                mediator.Send(secondRequest);
            };

            return button;
        }

        public static Button SendOnClick<T>(this Button button, IMediator mediator)
            where T : IRequest, new()
        {
            button.OnClick = entity => mediator.Send(new T());
            return button;
        }

        public static Button SendOnClick<T1, T2>(this Button button, IMediator mediator)
            where T1 : IRequest, new()
            where T2 : IRequest, new()
        {
            button.OnClick = entity =>
            {
                mediator.Send(new T1());
                mediator.Send(new T2());
            };

            return button;
        }

        public static Button SendOnClick<T>(this Button button, IMediator mediator, T request)
            where T : IRequest, new()
        {
            button.OnClick = entity => mediator.Send(request);
            return button;
        }

        public static T AddTo<T>(this T button, Panel panel) where T : Entity
        {
            panel.AddChild(button);

            return button;
        }

        public static Button AsToggle(this Button button)
        {
            button.ToggleMode = true;

            return button;
        }
        
        public static Button WidthText(this Button button)
        {
            var destRect = button.ButtonParagraph.GetTextDestRect();
            button.Width(destRect.Width);

            return button;
        }
        
        public static Button WidthTextWithPadding(this Button button, int extraPadding)
        {
            button.WidthText();
            button.Width(button.Size.X + extraPadding);

            return button;
        }
        
        public static Button HeightText(this Button button)
        {
            var destRect = button.ButtonParagraph.GetTextDestRect();
            button.Height(destRect.Height);

            return button;
        }
        
        public static Button HeightTextWithPadding(this Button button, int extraPadding)
        {
            button.HeightText();
            button.Width(button.Size.Y + extraPadding);

            return button;
        }
    }
}
