using PearAdminMvcOA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PearAdminMvcOA.Controllers
{
    public class PersonalNoteController : ApiController
    {
        //查询便签列表
        [HttpGet]
        public IHttpActionResult NoteList(int id,int page, int limit)
        {
            using (OAEntities db = new OAEntities())
            {
                string user = (db.UserInfo.FirstOrDefault(p => p.UserId == id)).UserName;
                var list = db.MyNote.Where(p=>p.CreateUser==user).OrderBy(n => n.NoteId).Skip((page - 1) * limit).Take(limit).ToList();
                var Count = db.MyNote.Count();
                return Json(new { code = 0, msg = "", count = Count, data = list });
            }
        }
        //模糊查询
        [HttpGet]
        public IHttpActionResult NoteList(int id,int page, int limit, string realName)
        {
            if (realName == null)
            {
                return NoteList(id,page, limit);
            }
            using (OAEntities db = new OAEntities())
            {
                string user = (db.UserInfo.FirstOrDefault(p => p.UserId == id)).UserName;
                var list = db.MyNote.OrderBy(n => n.NoteId).Skip((page - 1) * limit).Take(limit).Where(p=>p.NoteTitle.Contains(realName)&&p.CreateUser==user).ToList();
                var Count = db.Schedule.Count();
                return Json(new { code = 0, msg = "", count = Count, data = list });
            }
        }
        //查询单个便签
        [HttpGet]
        public IHttpActionResult Note(int? id)
        {
            using (OAEntities db = new OAEntities())
            {
                var list = db.MyNote.FirstOrDefault(p => p.NoteId == id);
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
                var list = db.MyNote.FirstOrDefault(p => p.NoteId == id);
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
