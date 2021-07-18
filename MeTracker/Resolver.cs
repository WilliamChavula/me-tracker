using System;
using Autofac;

namespace MeTracker
{
    public class Resolver
    {
        public static IContainer _container;

        public static void Initialize(IContainer container)
        {
            _container = container;
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public Resolver()
        {
        }
    }
}
