using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PearAdminMvcOA.Models;

namespace PearAdminMvcOA.Controllers
{
    [Authorize]
    public class HRDataController : ApiController
    {
        //请求列表
        [HttpGet]
        public IHttpActionResult UserList(int page=1, int limit=10)
        {
            using (OAEntities db = new OAEntities())
            {
                List<UserInfo> list = db.UserInfo.OrderBy(n => n.UserId).Skip((page - 1) * limit).Take(limit).ToList();
                var Count = db.UserInfo.Count();
                return Json(new { code = 0, msg = "", count = Count, data = list });
            }
        }
        //带查询请求列表
        [HttpGet]
        public IHttpActionResult UserList(int page, int limit,string realName)
        {
            if (realName == null)
            {
                return UserList(page, limit);
            }
            else
            {
                using (OAEntities db = new OAEntities())
                {
                    List<UserInfo> list = db.UserInfo.Where(n => n.UserName.Contains(realName)).OrderBy(n => n.UserId).Skip((page - 1) * limit).Take(limit).ToList();
                    var Count = db.UserInfo.Where(n => n.UserName.Contains(realName)).Count();
                    return Json(new { code = 0, msg = "", count = Count, data = list });
                }
            }
        }
        //删除
        [HttpGet]
        public IHttpActionResult DeleteUser(long? UserId)
        {
            if (UserId != null)
            {
                using (OAEntities db = new OAEntities())
                {
                    var User = db.UserInfo.FirstOrDefault(n => n.UserId == UserId);
                    db.UserInfo.Remove(User);
                    bool result = db.SaveChanges() > 0;
                    return result ? Json(new { success = true, msg = "成功" }) : Json(new { success = false, msg = "失败" });
                }
            }
            return Json(new { success = false, msg = "失败" });
        }
        //修改
        [HttpPut]
        public IHttpActionResult EditUser(UserInfo user)
        {
            if (user != null && ModelState.IsValid)
            {
                using (OAEntities db = new OAEntities())
                {
                    var us = db.UserInfo.FirstOrDefault(n => n.UserId == user.UserId);
                    us.UserName = user.UserName;
                    us.WordId = user.WordId;
                    us.Gender = user.Gender;
                    us.Email = user.Email;
                    us.Phone = user.Phone;
                    if (db.SaveChanges() > 0)
                    {
                        return Json(new { success = true, msg = "更新成功" });
                    }
                    return Json(new { success = false, msg = "数据有误" });
                }
            }
            return Json(new { success = false, msg = "数据有误" });
        }
        //用户是否启用
        [HttpGet]
        public IHttpActionResult Enable(long? UserId, bool State)
        {
            if (UserId != null)
            {
                using (OAEntities db = new OAEntities())
                {
                    var us = db.UserInfo.FirstOrDefault(n => n.UserId == UserId);
                    us.UserState = State ? 1 : 0;
                    if (db.SaveChanges() > 0)
                    {
                        return Json(new { success = true, msg = "更新成功" });
                    }
                    return Json(new { success = false, msg = "数据有误" });
                }
            }
            return Json(new { success = false, msg = "数据有误" });
        }
        //添加
        [HttpPost]
        public IHttpActionResult AddUser(UserInfo user)
        {
            UserInfo us = new UserInfo()
            {
                WordId = user.WordId,
                UserName = user.UserName,
                Gender = user.Gender,
                Email = user.Email,
                Phone = user.Phone,
                CreateTime = DateTime.Now.ToString(),
                PassWord = Tools.CommonBLL.Md5Encoding("66666666"),
                Avatar = "/libs/admin/images/avatar.jpg",
                RoleId = user.RoleId,
                UserState = 1
            };
            using (OAEntities db = new OAEntities())
            {
                try
                {
                    db.UserInfo.Add(us);
                    if (db.SaveChanges() > 0)
                    {
                        return Json(new { success = true, msg = "更新成功" });
                    }
                    return Json(new { success = false, msg = "数据有误" });
                }
                catch (Exception)
                {
                    return Json(new { success = false, msg = "数据有误" });
                }
               
            }
        }
    }
}
