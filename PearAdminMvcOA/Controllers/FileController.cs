using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PearAdminMvcOA.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
        // GET: File
        //文件上传
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FileList()
        {
            return View();
        }
        //文件分享
        public ActionResult Qrjs(string Url)
        {
            ViewBag.Url = Url;
            return View();
        }
    }
}