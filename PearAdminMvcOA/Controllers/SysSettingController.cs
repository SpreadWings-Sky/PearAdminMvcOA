using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PearAdminMvcOA.Models;
using PearAdminMvcOA.Tools;

namespace PearAdminMvcOA.Controllers
{
    [Authorize]
    public class SysSettingController : ApiController
    {
        //定义枚举
        enum MeunType
        {
            Menu, Page
        }
        //定义菜单类
        class PearMeun
        {
            public long id { get; set; }
            public string title { get; set; }
            public string icon { get; set; }
            public MeunType type { get; set; } = MeunType.Page;
            public string openType { get; set; } = "_iframe";
            public string href { get; set; }
            public List<PearMeun> children { get; set; }
        }
        /// <summary>
        ///菜单数据
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        public IHttpActionResult PearMenuData()
        {
            using (OAEntities db = new OAEntities())
            {
                //获取全部菜单
                var Menus = db.Menu.ToList();
                //找到一级菜单
                var data = Menus.Where(m => m.ParentMenuId == null).Select(m => new PearMeun
                {
                    id = m.Id,
                    href = m.Url,
                    title = m.MenuText,
                    icon = m.IcoString,
                    type = m.MenuTypeId == 1 ? MeunType.Menu : MeunType.Page
                }).ToList();
                //查找二级菜单
                for (int i = 0; i < data.Count(); i++)
                {
                    var Menu = Menus.Where(m => m.ParentMenuId == data[i].id).Select(m => new PearMeun
                    {
                        id = m.Id,
                        href = m.Url,
                        title = m.MenuText,
                        icon = m.IcoString,
                        type = m.MenuTypeId == 1 ? MeunType.Menu : MeunType.Page
                    }).ToList();
                    data[i].children = Menu;
                }
                return Json(data.ToList());
            }
        }
    }
}
