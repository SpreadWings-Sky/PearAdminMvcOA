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
    public class BulletinDataController : ApiController
    {
        //请求列表
        [HttpGet]
        public IHttpActionResult BulletinList(int page = 1, int limit = 10)
        {
            using (OAEntities db = new OAEntities())
            {
                var list = db.BulletinInfo.OrderBy(n => n.BID).Skip((page - 1) * limit).Take(limit).Join(db.UserInfo, a => a.BCreateUser, b => b.UserId, (a, b) => new
                {
                    BID = a.BID,
                    BTitle = a.BTitle,
                    BType = a.BType == 1 ? "通知" : "公告",
                    BState = a.BState,
                    CreateName = b.UserName,
                    BCreateTime = a.BCreateTime
                }).ToList();
                var Count = db.BulletinInfo.Count();
                return Json(new { code = 0, msg = "", count = Count, data = list });
            }
        }
        //带条件的请求
        [HttpGet]
        public IHttpActionResult BulletinList(int page, int limit, string Name, int? State, string Time)
        {
            if (string.IsNullOrEmpty(Name))
            {
                Name = "";
            }
            using (OAEntities db = new OAEntities())
            {
                var list = db.BulletinInfo.OrderBy(n => n.BID).Skip((page - 1) * limit).Take(limit).Join(db.UserInfo, a => a.BCreateUser, b => b.UserId, (a, b) => new
                {
                    BID = a.BID,
                    BTitle = a.BTitle,
                    BType = a.BType == 1 ? "通知" : "公告",
                    BState = a.BState,
                    CreateName = b.UserName,
                    BCreateTime = a.BCreateTime
                }).Where(n => n.CreateName.Contains(Name));
                //状态筛选
                if (State != null)
                {
                    list = list.Where(n => n.BState == State);
                }
                //时间筛选
                if (!string.IsNullOrEmpty(Time))
                {
                    var Start = Time.Split('T');
                    DateTime sTime = DateTime.Parse(Start[0]);
                    DateTime eTime = DateTime.Parse(Start[1]);
                    list = list.Where(n => n.BCreateTime.CompareTo(sTime) >= 0 && n.BCreateTime.CompareTo(eTime)<=0);
                }
                return Json(new { code = 0, msg = "", count = list.Count(), data = list.ToList() });
            }
        }
        //删除公告
        [HttpGet]
        public IHttpActionResult DeleteBulletin(long? DeleteID)
        {
            if (DeleteID != null)
            {
                using (OAEntities db = new OAEntities())
                {
                    var Bull = db.BulletinInfo.FirstOrDefault(n=>n.BID == DeleteID);
                    db.BulletinInfo.Remove(Bull);
                    bool result = db.SaveChanges() > 0;
                    return result ? Json(new { success = true, msg = "成功" }) : Json(new { success = false, msg = "失败" });
                }
            }
            return Json(new { success = false, msg = "失败" });
        }
        //添加公告
        [HttpPost]
        public IHttpActionResult AddBulletin(BulletinInfo bull)
        {
            if (ModelState.IsValid)
            {
                bull.BCreateTime = DateTime.Now;
                using (OAEntities db = new OAEntities())
                {
                    try
                    {
                        db.BulletinInfo.Add(bull);
                        if (db.SaveChanges() > 0)
                        {
                            return Json(new { success = true, msg = "添加成功" });
                        }
                        return Json(new { success = false, msg = "添加数据有误" });
                    }
                    catch (Exception)
                    {
                        return Json(new { success = false, msg = "添加数据有误" });
                    }
                }
            }
            return Json(new { success = false, msg = "添加数据有误" });
        }
        //获取一条公告
        [HttpGet]
        public IHttpActionResult GetFirstBull(long? BID)
        {
            if (BID != null)
            {
                using (OAEntities db = new OAEntities())
                {
                    var Bull = db.BulletinInfo.FirstOrDefault(n => n.BID == BID);
                    if (Bull != null)
                    {
                        return Json(new { success = true, msg = "查询成功", data = Bull});
                    }
                    return Json(new { success = false, msg = "查询失败"});
                }
            }
            return Json(new { success = false, msg = "查询失败"});
        }
        //修改公告
        [HttpPut]
        public IHttpActionResult EditBulletin(BulletinInfo EditBull)
        {
            if (EditBull!=null && ModelState.IsValid)
            {
                using (OAEntities db = new OAEntities())
                {
                    try
                    {
                        var Bu = db.BulletinInfo.FirstOrDefault(n => n.BID == EditBull.BID);
                        Bu.BTitle = EditBull.BTitle;
                        Bu.BType = EditBull.BType;
                        Bu.BDesc = EditBull.BDesc;
                        Bu.BCreateTime = DateTime.Now;
                        Bu.BCreateUser = EditBull.BCreateUser;
                        if (db.SaveChanges() > 0)
                        {
                            return Json(new { success = true, msg = "更新成功" });
                        }
                        return Json(new { success = false, msg = "更新数据有误" });
                    }
                    catch (Exception)
                    {
                        return Json(new { success = false, msg = "更新数据有误" });
                    }
                }
            }
            return Json(new { success = false, msg = "更新数据有误" });
        }
        //公告是否启用
        [HttpGet]
        public IHttpActionResult Enable(long? BID, bool State)
        {
            if (BID != null)
            {
                using (OAEntities db = new OAEntities())
                {
                    var us = db.BulletinInfo.FirstOrDefault(n => n.BID == BID);
                    us.BState = State ? 10 : 20;
                    if (db.SaveChanges() > 0)
                    {
                        return Json(new { success = true, msg = "更新成功" });
                    }
                    return Json(new { success = false, msg = "出现错误" });
                }
            }
            return Json(new { success = false, msg = "出现错误" });
        }

    }
}
