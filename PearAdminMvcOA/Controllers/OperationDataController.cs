using PearAdminMvcOA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PearAdminMvcOA.Controllers
{
    public class OperationDataController : ApiController
    {
        //查询
        [HttpGet]
        public IHttpActionResult Operationinfo()
        {
            using (OAEntities db = new OAEntities())
            {
                var List = db.Operation.Select(n => new
                {
                    Logid = n.Logid,
                    OperationContent = n.OperationContent,
                    Userid = db.UserInfo.Where(p => p.UserId == n.Userid).FirstOrDefault().UserName,
                    host = n.host,
                    Operationtime = n.Operationtime
                }).ToList();
                return Json(new { code = 0, msg = "", data = List });
            }
        }

    }
}
