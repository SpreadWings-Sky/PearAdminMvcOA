using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PearAdminMvcOA.Controllers
{
    public class HRController : Controller
    {
        // GET: HR
        //用户列表界面
        public ActionResult UserTablePage()
        {
            return View();
        }
        //用户添加界面
    }
}