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
    using System;
    using System.Collections.Generic;
    
    public partial class FileInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FileInfo()
        {
            this.AccessoryFile = new HashSet<AccessoryFile>();
        }
    
        public int FileId { get; set; }
        public string FileName { get; set; }
        public int FileType { get; set; }
        public string Remark { get; set; }
        public int FileOwner { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<int> ParentId { get; set; }
        public string FilePath { get; set; }
        public int IfDelete { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccessoryFile> AccessoryFile { get; set; }
        public virtual UserInfo UserInfo { get; set; }
        public virtual FileTypeInfo FileTypeInfo { get; set; }
    }
}
