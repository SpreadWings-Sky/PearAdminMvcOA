using PearAdminMvcOA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PearAdminMvcOA.Controllers
{
    public class PersonalScheduleController : ApiController
    {
        //查询日程列表
        [HttpGet]
        public IHttpActionResult ScheduleList(int id,int page, int limit)
        {
            using (OAEntities db = new OAEntities())
            {
                var list = db.Schedule.Include("UserInfo").Where(p=>p.Userid==id).OrderBy(n => n.ScheduleId).Skip((page - 1) * limit).Take(limit).Select(n => new
                {
                    ScheduleId = n.ScheduleId,
                    Title = n.Title,
                    Address = n.Address,
                    BeginTime = n.BeginTime,
                    EndTime = n.EndTime,
                    SchContent = n.SchContent,
                    UserName = n.UserInfo.UserName,
                    CreateTime = n.CreateTime
                }).ToList();
                var Count = db.Schedule.Include("UserInfo").Where(p => p.Userid == id).OrderBy(n => n.ScheduleId).Count();
                return Json(new { code = 0, msg = "", count = Count, data = list });
            }
        }
        [HttpGet]
        public IHttpActionResult ScheduleList(int id, int page , int limit, string realName) 
        {
            if (realName == null)
            {
                return ScheduleList(id,page, limit);
            }
            using (OAEntities db = new OAEntities())
            {
                var list = db.Schedule.Include("UserInfo").Where(p=>p.Title.Contains(realName)&&p.Userid==id).OrderBy(n => n.ScheduleId).Skip((page - 1) * limit).Take(limit).Select(n=>new
                {
                    ScheduleId = n.ScheduleId,
                    Title =n.Title,
                    Address = n.Address,
                    BeginTime = n.BeginTime,
                    EndTime = n.EndTime,
                    SchContent = n.SchContent,
                    UserName = n.UserInfo.UserName,
                    CreateTime = n.CreateTime
                }).ToList();
                var Count = db.Schedule.Include("UserInfo").Where(p => p.Title.Contains(realName) && p.Userid == id).Count();
                return Json(new { code = 0, msg = "", count = Count, data = list });
            }
        }
        //查询单个日程
        [HttpGet]
        public IHttpActionResult Schedule(int? id)
        {
            using (OAEntities db = new OAEntities())
            {
                var list = db.Schedule.Include("UserInfo").FirstOrDefault(p => p.ScheduleId == id);
                return Json(new { code = 0, msg = "", data = list });
            }
        }
        //添加
        [HttpPost]
        public IHttpActionResult Add(Schedule sche)
        {
            using(OAEntities db=new OAEntities())
            {
                db.Schedule.Add(sche);
                if (db.SaveChanges() > 0)
                {
                    return Json(new { code = 0, msg = "成功" });
                }
                else
                {
                    return Json(new { code = 1, msg = "失败" });
                }
            }
        }
        //修改
        [HttpPut]
        public IHttpActionResult Edit(Schedule sche)
        {
            using (OAEntities db = new OAEntities())
            {
                db.Entry(sche).State = System.Data.Entity.EntityState.Modified;
                if (db.SaveChanges() > 0)
                {
                    return Json(new { code = 0, msg = "成功" });
                }
                else
                {
                    return Json(new { code = 1, msg = "失败" });
                }
            }
        }
        //删除
        [HttpPost]
        public IHttpActionResult Detele(int? id)
        {
            using (OAEntities db = new OAEntities())
            {
                var list = db.Schedule.Include("UserInfo").FirstOrDefault(p => p.ScheduleId == id);
                db.Schedule.Remove(list);
                if (db.SaveChanges() > 0)
                {
                    return Json(new { success = true, msg = "成功" });
                }
                else
                {
                    return Json(new { success = false, msg = "失败" });
                }
            }
        }
    }
}
