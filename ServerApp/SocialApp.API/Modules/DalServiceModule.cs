
using Autofac;
using SocialApp.BusinessLayer.Abstract;
using SocialApp.BusinessLayer.Concrete;
using SocialApp.DataAccessLayer.Abstract;
using SocialApp.DataAccessLayer.Concrete;
using SocialApp.DataAccessLayer.Repositories;
using SocialApp.DataAccessLayer.UnitOfWork;
using System.Reflection;

namespace SocialApp.API.Modules
{
    public class DalServiceModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericDal<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(GenericService<>)).As(typeof(IGenericService<>)).InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            var apiAssembly = Assembly.GetExecutingAssembly();
            var dalAssembly = Assembly.GetAssembly(typeof(SocialContext));
            var blAssembly= Assembly.GetAssembly(typeof(IUserService));
            
            builder.RegisterAssemblyTypes(apiAssembly, dalAssembly, blAssembly).Where(x => x.Name.EndsWith("Dal")).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(apiAssembly, dalAssembly, blAssembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();
         
        }
    }
}
