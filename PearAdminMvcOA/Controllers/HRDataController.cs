using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PearAdminMvcOA.Models;

namespace PearAdminMvcOA.Controllers
{
    public class HRDataController : ApiController
    {
        [HttpGet]
        public IHttpActionResult UserList(int page = 1, int limit = 10)
        {
            using (OAEntities db = new OAEntities())
            {
                List<UserInfo> list = db.UserInfo.OrderBy(n => n.UserId).Skip((page - 1) * limit).Take(limit).ToList();
                var Count = db.UserInfo.Count();
                return Json(new { code = 0, msg = "", count = Count, data = list });
            }
        }
    }
}
