using System.Web.Mvc;
using Microsoft.Practices.Unity;
using ToDo.ServicesApi;
using Unity.Mvc3;

namespace ToDo
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IServiceApiItem, ServiceApiItem>(new HierarchicalLifetimeManager());
            container.RegisterType<IServiceApiListItem, ServiceApiListItem>(new HierarchicalLifetimeManager());

            return container;
        }
    }
}