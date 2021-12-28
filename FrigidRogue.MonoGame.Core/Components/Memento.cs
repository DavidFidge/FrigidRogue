﻿using FrigidRogue.MonoGame.Core.Interfaces.Components;

namespace FrigidRogue.MonoGame.Core.Services
{
    public class Memento<T> : IMemento<T>
    {
        public Memento(T state)
        {
            State = state;
        }

        public Memento()
        {
        }

        public T State { get; set; }
    }

    public static class MementoExtensions
    {
        public static Memento<T> CreateMemento<T>(this T item)
        {
            return new Memento<T>(item);
        }
    }
}