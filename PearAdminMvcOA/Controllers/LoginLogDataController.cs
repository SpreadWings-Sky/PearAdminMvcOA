using PearAdminMvcOA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PearAdminMvcOA.Controllers
{
    public class LoginLogDataController : ApiController
    {
        //查询
        [HttpGet]
        public IHttpActionResult LoginLoginfo(int page,int limit)
        {
            using (OAEntities db = new OAEntities())
            {
                var List = db.LoginLog.OrderBy(n => n.LoginId).Skip((page - 1) * limit).Take(limit).Select(n => new
                {
                    UserId = db.UserInfo.Where(p=>p.UserId==n.UserId).FirstOrDefault().UserName,
                    LoginTime = n.LoginTime,
                    IfSuccess = n.IfSuccess,
                    LoginUserIp = n.LoginUserIp,
                    LoginDesc = n.LoginDesc,
                    Browser = n.Browser
                }).ToList();
                var Count = db.LoginLog.Count();
                return Json(new { code = 0, msg = "", count = Count, data = List });
            }
        }

        [HttpGet]
        public IHttpActionResult LoginLogkeyword(string UserName,int page, int limit)
        {
            if (UserName == null)
            {
                UserName = "";
            }
            using (OAEntities db = new OAEntities())
            {
                
                var List = db.LoginLog.Include("UserInfo").Where(p=>p.UserInfo.UserName.Contains(UserName) ).OrderBy(n => n.LoginId).Skip((page - 1) * limit).Take(limit).Select(n => new
                {
                    UserId = db.UserInfo.Where(p => p.UserId == n.UserId).FirstOrDefault().UserName,
                    LoginTime = n.LoginTime,
                    IfSuccess = n.IfSuccess,
                    LoginUserIp = n.LoginUserIp,
                    LoginDesc = n.LoginDesc,
                    Browser = n.Browser
                }).ToList();
                var Count = db.LoginLog.Count();
                return Json(new { code = 0, msg = "", count = Count, data = List });
            }
        }


        //删除
        [HttpPut]
        public IHttpActionResult DepartDelete(int? DepartId)
        {
            using (OAEntities db = new OAEntities())
            {
                DepartInfo lits = db.DepartInfo.FirstOrDefault(p => p.DepartId == DepartId);
                db.DepartInfo.Remove(lits);
                bool stc = false;
                string mes = "删除失败";
                if (db.SaveChanges() > 0)
                {
                    stc = true;
                    mes = "删除成功";
                }
                return Json(new { success = stc, msg = mes });
            }
        }
        
    }
}
