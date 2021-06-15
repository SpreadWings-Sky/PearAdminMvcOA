using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PearAdminMvcOA.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Home
        //菜单
        public ActionResult Index()
        {
            return View();
        }
        //首页
        public ActionResult IndexPage()
        {
            return View();
        }
    }
}