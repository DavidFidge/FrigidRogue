using DavidFidge.MonoGame.Core.Components;

using InputHandlers.Mouse;

using Microsoft.Xna.Framework.Input;

namespace DavidFidge.MonoGame.Core.UserInterface
{
    public abstract class BaseMouseHandler : BaseComponent, IMouseHandler
    {
        public virtual void HandleMouseScrollWheelMove(MouseState mouseState, int difference)
        {
        }

        public virtual void HandleMouseMoving(MouseState mouseState, MouseState origin)
        {
        }

        public virtual void HandleLeftMouseClick(MouseState mouseState, MouseState origin)
        {
        }

        public virtual void HandleLeftMouseDoubleClick(MouseState mouseState, MouseState origin)
        {
        }

        public virtual void HandleLeftMouseDown(MouseState mouseState)
        {
        }

        public virtual void HandleLeftMouseUp(MouseState mouseState, MouseState origin)
        {
        }

        public virtual void HandleLeftMouseDragging(MouseState mouseState, MouseState originalMouseState)
        {
        }

        public virtual void HandleLeftMouseDragDone(MouseState mouseState, MouseState originalMouseState)
        {
        }

        public virtual void HandleRightMouseClick(MouseState mouseState, MouseState origin)
        {
        }

        public virtual void HandleRightMouseDoubleClick(MouseState mouseState, MouseState origin)
        {
        }

        public virtual void HandleRightMouseDown(MouseState mouseState)
        {
        }

        public virtual void HandleRightMouseUp(MouseState mouseState, MouseState origin)
        {
        }

        public virtual void HandleRightMouseDragging(MouseState mouseState, MouseState originalMouseState)
        {
        }

        public virtual void HandleRightMouseDragDone(MouseState mouseState, MouseState originalMouseState)
        {
        }

        public virtual void HandleMiddleMouseClick(MouseState mouseState, MouseState origin)
        {
        }

        public virtual void HandleMiddleMouseDoubleClick(MouseState mouseState, MouseState origin)
        {
        }

        public virtual void HandleMiddleMouseDown(MouseState mouseState)
        {
        }

        public virtual void HandleMiddleMouseUp(MouseState mouseState, MouseState origin)
        {
        }

        public virtual void HandleMiddleMouseDragging(MouseState mouseState, MouseState originalMouseState)
        {
        }

        public virtual void HandleMiddleMouseDragDone(MouseState mouseState, MouseState originalMouseState)
        {
        }
    }
}