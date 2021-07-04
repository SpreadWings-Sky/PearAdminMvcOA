using PearAdminMvcOA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PearAdminMvcOA.Controllers
{
    public class ConRoomDataController : ApiController
    {
        [HttpGet]
        public IHttpActionResult ConRoList(int page = 1, int limit = 10)
        {
            using (OAEntities db = new OAEntities())
            {

                var list = db.ConferenceRoom.OrderBy(p => p.RoomId).Skip((page - 1) * limit).Take(limit).Select(p => new
                {
                    RoomName=p.RoomName,
                    RoomLocation=p.RoomLocation,
                    Capacity=p.Capacity,
                    State=db.ConSta.FirstOrDefault(s=>s.ConStaId==p.State).ConStaName,
                    CreationTime = p.CreationTime,
                }).ToList();
                var Count = db.ConferenceRoom.Count();
                return Json(new { code = 0, msg = "", count = Count, data = list });
            }

        }
        //名字+日期+情况查询
        [HttpGet]
        public IHttpActionResult ConRoList(int page, int limit, string RoomName, int? State, string Time)
        {
            if (RoomName == null && State == null && Time == null)
            {
                return ConRoList(page, limit);
            }
            else
            {
                using (OAEntities db = new OAEntities())
                {
                    var list = db.ConferenceRoom.OrderBy(p => p.RoomId).Skip((page - 1) * limit).Take(limit);

                    //名字筛选
                    if (State == null && Time == null && RoomName != null)
                    {
                        var list2 = list.Where(p => p.RoomName.Contains(RoomName)).Select(p => new
                        {
                            RoomName = p.RoomName,
                            RoomLocation = p.RoomLocation,
                            Capacity = p.Capacity,
                            State = db.ConSta.FirstOrDefault(s => s.ConStaId == p.State).ConStaName,
                            CreationTime = p.CreationTime,
                        });
                        var Count = db.ManualSign.Count();
                        return Json(new { code = 0, msg = "", count = Count, data = list2.ToList()});

                    }
                    //总状态筛选
                    if (State != null && Time == null && RoomName == null)
                    {
                        var list1 = list.Where(p => p.State == State).Select(p => new
                        {
                            RoomName = p.RoomName,
                            RoomLocation = p.RoomLocation,
                            Capacity = p.Capacity,
                            State = db.ConSta.FirstOrDefault(s => s.ConStaId == p.State).ConStaName,
                            CreationTime = p.CreationTime,
                        });
                        var Count1 = db.ManualSign.Count();
                        return Json(new { code = 0, msg = "", count = Count1, data = list1.ToList() });
                    }
                    //总时间筛选
                    if (State == null && Time != null && RoomName == null)
                    {
                        var Start = Time.Split('~');
                        DateTime sTime = DateTime.Parse(Start[0]);
                        DateTime eTime = DateTime.Parse(Start[1]);
                        var list1 = list.Where(p => p.CreationTime.CompareTo(sTime) >= 0 && p.CreationTime.CompareTo(eTime) <= 0).Select(p => new
                        {
                            RoomName = p.RoomName,
                            RoomLocation = p.RoomLocation,
                            Capacity = p.Capacity,
                            State = db.ConSta.FirstOrDefault(s => s.ConStaId == p.State).ConStaName,
                            CreationTime = p.CreationTime,
                        });
                        var Count1 = db.ManualSign.Count();
                        return Json(new { code = 0, msg = "", count = Count1, data = list1.ToList() });
                    }
                    //状态+名字筛选
                    if (State != null && Time == null && RoomName != null)
                    {
                        var list1 = list.Where(p => p.State == State && p.RoomName.Contains(RoomName)).Select(p => new
                        {
                            RoomName = p.RoomName,
                            RoomLocation = p.RoomLocation,
                            Capacity = p.Capacity,
                            State = db.ConSta.FirstOrDefault(s => s.ConStaId == p.State).ConStaName,
                            CreationTime = p.CreationTime,
                        });
                        var Count1 = db.ManualSign.Count();
                        return Json(new { code = 0, msg = "", count = Count1, data = list1.ToList() });
                    }
                   
                    //时间+名字筛选
                    if (State == null && Time != null && RoomName != null)
                    {
                        var Start = Time.Split('~');
                        DateTime sTime = DateTime.Parse(Start[0]);
                        DateTime eTime = DateTime.Parse(Start[1]);
                        var list1 = list.Where(p => p.RoomName.Contains(RoomName) && p.CreationTime.CompareTo(sTime) >= 0 && p.CreationTime.CompareTo(eTime) <= 0).Select(p => new
                        {
                            RoomName = p.RoomName,
                            RoomLocation = p.RoomLocation,
                            Capacity = p.Capacity,
                            State = db.ConSta.FirstOrDefault(s => s.ConStaId == p.State).ConStaName,
                            CreationTime = p.CreationTime,
                        });
                        var Count1 = db.ManualSign.Count();
                        return Json(new { code = 0, msg = "", count = Count1, data = list1.ToList() });
                    }
                    //时间+状态筛选
                    if (State == null && Time != null && RoomName != null)
                    {
                        var Start = Time.Split('~');
                        DateTime sTime = DateTime.Parse(Start[0]);
                        DateTime eTime = DateTime.Parse(Start[1]);
                        var list1 = list.Where(p => p.State== State && p.CreationTime.CompareTo(sTime) >= 0 && p.CreationTime.CompareTo(eTime) <= 0).Select(p => new
                        {
                            RoomName = p.RoomName,
                            RoomLocation = p.RoomLocation,
                            Capacity = p.Capacity,
                            State = db.ConSta.FirstOrDefault(s => s.ConStaId == p.State).ConStaName,
                            CreationTime = p.CreationTime,
                        });
                        var Count1 = db.ManualSign.Count();
                        return Json(new { code = 0, msg = "", count = Count1, data = list1.ToList() });
                    }
                    //情况+时间+名字筛选
                    if (State != null && Time != null && RoomName != null)
                    {
                        var Start = Time.Split('~');
                        DateTime sTime = DateTime.Parse(Start[0]);
                        DateTime eTime = DateTime.Parse(Start[1]);
                        var list3 = list.Where(p => p.RoomName.Contains(RoomName) && p.State == State && p.CreationTime.CompareTo(sTime) >= 0 && p.CreationTime.CompareTo(eTime) <= 0).Select(p => new
                        {
                            RoomName = p.RoomName,
                            RoomLocation = p.RoomLocation,
                            Capacity = p.Capacity,
                            State = db.ConSta.FirstOrDefault(s => s.ConStaId == p.State).ConStaName,
                            CreationTime = p.CreationTime,
                        });
                        var Count2 = db.ManualSign.Count();
                        return Json(new { code = 0, msg = "", count = Count2, data = list3.ToList() });
                    }

                    return ConRoList(page, limit);
                }
            }


        }
    }
}
