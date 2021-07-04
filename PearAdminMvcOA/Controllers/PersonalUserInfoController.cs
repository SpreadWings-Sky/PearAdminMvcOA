using PearAdminMvcOA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PearAdminMvcOA.Controllers
{
    public class PersonalUserInfoController : ApiController
    {
        //用户列表
        [HttpGet]
        public IHttpActionResult List(int userid,string type)
        {
            using (OAEntities db = new OAEntities())
            {
                var list = db.UserInfo.OrderByDescending(n => n.UserId== userid).ToList();
                return Json(new { code = 0, msg = "", data = list });
            }
        }
        [HttpGet]
        public IHttpActionResult List(int id)
        {
            using (OAEntities db = new OAEntities())
            {
                var list = db.UserInfo.FirstOrDefault(p => p.UserId == id);
                return Json(new { code = 0, msg = "", data = list });
            }
        }
        [HttpGet]
        public IHttpActionResult ScheduleList(int userid, string type,string realName)
        {
            if (realName == null)
            {
                return List(userid,type);
            }
            using (OAEntities db = new OAEntities())
            {
                var list = db.UserInfo.Where(p => p.UserName.Contains(realName)).OrderByDescending(n => n.UserId==userid).ToList();
                var Count = db.Schedule.Count();
                return Json(new { code = 0, msg = "", count = Count, data = list });
            }
        }
        [HttpPost]
        public IHttpActionResult Edit(UserInfo u)
        {
            using (OAEntities db = new OAEntities())
            {
                UserInfo us = db.UserInfo.FirstOrDefault(p => p.UserId == u.UserId);
                us.UserName = u.UserName;
                us.Phone = u.Phone;
                us.Email = u.Email;
                us.Gender = u.Gender;
                if (db.SaveChanges() > 0)
                {
                    return Json(new { success = true, code = 0, msg = "成功" });
                }
                else
                {
                    return Json(new { success = false, code = 1, msg = "失败" });
                }
            }
        }
    }
}
