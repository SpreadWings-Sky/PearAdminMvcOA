using PearAdminMvcOA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PearAdminMvcOA.Controllers
{
    public class HRRoleController : ApiController
    {
        //查询日程列表
        [HttpGet]
        public IHttpActionResult RoleList(int page, int limit)
        {
            using (OAEntities db = new OAEntities())
            {
                var list = db.RoleInfo.OrderBy(n => n.RoleId).Skip((page - 1) * limit).Take(limit).ToList();
                var Count = db.RoleInfo.Count();
                return Json(new { code = 0, msg = "", count = Count, data = list });
            }
        }
        [HttpGet]
        public IHttpActionResult RoleList( int page, int limit, string realName)
        {
            if (realName == null)
            {
                return RoleList(page, limit);
            }
            using (OAEntities db = new OAEntities())
            {
                var list = db.RoleInfo.Where(p => p.RoleName.Contains(realName)).OrderBy(n => n.RoleId).Skip((page - 1) * limit).Take(limit).ToList();
                var Count = db.RoleInfo.Where(p => p.RoleName.Contains(realName)).Count();
                return Json(new { code = 0, msg = "", count = Count, data = list });
            }
        }
        //查询单个日程
        [HttpGet]
        public IHttpActionResult Role(int? id)
        {
            using (OAEntities db = new OAEntities())
            {
                var list = db.RoleInfo.FirstOrDefault(p => p.RoleId == id);
                return Json(new { code = 0, msg = "", data = list });
            }
        }
        //添加
        [HttpPost]
        public IHttpActionResult Add(RoleInfo R)
        {
            using (OAEntities db = new OAEntities())
            {
                db.RoleInfo.Add(R);
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
        public IHttpActionResult Edit(RoleInfo R)
        {
            using (OAEntities db = new OAEntities())
            {
                db.Entry(R).State = System.Data.Entity.EntityState.Modified;
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
                var list = db.RoleInfo.FirstOrDefault(p => p.RoleId == id);
                db.RoleInfo.Remove(list);
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
