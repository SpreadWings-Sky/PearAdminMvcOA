using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PearAdminMvcOA
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        //捕获404
        protected void Application_Error(object sender, EventArgs e)
        {
            var error = Server.GetLastError();
            var code = (error is HttpException) ? (error as HttpException).GetHttpCode() : 500;
            if (code != 404)
            {
                Response.Clear();
                Server.ClearError();
            }
            else
            {

                //防止302，对seo友好。
                var exception = Server.GetLastError();
                var httpException = exception as HttpException;
                Response.Clear();
                Server.ClearError();
                var routeData = new RouteData();
                routeData.Values["controller"] = "Home";
                routeData.Values["action"] = "NotFound";
                routeData.Values["exception"] = exception;
                Response.TrySkipIisCustomErrors = true;
                IController errorManager = new Controllers.HomeController();
                HttpContextWrapper wrapper = new HttpContextWrapper(Context);
                var rc = new RequestContext(wrapper, routeData);
                errorManager.Execute(rc);
            }

        }
    }
}
