using Autofac;
using Autofac.Integration.WebApi;
using InternHub.Model;
using InternHub.Model.Common;
using InternHub.Repository;
using InternHub.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace InternHub.WebApi.App_Start
{
    public class DependencyInjectionConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();


            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new RepositoryModule());
            builder.Register(x => new ConnectionString { Name = Environment.GetEnvironmentVariable("ConnectionString") }).As<IConnectionString>();

            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            // OPTIONAL: Register the Autofac model binder provider.
            builder.RegisterWebApiModelBinderProvider();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}