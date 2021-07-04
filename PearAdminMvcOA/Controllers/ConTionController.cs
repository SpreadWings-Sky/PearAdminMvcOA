using PearAdminMvcOA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PearAdminMvcOA.Controllers
{
    public class ConTionController : Controller
    {
        // GET: ConTion
        public ActionResult Management()
        {
            return View();
        }
        public ActionResult ManAdd()
        {
            return View();
        }

        public ActionResult ManModify(int? ManId)
        {
            using (OAEntities db = new OAEntities())
            {
                Management d = db.Management.Where(p => p.ManId == ManId).FirstOrDefault();
                ViewBag.Name = db.UserInfo.FirstOrDefault(n => n.UserId == d.UserId).UserName;
                ViewData["id"] = d;
                return View("~/Views/ConTion/ManModify.cshtml", d);
            }
        }
    }
}