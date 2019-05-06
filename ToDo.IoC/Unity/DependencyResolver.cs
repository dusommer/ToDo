using Microsoft.Practices.Unity;
using prmToolkit.NotificationPattern;
using System.Data.Entity;
using ToDo.Domain.Interfaces.Repositories;
using ToDo.Domain.Interfaces.Repositories.Base;
using ToDo.Domain.Interfaces.Services;
using ToDo.Domain.Services;
using ToDo.Infra.Persistence;
using ToDo.Infra.Persistence.Repository;
using ToDo.Infra.Persistence.Repository.Base;
using ToDo.Infra.Transaction;

namespace ToDo.IoC.Unity
{
    public static class DependencyResolver
    {
        public static void Resolve(UnityContainer container)
        {
            container.RegisterType<DbContext, ToDoContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());
            container.RegisterType<INotifiable, Notifiable>(new HierarchicalLifetimeManager());
            container.RegisterType(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));
            container.RegisterType<IRepositoryItem, RepositoryItem>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryListItem, RepositoryListItem>(new HierarchicalLifetimeManager());
            container.RegisterType<IServiceItem, ServiceItem>(new HierarchicalLifetimeManager());
            container.RegisterType<IServiceListItem, ServiceListItem>(new HierarchicalLifetimeManager());
        }
    }
}
