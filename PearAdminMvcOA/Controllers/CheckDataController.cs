using PearAdminMvcOA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PearAdminMvcOA.Controllers
{
    public class CheckDataController : ApiController
    {
        //请求列表
        //考勤数据初始展示
        [HttpGet]
        public IHttpActionResult CheckList(int page = 1, int limit = 10)
        {
            using (OAEntities db = new OAEntities())
            {
                var list = db.ManualSign.OrderBy(p => p.SignId).Skip((page - 1) * limit).Take(limit).Select(p => new
                {
                    SignId = p.SignId,
                    UserId = db.UserInfo.FirstOrDefault(u => u.UserId == p.UserId).UserName,
                    SignTime = p.SignTime,
                    SignDesc = db.Remarks.FirstOrDefault(e => e.RemarksID == p.SignDesc).RemaName,
                    SignText = p.SignText,
                    RoleId = db.RoleInfo.FirstOrDefault(r => r.RoleId == p.RoleId).RoleName,
                }).ToList();
                var Count = db.ManualSign.Count();
                return Json(new { code = 0, msg = "", count = Count, data = list });
            }
        }
        //名字+日期+情况查询
        [HttpGet]
        public IHttpActionResult CheckList(int page, int limit, string UserName, int? Desc, string Time)
        {
            if (UserName == null && Desc == null && Time == null)
            {
                return CheckList(page, limit);
            }
            else
            {
                using (OAEntities db = new OAEntities())
                {
                    var list = db.ManualSign.OrderBy(p => p.SignId).Skip((page - 1) * limit).Take(limit);
                    //总情况筛选
                    if (Desc != null && Time == null && UserName == null)
                    {
                        var list1 = list.Where(p => p.SignDesc == Desc).Select(p => new
                        {
                            SignId = p.SignId,
                            UserId = db.UserInfo.FirstOrDefault(u => u.UserId == p.UserId).UserName,
                            SignTime = p.SignTime,
                            SignDesc = db.Remarks.FirstOrDefault(e => e.RemarksID == p.SignDesc).RemaName,
                            SignText = p.SignText,
                            RoleId = db.RoleInfo.FirstOrDefault(r => r.RoleId == p.RoleId).RoleName,
                        });
                        var Count1 = db.ManualSign.Count();
                        return Json(new { code = 0, msg = "", count = Count1, data = list1.ToList() });
                    }
                    //情况+名字筛选
                    if (Desc != null && Time == null && UserName != null)
                    {
                        var list1 = list.Where(p => p.SignDesc == Desc && p.UserName.Contains(userName)).Select(p => new
                        {
                            SignId = p.SignId,
                            UserId = db.UserInfo.FirstOrDefault(u => u.UserId == p.UserId).UserName,
                            SignTime = p.SignTime,
                            SignDesc = db.Remarks.FirstOrDefault(e => e.RemarksID == p.SignDesc).RemaName,
                            SignText = p.SignText,
                            RoleId = db.RoleInfo.FirstOrDefault(r => r.RoleId == p.RoleId).RoleName,
                        });
                        var Count1 = db.ManualSign.Count();
                        return Json(new { code = 0, msg = "", count = Count1, data = list1.ToList() });
                    }
                    //总时间筛选
                    if (Desc == null && Time != null && UserName == null)
                    {
                        var Start = Time.Split('~');
                        DateTime sTime = DateTime.Parse(Start[0]);
                        DateTime eTime = DateTime.Parse(Start[1]);
                        var list1 = list.Where(p => p.SignTime.CompareTo(sTime) >= 0 && p.SignTime.CompareTo(eTime) <= 0).Select(p => new
                        {
                            SignId = p.SignId,
                            UserId = db.UserInfo.FirstOrDefault(u => u.UserId == p.UserId).UserName,
                            SignTime = p.SignTime,
                            SignDesc = db.Remarks.FirstOrDefault(e => e.RemarksID == p.SignDesc).RemaName,
                            SignText = p.SignText,
                            RoleId = db.RoleInfo.FirstOrDefault(r => r.RoleId == p.RoleId).RoleName,
                        });
                        var Count1 = db.ManualSign.Count();
                        return Json(new { code = 0, msg = "", count = Count1, data = list1.ToList() });
                    }
                    //时间+名字筛选
                    if (Desc == null && Time != null && UserName != null)
                    {
                        var Start = Time.Split('~');
                        DateTime sTime = DateTime.Parse(Start[0]);
                        DateTime eTime = DateTime.Parse(Start[1]);
                        var list1 = list.Where(p => p.userId.Contains(userId) && p.SignTime.CompareTo(sTime) >= 0 && p.SignTime.CompareTo(eTime) <= 0).Select(p => new
                        {
                            SignId = p.SignId,
                            UserId = db.UserInfo.FirstOrDefault(u => u.UserId == p.UserId).UserName,
                            SignTime = p.SignTime,
                            SignDesc = db.Remarks.FirstOrDefault(e => e.RemarksID == p.SignDesc).RemaName,
                            SignText = p.SignText,
                            RoleId = db.RoleInfo.FirstOrDefault(r => r.RoleId == p.RoleId).RoleName,
                        });
                        var Count1 = db.ManualSign.Count();
                        return Json(new { code = 0, msg = "", count = Count1, data = list1.ToList() });
                    }
                    //情况+时间+名字筛选
                    if (Desc != null && Time != null && UserName != null)
                    {
                        var Start = Time.Split('~');
                        DateTime sTime = DateTime.Parse(Start[0]);
                        DateTime eTime = DateTime.Parse(Start[1]);
                        var list3 = list.Where(p => p.UserName.Contains(userName) && p.SignDesc == Desc && p.SignTime.CompareTo(sTime) >= 0 && p.SignTime.CompareTo(eTime) <= 0).Select(p => new
                        {
                            SignId = p.SignId,
                            UserId = db.UserInfo.FirstOrDefault(u => u.UserId == p.UserId).UserName,
                            SignTime = p.SignTime,
                            SignDesc = db.Remarks.FirstOrDefault(e => e.RemarksID == p.SignDesc).RemaName,
                            SignText = p.SignText,
                            RoleId = db.RoleInfo.FirstOrDefault(r => r.RoleId == p.RoleId).RoleName,
                        });
                        var Count2 = db.ManualSign.Count();
                        return Json(new { code = 0, msg = "", count = Count2, data = list3.ToList() });
                    }

                    //名字筛选
                    if (Desc == null && Time == null && UserName != null)
                    {
                        var list2 = db.UserInfo.Where(p => p.UserName.Contains(UserName)).Select(p => new
                        {
                            SignId = p.SignId,
                            UserId = db.UserInfo.FirstOrDefault(u => u.UserId == p.UserId).UserName,
                            SignTime = p.SignTime,
                            SignDesc = db.Remarks.FirstOrDefault(e => e.RemarksID == p.SignDesc).RemaName,
                            SignText = p.SignText,
                            RoleId = db.RoleInfo.FirstOrDefault(r => r.RoleId == p.RoleId).RoleName,
                        });
                        var Count = db.ManualSign.Count();
                        return Json(new { code = 0, msg = "", count = Count, data = list2.ToList() });

                    }
                    return CheckList(page, limit);
                }
            }

        }


        //删除功能
        [HttpGet]
        public IHttpActionResult Delete(int? SignId)
        {
            if (SignId != null)
            {
                using (OAEntities db = new OAEntities())
                {
                    var User = db.ManualSign.FirstOrDefault(n => n.SignId == SignId);
                    db.ManualSign.Remove(User);
                    bool result = db.SaveChanges() > 0;
                    return result ? Json(new { success = true, msg = "成功" }) : Json(new { success = false, msg = "失败" });
                }
            }
            return Json(new { success = false, msg = "失败" });
        }

        [HttpPut]
        //修改功能
        public IHttpActionResult EditSign(ManualSign user)
        {
            if (user != null && ModelState.IsValid)
            {
                using (OAEntities db = new OAEntities())
                {
                    var us = db.ManualSign.FirstOrDefault(n => n.SignId == user.SignId);
                    us.SignDesc = user.SignDesc;
                    us.SignText = user.SignText;
                    if (db.SaveChanges() > 0)
                    {
                        return Json(new { success = true, msg = "更新成功" });
                    }
                    return Json(new { success = false, msg = "数据有误" });
                }
            }
            return Json(new { success = false, msg = "数据有误" });
        }
    }
}
