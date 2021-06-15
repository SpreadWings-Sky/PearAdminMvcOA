using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Script.Serialization;

namespace PearAdminMvcOA.Tools
{
    public class PearCard:IDisposable
    {
        public HtmlHelper HtmlHelper { get; set; }
        public PearCard(HtmlHelper healper)
        {
            HtmlHelper = healper;
        }
        public void Dispose()
        {
            HtmlHelperTools.EndPearCard(HtmlHelper);   
        }
    }
    public static class HtmlHelperTools
    {
        /**
         * 编写组件的思路与想法实现
         *  1. UIHint
         * 一般情况下 UIHint处理用户的单属性数据
         *
         * 2.扩展htmlhelper
         *   准备要生成的html素材
         *   考虑生成的代码段是否可以容纳其他组件
         *    1.如果允许放入其他组件或html内容则需要写两个方法。一个用来生成开始部分的代码段，另一个用来生成结束代码段
         *    2.如果不需要放入其他组件 则直接输出全部html内容即可
         *
         */


        public static PearCard BeginPearCard(this HtmlHelper htmlHelper,string title )
        {
            var html = $"<div class=\"layui-card\">" +
                       $"<div class=\"layui-card-header\">{title}</div>" +
                       $"<div class=\"layui-card-body\">"  ;

            htmlHelper.ViewContext.Writer.Write(html);
            return new PearCard(htmlHelper);
        }
        public static void EndPearCard(this HtmlHelper htmlHelper  )
        {

            htmlHelper.ViewContext.Writer.Write(@"</div></div>");
        }


        public static HtmlString PearIcon(this HtmlHelper htmlHelper, string icon,string color="",int size=12)
        {
            return new HtmlString($"<i class=\"layui-icon {icon}\" style='font-size:{size}px;{(string.IsNullOrEmpty(color)?"":"color:"+ color)}'></i>   ");
        }

        public static HtmlString PearSelect(this HtmlHelper helper, string title, string selectName, SelectList list, string @class = "layui-input-block")
        {
            var items = "";
            foreach (var VARIABLE in list)
            {
                items += $"<option {(VARIABLE.Selected ? "selected" : String.Empty)}  value='{VARIABLE.Value}'>{VARIABLE.Text}</option>";
            }
            var html = $@" <div class='layui-form-item'><label class='layui-form-label'>{title}</label><div  class='{@class}'><select id='{selectName}' name='{selectName}'>{items}</select></div></div>";
            return new HtmlString(html);
        }
        //public static HtmlString PearSelectByDictionary(this HtmlHelper helper, string title, string selectName,string val, string dicGroupName, string @class = "layui-input-block")
        //{
        //    BaseManager baseMgr = new BaseManager();
        //    var vals = baseMgr.GetDictionariesByGroupName(dicGroupName);

        //    var items = "";
        //    foreach (var VARIABLE in vals)
        //    {
        //        items += $"<option {(VARIABLE.Value == val ? "selected" : String.Empty)}  value='{VARIABLE.Value}'>{VARIABLE.GroupName}</option>";
        //    }
        //    var html = $@" <div class='layui-form-item'><label class='layui-form-label'>{title}</label><div  class='{@class}'><select id='{selectName}' name='{selectName}'>{items}</select></div></div>";
        //    return new HtmlString(html);
        //}
        public static MvcForm BeginPearForm(this HtmlHelper helper,string id=null)
        {
            var html = $"<form class=\"layui-form\"  id=\"{(string.IsNullOrEmpty(id)?"form":id)}\" >";

            helper.ViewContext.Writer.Write(html);

            return new MvcForm(helper.ViewContext);
        }


        public static HtmlString PearTable(this HtmlHelper helper, string tableId="tbl", string url="",List<PearTableCell> tableCells=null)
        {
            var html = @"<table id='" + tableId + "'></table>";


            JavaScriptSerializer scriptSerializer1 = new JavaScriptSerializer();
            var cols = scriptSerializer1.Serialize(tableCells);
 
            return new HtmlString(html);
        }
        /// <summary>
        /// 弹出窗口
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="urlOrSearch">Open用url或content 抽屉用search</param>
        /// <param name="width">宽度可以使用百分比或者px</param>
        /// <param name="type">弹出类型，可以是modal和抽屉</param>
        /// <returns></returns>
        public static HtmlString PearOpen(this HtmlHelper helper, string urlOrSearch, string title = "信息", string width="500px",string height="",bool isFlush = true, PearOpenType type = PearOpenType.Modal)
        {
            var area = "";
            if (string.IsNullOrEmpty(height))
                area = $"'width'";
            else
            {
                area = $" ['{width}', '{height}']";
            }

            var flushStr = isFlush ? "layui.layer.iframeAuto(index);" : "";
            
            var mhtml = "";
            if (type == PearOpenType.Modal)
            {
                mhtml = @"layui.layer.open({
                    type:2,
                    content: "+urlOrSearch+@" ,
                    area:"+area+$@",
                    title:'"+title+$@"',
                    success: function(layer, index) {{
                       "+ flushStr +$@"
                    }}
                    ,end : function(){{
                        if(openEndFunc) {{
                            openEndFunc();
                        }}
                    }}
                }});";
            }
            else
            {  
               mhtml = @"tools.drawer open({
                    direction: '"+(type == PearOpenType.RightBox? "right":"left")+@"', 
                    dom:  "+urlOrSearch+@" ,
                    distance: '"+width+@"'
                });"; 

            }
          
            return new HtmlString(mhtml);
        }

        /// <summary>
        /// 生成表单提交按钮
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="btnTxt">显示的按钮文本</param>
        /// <param name="eventName">事件名称</param>
        /// <returns></returns>
        public static HtmlString FormSubmitButton(this HtmlHelper helper, string btnTxt="提交", string eventName="formSubmit")
        {
            var html = @"<div class='layui-form-item'>
                <div class='layui-input-block'>
                <button lay-submit lay-filter='"+eventName+@"' class='pear-btn pear-btn-danger'>"+btnTxt+@"</button>
                </div>
                </div>";
            return new HtmlString(html);
        }
        /// <summary>
        /// 单选按钮组
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="title"></param>
        /// <param name="groupName"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static HtmlString PearRadioButtonsGroup(this HtmlHelper helper,string title,string groupName,SelectList list)
        {

            var items = "";
            foreach (var item in list)
            {
                items+=$" <input type='radio' name='{groupName}' value='{item.Value}' title='{item.Text}' checked='{(item.Selected?"checked":"")}'><div class='layui-unselect layui-form-radio layui-form-radioed'><i class='layui-anim layui-icon layui-anim-scaleSpring'></i><div>{item.Text}</div></div> ";
            }
            
            var html = @"<div class='layui-form-item'>
                <label class='layui-form-label'>"+title+@"</label>
                <div class='layui-input-block'> 
               "+items+@"
                </div>
                </div>";
            
            return new HtmlString(html);
        }
        /// <summary>
        /// 关闭Modal框，仅用在一层弹Iframe框
       /// </summary>
        /// <param name="helper"></param>
        /// <param name="doneFuc"></param>
        /// <param name="isCloseModal"></param>
        /// <returns></returns>
        public static HtmlString ModalOpenDoneFucAndCloseIframeModal(this HtmlHelper helper, string msg, string doneFuc=null )
        {
            var html = @"if (res.code == 200){
                    window.parent.tools.notice.success('"+msg+@"');
                    window.parent.layui.layer.closeAll();
                    "+(string.IsNullOrEmpty(doneFuc)?"":doneFuc)+@"
                }else{ 
                    window.parent.tools.notice.warning(res.msg);
                }";
            return new HtmlString(html);
        }
        
        /// <summary>
        /// get获取json
        /// </summary> 
        /// <param name="data"></param>
        /// <returns></returns>
        public static JsonResult GetJson(  object data)
        {
            return  new JsonResult()
            {
                Data = data,
                ContentType = null,
                ContentEncoding = null,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }

    public enum PearOpenType
    {
        Modal,RightBox,LeftBox
    }

    public class PearTableCell
    {
        public string field { get; set; }
        public string title { get; set; } 
        public int width { get; set; }

        public bool sort { get; set; }
        public string templet { get; set; }
        public string toolbar { get; set; }
    }
    
}