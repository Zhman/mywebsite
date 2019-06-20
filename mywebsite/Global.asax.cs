using JewelryStore.Models;
using JewelryStore.UI.Binders;
using JewelryStore.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace JewelryStore.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelBinders.Binders[typeof(List<CartItem>)] = new CartItemListModelBinder();
        }

        void Session_Start(object sender, EventArgs e)
        {
            HttpContext.Current.Session.Add("__MyAppSession", string.Empty);

            ////не дает не аутентифицированым пользователям доступ ко всем cartItem`ам всех пользователей в корзине
            //HttpCookie myCookie = new HttpCookie("ASP.NET_SessionId");
            //myCookie.Expires = DateTime.Now.AddDays(-1);

            //Response.Cookies.Add(myCookie);
        }
    }
}
