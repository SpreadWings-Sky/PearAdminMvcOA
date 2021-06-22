using PearAdminMvcOA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PearAdminMvcOA.Controllers
{
    public class PersonalSignController : ApiController
    {
        //查询便签列表
        [HttpGet]
        public IHttpActionResult NoteList(int id,int page, int limit)
        {
            using (OAEntities db = new OAEntities())
            {
                var list = db.ManualSign.Include("UserInfo").Where(p=>p.UserId== id&&p.SignDesc==5).OrderByDescending(p=>p.SignTime).Skip((page - 1) * limit).Take(limit).Select(p => new {
                    SignId = p.SignId,
                    UserName = p.UserInfo.UserName,
                    SignTime = p.SignTime,
                    SignText = p.SignText,
                }).ToList();
                var Count = db.ManualSign.Include("UserInfo").Where(p => p.UserId == id && p.SignDesc == 5).Count();
                return Json(new { code = 0, msg = "", count = Count, data = list });
            }
        }
        //模糊查询
        [HttpGet]
        public IHttpActionResult NoteList(int id, int page, int limit, string Time)
        {
            if (Time == null)
            {
                return NoteList(id, page, limit);
            }
            using (OAEntities db = new OAEntities())
            {
                var Start = Time.Split('t');
                DateTime stime = DateTime.Parse(Start[0]);
                DateTime etime = DateTime.Parse(Start[1]);
                var list = db.ManualSign.Include("UserInfo").Where(p => p.SignTime.CompareTo(stime) >= 0 && p.SignTime.CompareTo(etime) <= 0 && p.SignDesc == 5&&p.UserId==id).OrderByDescending(p => p.SignTime).Skip((page - 1) * limit).Take(limit).Select(p => new
                {
                    SignId = p.SignId,
                    UserName = p.UserInfo.UserName,
                    Userid = p.UserInfo.UserId,
                    SignTime = p.SignTime,
                    SignText = p.SignText,
                }).ToList() ;
                var Count = db.ManualSign.Include("UserInfo").Where(p => p.SignTime.CompareTo(stime) >= 0 && p.SignTime.CompareTo(etime) <= 0 && p.SignDesc == 5 && p.UserId == id).Count();
                return Json(new { code = 0, msg = "", count = Count, data = list });
            }
        }
        //查询单个便签
        [HttpGet]
        public IHttpActionResult Note(int? id)
        {
            using (OAEntities db = new OAEntities())
            {
                var list = db.Schedule.Include("UserInfo").FirstOrDefault(p => p.ScheduleId == id);
                return Json(new { code = 0, msg = "", data = list });
            }
        }
        //添加
        [HttpPost]
        public IHttpActionResult Add(MyNote Note)
        {
            using (OAEntities db = new OAEntities())
            {
                db.MyNote.Add(Note);
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
        public IHttpActionResult Edit(MyNote Note)
        {
            using (OAEntities db = new OAEntities())
            {
                db.Entry(Note).State = System.Data.Entity.EntityState.Modified;
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
                var list = db.MyNote.Include("UserInfo").FirstOrDefault(p => p.NoteId == id);
                db.MyNote.Remove(list);
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
