﻿using System;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;

namespace SCv20_Tools.Web {
    // Note: For instructions on enabling IIS6 or IIS7 classic mode,
    // visit http://go.microsoft.com/?LinkId=9394801

    public class Global : System.Web.HttpApplication {

        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               "SceneDefault",
               "Mission/{missionid}/Scenes/{action}/{id}",
               new {
                   controller = "Scene",
                   missionid = UrlParameter.Optional,
                   action = "Create",
                   id = UrlParameter.Optional,
               },
               new {
                   missionid = @"\d+"
                   //id = @"\d+"
               });


            routes.MapRoute(
                "MissionDefault",
                "Mission/{id}/{action}/{key}",
                new {
                    controller = "Mission",
                    id = UrlParameter.Optional,
                    action = "Index",
                    key = UrlParameter.Optional
                }, new {
                    id = @"\d+"
                }
            );

           

            routes.MapRoute(
                "Default",                      // Route name
                "{controller}/{action}/{id}",   // URL with parameters
                new {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                } // Parameter defaults
            );
        }

        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();

            // Use LocalDB for Entity Framework by default
            //Database.DefaultConnectionFactory = new SqlConnectionFactory(@"Data Source=(localdb)\v11.0; Integrated Security=True; MultipleActiveResultSets=True");

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_BeginRequest(object sender, EventArgs e) {
            var ci = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();  //new CultureInfo("pt-BR");
            ci.NumberFormat.CurrencySymbol = "$";
            Thread.CurrentThread.CurrentCulture = ci;
        }
    }
}