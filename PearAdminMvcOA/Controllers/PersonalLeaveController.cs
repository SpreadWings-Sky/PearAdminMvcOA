using PearAdminMvcOA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PearAdminMvcOA.Controllers
{
    public class PersonalLeaveController : ApiController
    {
        [HttpGet]
        public IHttpActionResult List(int userid,int luserid)
        {
            using (OAEntities db = new OAEntities())
            {
                var list = db.LeaveMessage;
                if (userid == luserid)
                {
                    var list2 = list.Include("UserInfo").Where(p => p.userid == userid).Select(p=>new { 
                        Lid=p.Lid,
                        userid=p.userid,
                        luserid=p.Luserid,
                        leaveText=p.LeaveText,
                        time=p.CreateTime,
                        UserName=p.UserInfo1.UserName,
                        Img=p.UserInfo1.Avatar==null?"/libs/admin/images/avatar.jpg":p.UserInfo1.Avatar
                    }).ToList();
                    return Json(new { code = 0, msg = "", data = list2 });
                }
                else
                {
                    var list2 = list.Include("UserInfo").Where(p => p.Luserid == luserid&&p.userid==userid).Select(p => new {
                        Lid = p.Lid,
                        userid = p.userid,
                        luserid = p.Luserid,
                        leaveText = p.LeaveText,
                        time = p.CreateTime,
                        UserName = p.UserInfo1.UserName,
                        Img = p.UserInfo1.Avatar == null ? "/libs/admin/images/avatar.jpg" : p.UserInfo1.Avatar
                    }).ToList();
                    return Json(new { code = 0, msg = "", data = list2 });
                }
            }
        }
        [HttpPost]
        public IHttpActionResult Post(LeaveMessage l)
        {
            using(OAEntities db=new OAEntities())
            {
                db.LeaveMessage.Add(l);
                if (db.SaveChanges() > 0)
                {
                    return Json(new { code = 0, msg = "留言成功" });
                }
                else
                {
                    return Json(new { code = 1, msg = "服务器繁忙" });
                }
            }
        }
    }
}
