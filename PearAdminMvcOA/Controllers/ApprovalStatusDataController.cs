using PearAdminMvcOA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PearAdminMvcOA.Controllers
{
    public class ApprovalStatusDataController : ApiController
    {
        [HttpGet]
        public IHttpActionResult ApprovalStatusInfo()
        {
            using (OAEntities db = new OAEntities())
            {
                var List = db.ApprovalStatus.Select(n => new
                {
                    ApplicantUserid = db.UserInfo.FirstOrDefault(p=>p.UserId==n.ApplicantUserid).UserName,
                    Userid = db.UserInfo.FirstOrDefault(p => p.UserId == n.Userid).UserName,
                    Approvaltime = n.Approvaltime,
                    Approvalcontent = n.Approvalcontent,
                    Approvalstatus = n.Approvalstatus1 == "1" ? "通过" : "未通过",
                }).ToList();
                return Json(new { code = 0, msg = "", data = List });

            }
        }


        [HttpPut]
        public IHttpActionResult Apply_1(int id)
        {
            using (OAEntities db = new OAEntities())
            {
                int Userid = 8;
                Apply a = db.Apply.FirstOrDefault(p=>p.ApplyID==id);
                ApprovalStatus ast = new ApprovalStatus();
                ast.ApplicantUserid = Userid;
                ast.Userid = a.userid;
                ast.Approvaltime = a.ApplicationTime;
                ast.Approvalcontent = a.ApplicationContent;
                ast.Approvalstatus1 = "1";
                db.ApprovalStatus.Add(ast);
                db.Apply.Remove(a);
                bool stc = false;
                string mes = "通过失败";
                if (db.SaveChanges() > 0)
                {
                    stc = true;
                    mes = "通过成功";
                }
                return Json(new { success = stc, msg = mes });
            }
        }

        [HttpPost]
        public IHttpActionResult Apply_2(int id)
        {
            using (OAEntities db = new OAEntities())
            {
                int Userid = 8;
                Apply a = db.Apply.FirstOrDefault(p => p.ApplyID == id);
                ApprovalStatus ast = new ApprovalStatus();
                ast.ApplicantUserid = Userid;
                ast.Userid = a.userid;
                ast.Approvaltime = a.ApplicationTime;
                ast.Approvalcontent = a.ApplicationContent;
                ast.Approvalstatus1 = "0";
                db.ApprovalStatus.Add(ast);
                db.Apply.Remove(a);
                bool stc = false;
                string mes = "不通过失败";
                if (db.SaveChanges() > 0)
                {
                    stc = true;
                    mes = "不通过成功";
                }
                return Json(new { success = stc, msg = mes });
            }
        }


    }
}
