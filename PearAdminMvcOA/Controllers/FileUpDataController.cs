using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;
using PearAdminMvcOA.Models;
using FileInfo = PearAdminMvcOA.Models.FileInfo;

namespace PearAdminMvcOA.Controllers
{
    public class FileUpDataController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Uploadfile()
        {
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                try
                {
                    //得到客户端上传的文件
                    HttpPostedFile file = HttpContext.Current.Request.Files[0];
                    string Name = file.FileName;
                    string fileExtension = Path.GetExtension(file.FileName);// 文件扩展名
                    string fileName = Guid.NewGuid().ToString() + fileExtension;// 名称
                    //服务器端要保存的路径
                    string filePath = HttpContext.Current.Server.MapPath("~/UserFile/") + fileName;
                    //存储信息到数据库
                    using (OAEntities db = new OAEntities())
                    {
                        FileInfo info = new FileInfo
                        {
                            FileName = Name,
                            FileType = db.FileTypeInfo.FirstOrDefault(n => n.FileTypeSuffix.Contains(fileExtension.ToLower())).FileTypeId,
                            FileOwner = HttpContext.Current.Request.Cookies["UserId"].Value[0],
                            CreateDate = DateTime.Now, 
                            FilePath = filePath,
                            IfDelete = 0
                        };
                        db.FileInfo.Add(info);
                        if (!(db.SaveChanges() > 0))
                        {
                            return Json(new { res = "数据信息保存失败" });
                        }
                    }
                    file.SaveAs(filePath);
                    //返回结果
                    return Json( new { code = 0} );
                }
                catch (Exception ex)
                {
                    return Json(new { res = ex.Message });
                }
            }
            else
            {
                return Json(new { res = "error" }); ;
            }
        }
        [HttpGet]
        public IHttpActionResult GetFileList(int page = 1, int limit = 10)
        {
            using (OAEntities db = new OAEntities())
            {
                var FileList = db.FileInfo.Include("FileTypeInfo").Include("UserInfo").Where(n=>n.IfDelete==0).Select(n => new {
                    id=n.FileId,
                    icon = n.FileTypeInfo.FileTypeImage,
                    FileOwner = n.UserInfo.UserName,
                    createTime = n.CreateDate,
                    FileName = n.FileName
                }).ToList();
                return Json(new { code = 0, count = FileList.Count(), data = FileList });
            }
        }
    }
}
