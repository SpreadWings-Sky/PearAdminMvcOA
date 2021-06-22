using PearAdminMvcOA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PearAdminMvcOA.Controllers
{
    public class DepartController : ApiController
    {
        //查询
        [HttpGet]
        public IHttpActionResult Departinfo()
        {
            using (OAEntities db = new OAEntities())
            {
                var List = db.DepartInfo.Select(n => new
                {
                    DepartId = n.DepartId,
                    DepartName = n.DepartName,
                    UserName = db.UserInfo.FirstOrDefault(p => p.UserId == n.PrincipalUser).UserName,
                    ConnectTelNo = n.ConnectTelNo,
                    ConnectMobileTelNo = n.ConnectMobileTelNo,
                    Faxes = n.Faxes,
                    BranchName = db.BranchInfo.FirstOrDefault(p => p.BranchId == n.BranchId).BranchName
                }).ToList();
                return Json(new { code = 0, msg = "", data = List });

            }
        }
        //修改
        [HttpPut]
        public IHttpActionResult DepartModify(DepartInfo list)
        {

            using (OAEntities db = new OAEntities())
            {
                DepartInfo lits = db.DepartInfo.FirstOrDefault(p => p.DepartId == list.DepartId);
                lits.DepartName = list.DepartName;
                lits.PrincipalUser = list.PrincipalUser;
                lits.ConnectTelNo = list.ConnectTelNo;
                lits.ConnectMobileTelNo = list.ConnectMobileTelNo;
                lits.Faxes = list.Faxes;
                lits.BranchId = list.BranchId;
                bool  stc = false;
                string mes = "添加失败";
                if (db.SaveChanges() > 0)
                {
                    stc = true;
                    mes = "添加成功";
                }
                return Json(new { success = stc, msg = mes });
            }
        }
        //删除
        [HttpPut]
        public IHttpActionResult DepartDelete(int? DepartId)
        {
            using (OAEntities db = new OAEntities())
            {
                DepartInfo lits = db.DepartInfo.FirstOrDefault(p => p.DepartId == DepartId);
                db.DepartInfo.Remove(lits);
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
        public IHttpActionResult DepartAdd(DepartInfo d)
        {
            using (OAEntities db = new OAEntities())
            {
                db.DepartInfo.Add(d);
                int stc = 0;
                string mes = "添加失败";
                if (db.SaveChanges() > 0)
                {
                    stc = 200;
                    mes = "添加成功";
                }
                return Json(new { StatusCode = stc, message = mes });
            }
        }


    }
}
