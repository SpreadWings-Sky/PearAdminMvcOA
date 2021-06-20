using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PearAdminMvcOA.Controllers
{
    [Authorize]
    public class BulletinController : Controller
    {
        // GET: Bulletin
        //公告管理
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        //添加公告
        public ActionResult Add()
        {
            return View("~/Views/Bulletin/BullManager/Add.cshtml");
        }
        //修改公告
        public ActionResult Edit(long? BID)
        {
            ViewBag.BID = BID;
            return View("~/Views/Bulletin/BullManager/Edit.cshtml");
        }
        //查看详细
        public ActionResult Detailed(long? BID)
        {
            ViewBag.BID = BID;
            return View("~/Views/Bulletin/BullManager/Detailed.cshtml");
        }
    }
}