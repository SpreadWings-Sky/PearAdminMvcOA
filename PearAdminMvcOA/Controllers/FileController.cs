using PearAdminMvcOA.Models;
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
        //首页文件
        public ActionResult OrderFile()
        {
            using ( OAEntities db = new OAEntities())
            {
                var FileList = db.FileInfo.Include("FileTypeInfo").Include("UserInfo").Where(n => n.IfDelete == 0).OrderByDescending(n => n.FileType).Select(n => new
                {
                    id = n.FileId,
                    icon = n.FileTypeInfo.FileTypeImage,
                    FileOwner = n.UserInfo.UserName,
                    createTime = n.CreateDate,
                    FileName = n.FileName,
                    FileType = n.FileType,
                    fileId = n.FileId
                }).Take(5).ToList();
                return Json(new { code = 0, data = FileList });
            }
        }
    }
}