using SOSGameLogic.Implementation;
using SOSGameLogic.Interfaces;
using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;
using Unity.Lifetime;
namespace SOSGameUI
{
    public class Global : HttpApplication
    {
        
        void Application_Start(object sender, EventArgs e)
        {
           // var container = new UnityContainer();
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //container.RegisterType<IGame, Game>(new ContainerControlledLifetimeManager()); // Singleton lifetime
            //container.RegisterType<IBoard, Board>(new ContainerControlledLifetimeManager());
            //container.RegisterType<IPlayer, Player>(new ContainerControlledLifetimeManager());
            //container.RegisterType<IEngine, Engine>(new ContainerControlledLifetimeManager());

            // Register the container globally for the application
//HttpContext.Current.Application["UnityContainer"] = container;

        }
    }
}