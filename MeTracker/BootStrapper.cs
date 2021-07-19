using System;
using System.Linq;
using System.Reflection;
using Autofac;
using MeTracker.Repository;
using MeTracker.ViewModels;
using Xamarin.Forms;

namespace MeTracker
{
    public class BootStrapper
    {
        protected ContainerBuilder ContainerBuilder { get; set; }

        public BootStrapper()
        {
            Initialize();
            FinishInitialization();
        }

        protected virtual void Initialize()
        {
            ContainerBuilder = new ContainerBuilder();

            var currentAssembly = Assembly.GetExecutingAssembly();

            foreach (var type in currentAssembly.DefinedTypes.Where(e => e.IsSubclassOf(typeof(Page))))
            {
                ContainerBuilder.RegisterType(type.AsType());
            }

            foreach (var type in currentAssembly.DefinedTypes.Where(e => e.IsSubclassOf(typeof(ViewModel))))
            {
                ContainerBuilder.RegisterType(type.AsType());
            }

            ContainerBuilder.RegisterType<LocationRepository>().As<ILocationReposiroty>();
        }

        private void FinishInitialization()
        {
            var container = ContainerBuilder.Build();
            Resolver.Initialize(container);
        }
    }
}
