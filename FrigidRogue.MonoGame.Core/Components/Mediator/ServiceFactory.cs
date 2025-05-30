﻿namespace FrigidRogue.MonoGame.Core.Components.Mediator;

public delegate object ServiceFactory(Type serviceType);

public static class ServiceFactoryExtensions
{
    public static T GetInstance<T>(this ServiceFactory factory)
        => (T) factory(typeof(T));

    public static IEnumerable<T> GetInstances<T>(this ServiceFactory factory)
        => (IEnumerable<T>) factory(typeof(IEnumerable<T>));
}