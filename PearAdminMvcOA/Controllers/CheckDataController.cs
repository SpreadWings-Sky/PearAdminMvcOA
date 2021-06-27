using PearAdminMvcOA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PearAdminMvcOA.Controllers
{
    public static class DistinctByClass
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
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
                    UserId =db.UserInfo.FirstOrDefault(u=>u.UserId==p.UserId).UserName,
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
        public IHttpActionResult CheckList(int page, int limit, string userName, int? Desc,string Time)
        {
            if (userName == null && Desc == null&&Time==null)
            {
                return CheckList(page, limit);
            }
            else
            {
                using (OAEntities db = new OAEntities())
                {
                    var list = db.ManualSign.OrderBy(p => p.SignId).Skip((page - 1) * limit).Take(limit);
                    //总情况筛选
                    if (Desc != null&&Time==null && userName == null)
                    {
                       var  list1 = list.Where(p =>p.SignDesc==Desc).Select(p => new
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
                    if (Desc != null && Time == null&&userName!=null)
                    {
                        var list1 = list.Include("UserInfo").Where(p => p.SignDesc == Desc&&p.UserInfo.UserName.Contains(userName)).Select(p => new
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
                    if (Desc == null && Time != null&&userName == null)
                    {
                        var Start = Time.Split('~');
                        DateTime sTime = DateTime.Parse(Start[0]);
                        DateTime eTime = DateTime.Parse(Start[1]);
                        var list1 = list.Where(p => p.SignTime.CompareTo(sTime)>=0&& p.SignTime.CompareTo(eTime)<=0).Select(p => new
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
                    if (Desc == null && Time != null && userName != null)
                    {
                        var Start = Time.Split('~');
                        DateTime sTime = DateTime.Parse(Start[0]);
                        DateTime eTime = DateTime.Parse(Start[1]);
                        var list1 = list.Include("UserInfo").Where(p => p.UserInfo.UserName.Contains(userName)&& p.SignTime.CompareTo(sTime) >= 0 && p.SignTime.CompareTo(eTime) <= 0).Select(p => new
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
                    if (Desc != null && Time != null&&userName!=null)
                    {
                        var Start = Time.Split('~');
                        DateTime sTime = DateTime.Parse(Start[0]);
                        DateTime eTime = DateTime.Parse(Start[1]);
                        var list3 = list.Include("UserInfo").Where(p => p.UserInfo.UserName.Contains(userName)&& p.SignDesc == Desc&& p.SignTime.CompareTo(sTime) >= 0 && p.SignTime.CompareTo(eTime) <= 0).Select(p => new
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
                    if (Desc == null && Time == null && userName != null)
                    {
                        var list2 = list.Include("UserInfo").Where(p => p.UserInfo.UserName.Contains(userName)).Select(p => new
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
                    //时间+情况筛选
                    if (Desc != null && Time != null && userName == null)
                    {
                        var Start = Time.Split('~');
                        DateTime sTime = DateTime.Parse(Start[0]);
                        DateTime eTime = DateTime.Parse(Start[1]);
                        var list1 = list.Where(p => p.SignDesc == Desc && p.SignTime.CompareTo(sTime) >= 0 && p.SignTime.CompareTo(eTime) <= 0).Select(p => new
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


        //CountCheck 考勤统计页面的数据展示
        [HttpGet]
        public IHttpActionResult CheckList(string id, int page = 1, int limit = 10)
        {
            using (OAEntities db = new OAEntities())
            {
                var list = db.ManualSign.OrderBy(p => p.SignId).Skip((page - 1) * limit).Take(limit).Select(p => new
                {
                    SignId = p.SignId,
                    UserId = db.UserInfo.FirstOrDefault(u => u.UserId == p.UserId).UserName,
                    DepartId = db.DepartInfo.FirstOrDefault(d => d.DepartId == p.DepartId).DepartName,
                    SignTime = p.SignTime,
                    RoleId = db.RoleInfo.FirstOrDefault(r => r.RoleId == p.RoleId).RoleName,
                    Qian = db.ManualSign.Count(q => q.SignDesc == 1 && q.UserId == p.UserId),
                    Wei = db.ManualSign.Count(q => q.SignDesc == 2 && q.UserId == p.UserId),
                    Chi = db.ManualSign.Count(q => q.SignDesc == 3 && q.UserId == p.UserId),
                    Zao = db.ManualSign.Count(q => q.SignDesc == 4 && q.UserId == p.UserId),
                    Qing = db.ManualSign.Count(q => q.SignDesc == 5 && q.UserId == p.UserId),
                }).DistinctBy(n => n.UserId).ToList();
                var Count = list.Count();
                return Json(new { code = 0, msg = "", count = Count, data = list });
            }
        }

        //考勤统计页面的名字+日期+情况查询
        [HttpGet]
        public IHttpActionResult CheckList(string id,int page, int limit, string userName, string Time)
        {
            if (userName == null  && Time == null)
            {
                return CheckList(id,page,limit);
            }
            else
            {
                using (OAEntities db = new OAEntities())
                {
                    var list = db.ManualSign.OrderBy(p => p.SignId).Skip((page - 1) * limit).Take(limit);
                    //总时间筛选
                    if (Time != null && userName == null)
                    {
                        var Start = Time.Split('~');
                        DateTime sTime = DateTime.Parse(Start[0]);
                        DateTime eTime = DateTime.Parse(Start[1]);
                        var list3 = list.Where(p => p.SignTime.CompareTo(sTime) >= 0 && p.SignTime.CompareTo(eTime) <= 0).Select(p => new
                        {
                            SignId = p.SignId,
                            UserId = db.UserInfo.FirstOrDefault(u => u.UserId == p.UserId).UserName,
                            DepartId = db.DepartInfo.FirstOrDefault(d => d.DepartId == p.DepartId).DepartName,
                            SignTime = p.SignTime,
                            RoleId = db.RoleInfo.FirstOrDefault(r => r.RoleId == p.RoleId).RoleName,
                            Qian = db.ManualSign.Count(q => q.SignDesc == 1 && q.UserId == p.UserId),
                            Wei = db.ManualSign.Count(q => q.SignDesc == 2 && q.UserId == p.UserId),
                            Chi = db.ManualSign.Count(q => q.SignDesc == 3 && q.UserId == p.UserId),
                            Zao = db.ManualSign.Count(q => q.SignDesc == 4 && q.UserId == p.UserId),
                            Qing = db.ManualSign.Count(q => q.SignDesc == 5 && q.UserId == p.UserId),
                        }).DistinctBy(n => n.UserId);
                        var Count3 = list3.Count();
                        return Json(new { code = 0, msg = "", count = Count3, data = list3.ToList() });
                    }
                    //时间+名字筛选
                    if ( Time != null && userName != null)
                    {
                        var Start = Time.Split('~');
                        DateTime sTime = DateTime.Parse(Start[0]);
                        DateTime eTime = DateTime.Parse(Start[1]);
                        var list4 = list.Include("UserInfo").Where(p => p.UserInfo.UserName.Contains(userName) && p.SignTime.CompareTo(sTime) >= 0 && p.SignTime.CompareTo(eTime) <= 0).Select(p => new
                        {
                            SignId = p.SignId,
                            UserId = db.UserInfo.FirstOrDefault(u => u.UserId == p.UserId).UserName,
                            DepartId = db.DepartInfo.FirstOrDefault(d => d.DepartId == p.DepartId).DepartName,
                            SignTime = p.SignTime,
                            RoleId = db.RoleInfo.FirstOrDefault(r => r.RoleId == p.RoleId).RoleName,
                            Qian = db.ManualSign.Count(q => q.SignDesc == 1 && q.UserId == p.UserId),
                            Wei = db.ManualSign.Count(q => q.SignDesc == 2 && q.UserId == p.UserId),
                            Chi = db.ManualSign.Count(q => q.SignDesc == 3 && q.UserId == p.UserId),
                            Zao = db.ManualSign.Count(q => q.SignDesc == 4 && q.UserId == p.UserId),
                            Qing = db.ManualSign.Count(q => q.SignDesc == 5 && q.UserId == p.UserId),
                        }).DistinctBy(n => n.UserId);
                        var Count4 = list4.Count();
                        return Json(new { code = 0, msg = "", count = Count4, data = list4.ToList() });
                    }

                    //名字筛选
                    if ( Time == null && userName != null)
                    {
                        var list6 = list.Include("UserInfo").Where(p => p.UserInfo.UserName.Contains(userName)).Select(p => new
                        {
                            SignId = p.SignId,
                            UserId = db.UserInfo.FirstOrDefault(u => u.UserId == p.UserId).UserName,
                            DepartId = db.DepartInfo.FirstOrDefault(d => d.DepartId == p.DepartId).DepartName,
                            SignTime = p.SignTime,
                            RoleId = db.RoleInfo.FirstOrDefault(r => r.RoleId == p.RoleId).RoleName,
                            Qian = db.ManualSign.Count(q => q.SignDesc == 1 && q.UserId == p.UserId),
                            Wei = db.ManualSign.Count(q => q.SignDesc == 2 && q.UserId == p.UserId),
                            Chi = db.ManualSign.Count(q => q.SignDesc == 3 && q.UserId == p.UserId),
                            Zao = db.ManualSign.Count(q => q.SignDesc == 4 && q.UserId == p.UserId),
                            Qing = db.ManualSign.Count(q => q.SignDesc == 5 && q.UserId == p.UserId),
                        }).DistinctBy(n => n.UserId);
                        var Count6 = list6.Count();
                        return Json(new { code = 0, msg = "", count = Count6, data = list6.ToList() });

                    }
                    return CheckList(page, limit);
                }
            }


        }

    }
}
