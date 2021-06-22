using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PearAdminMvcOA.Models;

namespace PearAdminMvcOA.Controllers
{
    public class PersonalController : Controller
    {
        // GET: Personal
        public ActionResult Schedule()
        {
            return View();
        }
        public ActionResult ScheduleAdd()
        {
            ViewBag.UserID = (Session["User"] as UserInfo).UserId;
            return View("~/Views/Personal/ScheduleManager/Add.cshtml");
        }
        public ActionResult ScheduleEdit(int? id)
        {
            ViewBag.ScheduleId = id;
            return View("~/Views/Personal/ScheduleManager/Edit.cshtml");
        }
        public ActionResult Note()
        {
            return View();
        }
        public ActionResult NoteAdd()
        {
            ViewBag.UserName = (Session["User"] as UserInfo).UserName;
            return View("~/Views/Personal/NoteManager/Add.cshtml");
        }
        public ActionResult NoteEdit(int? id)
        {
            ViewBag.Noteid = id;
            return View("~/Views/Personal/NoteManager/Edit.cshtml");
        }
        public ActionResult Mail()
        {
            return View();
        }
        public ActionResult Leave()
        {
            return View();
        }
    }
}