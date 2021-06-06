using PearAdminMvcOA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PearAdminMvcOA.Tools;

namespace PearAdminMvcOA.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        //加载页面
        public ActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// 重载一个登录方法
        /// </summary>
        /// <param name="name">用户输入信息</param>
        /// <returns>验证状态</returns>
        [HttpPost]
        public ActionResult Login(string WorkId, string pwd)
        {
            if (!string.IsNullOrEmpty(WorkId) && !string.IsNullOrEmpty(pwd))
            {
                pwd = CommonBLL.HashEncoding(pwd);
                using (OAEntities db = new OAEntities())
                {
                    UserInfo us = db.UserInfo.FirstOrDefault(n => n.WordId == WorkId && n.PassWord == pwd);
                    return us == null ? Json(new { code = 500 }) : Json(new { code = 200 });
                }
            }
            else return Json(new { code = 500 });
        }
    }
}