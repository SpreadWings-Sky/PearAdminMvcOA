using PearAdminMvcOA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PearAdminMvcOA.Controllers
{
    public class CheckController : Controller
    {
        // GET: Check
        public ActionResult Check()
        {
            return View();
        }
        public ActionResult UserCheck()
        {
            ViewBag.Time = DateTime.Now;
            return View();
        }
        public ActionResult Edit(int? SignId)
        {
            using (OAEntities db = new OAEntities())
            {
                var user = db.ManualSign.Select(p => new
                {
                    SignId = p.SignId,
                    UserId = db.UserInfo.FirstOrDefault(u => u.UserId == p.UserId).UserName,
                    SignTime = p.SignTime,
                    SignDesc = p.SignDesc,//db.Remarks.FirstOrDefault(e => e.RemarksID == p.SignDesc).RemaName,
                    SignText = p.SignText,
                    RoleId = db.RoleInfo.FirstOrDefault(r => r.RoleId == p.RoleId).RoleName,
                }).FirstOrDefault(p=>p.SignId==SignId);
                ViewBag.SignId = user.SignId;
                ViewBag.UserName = user.UserId;
                ViewBag.SignTime = user.SignTime;
                ViewBag.SignDesc = user.SignDesc;
                ViewBag.SignText = user.SignText;
                ViewBag.RoleId = user.RoleId;
                if (user == null)
                {
                    return Json("请选择");
                }
                return View("~/Views/Check/Edit.cshtml");
            }
        }
    }
}