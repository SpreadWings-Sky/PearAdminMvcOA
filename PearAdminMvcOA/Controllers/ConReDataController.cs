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
    public class ConReDataController : ApiController
    {
        [HttpGet]
        public IHttpActionResult ConTiList(int page = 1, int limit = 10)
        {
            using (OAEntities db = new OAEntities())
            {
                var list = db.ManRe.OrderBy(p => p.ManId).Skip((page - 1) * limit).Take(limit).Select(p => new
                {
                    ManId = p.ManId,
                    ManRoom = p.ManRoom,
                    ManTitle = p.ManTitle,
                    ManComm = p.ManComm,
                    StateTime = p.StateTime,
                    EndTime = p.EndTime,
                    UserId = db.UserInfo.FirstOrDefault(u => u.UserId == p.UserId).UserName,
                    CreaTime = p.CreaTime,
                }).ToList();
                var Count = db.ManRe.Count();
                return Json(new { code = 0, msg = "", count = Count, data = list });
            }

        }

        //名字+日期+情况查询
        [HttpGet]
        public IHttpActionResult ConTiList(int page, int limit, string UserName, string RoomName, string Time)
        {
            if (UserName == null && RoomName == null && Time == null)
            {
                return ConTiList(page, limit);
            }
            else
            {
                using (OAEntities db = new OAEntities())
                {
                    var list = db.ManRe.OrderBy(p => p.ManId).Skip((page - 1) * limit).Take(limit);

                    //预约人名字筛选
                    if (UserName != null && Time == null && RoomName == null)
                    {
                        var list2 = list.Include("UserInfo").Where(p => p.UserInfo.UserName.Contains(UserName)).Select(p => new
                        {
                            ManId = p.ManId,
                            ManRoom = p.ManRoom,
                            ManTitle = p.ManTitle,
                            ManComm = p.ManComm,
                            StateTime = p.StateTime,
                            EndTime = p.EndTime,
                            UserId = db.UserInfo.FirstOrDefault(u => u.UserId == p.UserId).UserName,
                            CreaTime = p.CreaTime,
                        });
                        var Count = db.ManRe.Count();
                        return Json(new { code = 0, msg = "", count = Count, data = list2.ToList() });

                    }
                    //房间名称筛选
                    if (UserName == null && Time == null && RoomName != null)
                    {
                        var list1 = list.Where(p => p.ManRoom.Contains(RoomName)).Select(p => new
                        {
                            ManId = p.ManId,
                            ManRoom = p.ManRoom,
                            ManTitle = p.ManTitle,
                            ManComm = p.ManComm,
                            StateTime = p.StateTime,
                            EndTime = p.EndTime,
                            UserId = db.UserInfo.FirstOrDefault(u => u.UserId == p.UserId).UserName,
                        });
                        var Count1 = db.ManRe.Count();
                        return Json(new { code = 0, msg = "", count = Count1, data = list1.ToList() });
                    }
                    //开始时间筛选
                    if (UserName == null && Time != null && RoomName == null)
                    {
                        var Start = Time.Split('~');
                        DateTime sTime = DateTime.Parse(Start[0]);
                        DateTime eTime = DateTime.Parse(Start[1]);
                        var list1 = list.Where(p => p.StateTime.CompareTo(sTime) >= 0 && p.EndTime.CompareTo(eTime) <= 0).Select(p => new
                        {
                            ManId = p.ManId,
                            ManRoom = p.ManRoom,
                            ManTitle = p.ManTitle,
                            ManComm = p.ManComm,
                            StateTime = p.StateTime,
                            EndTime = p.EndTime,
                            UserId = db.UserInfo.FirstOrDefault(u => u.UserId == p.UserId).UserName,
                            CreaTime = p.CreaTime,

                        });
                        var Count1 = db.ManRe.Count();
                        return Json(new { code = 0, msg = "", count = Count1, data = list1.ToList() });
                    }
                    //预约人名字+房间名字筛选
                    if (UserName != null && Time == null && RoomName != null)
                    {
                        var list1 = list.Include("UserInfo").Where(p => p.UserInfo.UserName.Contains(UserName) && p.ManRoom.Contains(RoomName)).Select(p => new
                        {
                            ManId = p.ManId,
                            ManRoom = p.ManRoom,
                            ManTitle = p.ManTitle,
                            ManComm = p.ManComm,
                            StateTime = p.StateTime,
                            EndTime = p.EndTime,
                            UserId = db.UserInfo.FirstOrDefault(u => u.UserId == p.UserId).UserName,
                            CreaTime = p.CreaTime,

                        });
                        var Count1 = db.ManRe.Count();
                        return Json(new { code = 0, msg = "", count = Count1, data = list1.ToList() });
                    }

                    //预约人名字+时间筛选
                    if (UserName != null && Time != null && RoomName == null)
                    {
                        var Start = Time.Split('~');
                        DateTime sTime = DateTime.Parse(Start[0]);
                        DateTime eTime = DateTime.Parse(Start[1]);
                        var list1 = list.Include("UserInfo").Where(p => p.UserInfo.UserName.Contains(UserName) && p.StateTime.CompareTo(sTime) >= 0 && p.EndTime.CompareTo(eTime) <= 0).Select(p => new
                        {
                            ManId = p.ManId,
                            ManRoom = p.ManRoom,
                            ManTitle = p.ManTitle,
                            ManComm = p.ManComm,
                            StateTime = p.StateTime,
                            EndTime = p.EndTime,
                            UserId = db.UserInfo.FirstOrDefault(u => u.UserId == p.UserId).UserName,
                            CreaTime = p.CreaTime,

                        });
                        var Count1 = db.ManRe.Count();
                        return Json(new { code = 0, msg = "", count = Count1, data = list1.ToList() });
                    }
                    //开始时间+房间名称筛选
                    if (UserName == null && Time != null && RoomName != null)
                    {
                        var Start = Time.Split('~');
                        DateTime sTime = DateTime.Parse(Start[0]);
                        DateTime eTime = DateTime.Parse(Start[1]);
                        var list1 = list.Where(p => p.ManRoom.Contains(RoomName) && p.StateTime.CompareTo(sTime) >= 0 && p.EndTime.CompareTo(eTime) <= 0).Select(p => new
                        {
                            ManId = p.ManId,
                            ManRoom = p.ManRoom,
                            ManTitle = p.ManTitle,
                            ManComm = p.ManComm,
                            StateTime = p.StateTime,
                            EndTime = p.EndTime,
                            UserId = db.UserInfo.FirstOrDefault(u => u.UserId == p.UserId).UserName,
                            CreaTime = p.CreaTime,

                        });
                        var Count1 = db.ManRe.Count();
                        return Json(new { code = 0, msg = "", count = Count1, data = list1.ToList() });
                    }
                    //预约人名字+开始时间+房间名字筛选
                    if (UserName != null && Time != null && RoomName != null)
                    {
                        var Start = Time.Split('~');
                        DateTime sTime = DateTime.Parse(Start[0]);
                        DateTime eTime = DateTime.Parse(Start[1]);
                        var list1 = list.Include("UserInfo").Where(p => p.UserInfo.UserName.Contains(UserName) && p.ManRoom.Contains(RoomName) && p.StateTime.CompareTo(sTime) >= 0 && p.EndTime.CompareTo(eTime) <= 0).Select(p => new
                        {
                            ManId = p.ManId,
                            ManRoom = p.ManRoom,
                            ManTitle = p.ManTitle,
                            ManComm = p.ManComm,
                            StateTime = p.StateTime,
                            EndTime = p.EndTime,
                            UserId = db.UserInfo.FirstOrDefault(u => u.UserId == p.UserId).UserName,
                            CreaTime = p.CreaTime,

                        });
                        var Count1 = db.ManRe.Count();
                        return Json(new { code = 0, msg = "", count = Count1, data = list1.ToList() });
                    }

                    return ConTiList(page, limit);
                }
            }


        }
        //修改
        [HttpPut]
        public IHttpActionResult ManModify(ManRe ma)
        {

            using (OAEntities db = new OAEntities())
            {
                ManRe lits = db.ManRe.FirstOrDefault(p => p.ManId == ma.ManId);
                string Name = ma.UserId.ToString();
                lits.ManRoom = ma.ManRoom;
                lits.ManTitle = ma.ManTitle;
                lits.ManComm = ma.ManComm;
                lits.StateTime = ma.StateTime;
                lits.EndTime = ma.EndTime;
                lits.UserId = db.UserInfo.FirstOrDefault(n => n.UserName == Name).UserId;
                bool stc = false;
                string mes = "修改失败";
                if (db.SaveChanges() > 0)
                {
                    stc = true;
                    mes = "修改成功";
                }
                return Json(new { success = stc, msg = mes });
            }
        }
        //删除
        [HttpGet]
        public IHttpActionResult ManDelete(int ManId)
        {
            using (OAEntities db = new OAEntities())
            {
                ManRe lits = db.ManRe.FirstOrDefault(p => p.ManId == ManId);
                db.ManRe.Remove(lits);
                bool stc = false;
                string mes = "删除失败";
                if (db.SaveChanges() > 0)
                {
                    stc = true;
                    mes = "删除成功";
                }
                return Json(new { success = stc, msg = mes });
            }
        }
    }
}
