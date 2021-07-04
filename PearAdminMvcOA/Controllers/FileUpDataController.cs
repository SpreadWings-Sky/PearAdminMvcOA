using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
                    //获取用户ID
                    int UserID = int.Parse(HttpContext.Current.Request.Cookies["UserId"].Value[0].ToString());
                    //存储信息到数据库
                    using (OAEntities db = new OAEntities())
                    {
                        FileInfo info = new FileInfo
                        {
                            FileName = Name,
                            FileType = db.FileTypeInfo.FirstOrDefault(n => n.FileTypeSuffix.Contains(fileExtension.ToLower())).FileTypeId,
                            FileOwner = UserID,
                            CreateDate = DateTime.Now,
                            FilePath = @"~/UserFile/" + fileName,
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
                    return Json(new { code = 0 });
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
        public IHttpActionResult GetFileList(int pare = 0)
        {
            using (OAEntities db = new OAEntities())
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
                }).ToList();
                return Json(new { code = 0, data = FileList });
            }
        }
        [HttpGet]
        public HttpResponseMessage DownFile(int FileID)
        {
            using (OAEntities db = new OAEntities())
            {
                FileInfo file = db.FileInfo.Find(FileID);

                var FilePath = System.Web.Hosting.HostingEnvironment.MapPath(file.FilePath);
                var stream = new FileStream(FilePath, FileMode.Open);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(stream);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = file.FileName
                };
                return response;
            }
        }

        [HttpPut]
        public IHttpActionResult DeleteFile(int Fielid)
        {
            using (OAEntities db = new OAEntities())
            {
                int UserID = int.Parse(HttpContext.Current.Request.Cookies["UserId"].Value[0].ToString());
                if(db.UserInfo.Find(UserID)==null)
                {
                    return Json(new { success = false, msg = "请先登录" });
                }
                FileInfo file = db.FileInfo.Find(Fielid);
                if(file.FileOwner != UserID)
                {
                    return Json(new { success = false, msg = "不能删除他人文件" });
                }
                file.IfDelete = 1;
                if (db.SaveChanges() > 0)
                {
                    return Json(new { success = true, msg = "删除成功,已进入回收站" });
                }
                else
                {
                    return Json(new { success = false, msg = "删除失败" });
                }
            }
        }
    }
}
