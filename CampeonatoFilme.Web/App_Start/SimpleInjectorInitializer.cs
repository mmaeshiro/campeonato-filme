[assembly: WebActivator.PostApplicationStartMethod(typeof(CampeonatoFilme.Web.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace CampeonatoFilme.Web.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;
    using SimpleInjector;
    using SimpleInjector.Advanced;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    using System.Web;
    using Infra;

    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            // Chamada dos módulos do Simple Injector
            InitializeContainer(container);

            //// Necessário para registrar o ambiente do Owin que é dependência do Identity
            //// Feito fora da camada de IoC para não levar o System.Web para fora
            //container.RegisterPerWebRequest(() =>
            //{
                //if (HttpContext.Current != null && HttpContext.Current.Items["owin.Environment"] == null && container.IsVerifying())
                //{
                //    return new OwinContext().Authentication;
                //}
                //return HttpContext.Current.GetOwinContext().Authentication;

            //});

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void InitializeContainer(Container container)
        {        
            BootStrapper.RegisterServices(container);
        }
    }
}