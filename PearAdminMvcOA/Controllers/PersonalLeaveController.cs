using PearAdminMvcOA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PearAdminMvcOA.Controllers
{
    public class PersonalLeaveController : ApiController
    {
        [HttpPost]
        public IHttpActionResult ApplyLeave(ManualSign man)
        {
            using (OAEntities db = new OAEntities())
            {
                db.ManualSign.Add(man);
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
