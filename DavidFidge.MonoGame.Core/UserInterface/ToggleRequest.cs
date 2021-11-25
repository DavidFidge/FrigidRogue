﻿using MediatR;

namespace DavidFidge.MonoGame.Core.UserInterface
{
    public abstract class ToggleRequest : IRequest
    {
        protected ToggleRequest(bool isChecked)
        {
            IsChecked = isChecked;
        }

        public bool IsChecked { get; }
    }
}