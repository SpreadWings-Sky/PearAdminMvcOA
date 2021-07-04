using PearAdminMvcOA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PearAdminMvcOA.Controllers
{
    public class ApplyDataController : ApiController
    {

        [HttpGet]
        public IHttpActionResult ApplyInfo(int page,int limit)
        {
            using (OAEntities db = new OAEntities())
            {
                var List = db.Apply.OrderBy(n => n.ApplyID).Skip((page - 1) * limit).Take(limit).Select(n => new
                {
                    ApplyID = n.ApplyID,
                    userid = db.UserInfo.FirstOrDefault(p => p.UserId == n.userid).UserName,
                    ApplicationContent = n.ApplicationContent,
                    ApplicationTime = n.ApplicationTime
                }).ToList();
                var Count = db.Apply.Count();
                return Json(new { code = 0, msg = "", count = Count, data = List });
            }
        }

        [HttpGet]
        public IHttpActionResult Applykeyword(string UserName, int page, int limit)
        {
            if (UserName == null)
            {
                UserName = "";
            }
            using (OAEntities db = new OAEntities())
            {
                var List = db.Apply.Include("UserInfo").Where(p=>p.UserInfo.UserName.Contains(UserName)).OrderBy(n => n.ApplyID).Skip((page - 1) * limit).Take(limit).Select(n => new
                {
                    ApplyID = n.ApplyID,
                    userid = db.UserInfo.FirstOrDefault(p => p.UserId == n.userid).UserName,
                    ApplicationContent = n.ApplicationContent,
                    ApplicationTime = n.ApplicationTime
                }).ToList();
                var Count = db.Apply.Count();
                return Json(new { code = 0, msg = "", count = Count, data = List });
            }
        }

    }
}
