//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace PearAdminMvcOA.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    
    public partial class LeaveMessage
    {
        public int Lid { get; set; }
        public int userid { get; set; }
        public int Luserid { get; set; }
        public string LeaveText { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        [JsonIgnore]
        public virtual UserInfo UserInfo { get; set; }
        [JsonIgnore]
        public virtual UserInfo UserInfo1 { get; set; }
    }
}
