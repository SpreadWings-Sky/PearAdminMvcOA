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
        public ActionResult RoleAdd()
        {
            ViewBag.UserID = (Session["User"] as UserInfo).UserId;
            return View("~/Views/HR/RoleManager/Add.cshtml");
        }
        public ActionResult RoleEdit(int? id)
        {
            ViewBag.Roleid = id;
            return View("~/Views/HR/RoleManager/Edit.cshtml");
        }
        //角色管理界面
        public ActionResult Role()
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

        //用户添加界面
        public ActionResult Department()
        {
            return View();
        }
        public ActionResult DepartmentAdd()
        {
            return View();
        }

        //修改查询页面
        public ActionResult DepartmentModify(int? DepartId)
        {
            using (OAEntities db = new OAEntities())
            {
                DepartInfo d = db.DepartInfo.Where(p => p.DepartId == DepartId).FirstOrDefault();
                ViewData["id"] = d;
                return View("~/Views/HR/DepartmentModify.cshtml", d);
            }
        }
    }
}