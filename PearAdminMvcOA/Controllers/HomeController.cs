using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PearAdminMvcOA.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (Session["User"] == null) 
                return RedirectToAction("Login", "Account");
            return View();
        }
        //主界面
        public ActionResult Page()
        {
            return View();
        }
        //返回404界面
        public ActionResult NotFound()
        {
            Response.Status = "404 Not Found";
            Response.StatusCode = 404;
            return View();
        }
    }
}