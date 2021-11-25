using GeonBit.UI.Entities;

using MediatR;

namespace DavidFidge.Monogame.Core.View.Extensions
{
    public static class ButtonExtensions
    {
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

        public static Button SendOnClick(this Button button, IMediator mediator, IRequest request)
        {
            button.OnClick = entity => mediator.Send(request);
            return button;
        }

        public static Button AddTo(this Button button, Panel panel)
        {
            panel.AddChild(button);

            return button;
        }

        public static Button AsToggle(this Button button)
        {
            button.ToggleMode = true;

            return button;
        }
    }
}
