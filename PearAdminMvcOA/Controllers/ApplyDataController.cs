using PearAdminMvcOA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PearAdminMvcOA.Controllers
{
    public class ApplyDataController : ApiController
    {

        [HttpGet]
        public IHttpActionResult ApplyInfo()
        {
            using (OAEntities db = new OAEntities())
            {
                var List = db.Apply.Select(n => new
                {
                    ApplyID = n.ApplyID,
                    userid = db.UserInfo.FirstOrDefault(p => p.UserId == n.userid).UserName,
                    ApplicationContent = n.ApplicationContent,
                    ApplicationTime = n.ApplicationTime
                }).ToList();
                return Json(new { code = 0, msg = "", data = List });
            }
        }

        



    }
}
