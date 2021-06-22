using PearAdminMvcOA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PearAdminMvcOA.Controllers
{
    public class ConferenceRoomController : Controller
    {
        // GET: ConferenceRoom
        public ActionResult Room()
        {
            return View();
        }
        public ActionResult RoomAdd()
        {
            return View();
        }

        public ActionResult RoomModify(int? RoomId)
        {
            using (OAEntities db = new OAEntities())
            {
                ConferenceRoom d = db.ConferenceRoom.Where(p => p.RoomId == RoomId).FirstOrDefault();
                ViewData["id"] = d;
                return View("~/Views/ConferenceRoom/RoomModify.cshtml", d);
            }
        }


    }
}