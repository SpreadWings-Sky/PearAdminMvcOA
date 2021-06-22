using PearAdminMvcOA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PearAdminMvcOA.Controllers
{
    public class ConferenceRoomDataController : ApiController
    {
        //查询
        [HttpGet]
        public IHttpActionResult Roominfo()
        {
            using (OAEntities db = new OAEntities())
            {
                var List = db.ConferenceRoom.Select(n => new
                {
                    RoomId = n.RoomId,
                    RoomName = n.RoomName,
                    RoomLocation = n.RoomLocation,
                    Capacity = n.Capacity,
                    State = n.State==1?"使用中":"预约中",
                    CreationTime = n.CreationTime
                }).ToList();
                return Json(new { code = 0, msg = "", data = List });

            }
        }
        //修改
        [HttpPut]
        public IHttpActionResult RoomModify(ConferenceRoom list)
        {

            using (OAEntities db = new OAEntities())
            {
                ConferenceRoom lits = db.ConferenceRoom.FirstOrDefault(p => p.RoomId == list.RoomId);
                lits.RoomName = list.RoomName;
                lits.RoomLocation = list.RoomLocation;
                lits.Capacity = list.Capacity;
                lits.State = list.State;
                lits.CreationTime = list.CreationTime;
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
        [HttpPut]
        public IHttpActionResult RoomDelete(int? RoomId)
        {
            using (OAEntities db = new OAEntities())
            {
                ConferenceRoom lits = db.ConferenceRoom.FirstOrDefault(p => p.RoomId == RoomId);
                db.ConferenceRoom.Remove(lits);
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

        //添加
        [HttpPost]
        public IHttpActionResult RoomAdd(ConferenceRoom d)
        {
            using (OAEntities db = new OAEntities())
            {
                db.ConferenceRoom.Add(d);
                bool stc = false;
                string mes = "添加失败";
                if (db.SaveChanges() > 0)
                {
                    stc = true;
                    mes = "添加成功";
                }
                return Json(new { success = stc, msg = mes });
            }
        }

    }
}
