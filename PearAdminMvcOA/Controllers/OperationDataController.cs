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
        public IHttpActionResult Operationinfo(int page, int limit)
        {
            using (OAEntities db = new OAEntities())
            {
                var List = db.Operation.OrderBy(n => n.Logid).Skip((page - 1) * limit).Take(limit).Select(n => new
                {
                    Logid = n.Logid,
                    OperationContent = n.OperationContent,
                    Userid = db.UserInfo.Where(p => p.UserId == n.Userid).FirstOrDefault().UserName,
                    host = n.host,
                    Operationtime = n.Operationtime
                }).ToList();
                var Count = db.Operation.Count();
                return Json(new { code = 0, msg = "", count = Count, data = List });
            }
        }

        [HttpGet]
        public IHttpActionResult OperationList(string UserName, int page, int limit)
        {
            if (UserName == null)
            {
                UserName = "";
            }
            using (OAEntities db = new OAEntities())
            {
                var List = db.Operation.Include("UserInfo").Where(p=>p.UserInfo.UserName.Contains(UserName)).OrderBy(n => n.Logid).Skip((page - 1) * limit).Take(limit).Select(n => new
                {
                    Logid = n.Logid,
                    OperationContent = n.OperationContent,
                    Userid = db.UserInfo.Where(p => p.UserId == n.Userid).FirstOrDefault().UserName,
                    host = n.host,
                    Operationtime = n.Operationtime
                }).ToList();
                var Count = db.Operation.Count();
                return Json(new { code = 0, msg = "", count = Count, data = List });
            }
        }


    }
}
