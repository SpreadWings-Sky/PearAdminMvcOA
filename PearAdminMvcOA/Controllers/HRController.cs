using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PearAdminMvcOA.Models;

namespace PearAdminMvcOA.Controllers
{
    [Authorize]
    public class HRController : Controller
    {
        // GET: HR
        //用户列表界面
        public ActionResult UserTablePage()
        {
            return View();
        }
        //用户修改界面
        public ActionResult Edit(long? UserId)
        {
            using (OAEntities db = new OAEntities())
            {
                UserInfo user = db.UserInfo.FirstOrDefault(n => n.UserId == UserId);
                if (user == null)
                {
                    return Json("请选择");
                }
                return View("~/Views/HR/UserManger/Edit.cshtml", user);
            }
        }
        //添加界面
        public ActionResult Add()
        {
            return View("~/Views/HR/UserManger/Add.cshtml");
        }
    }
}