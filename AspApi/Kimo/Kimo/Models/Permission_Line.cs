//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kimo.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Permission_Line
    {
        public int PermissionLineID { get; set; }
        public Nullable<int> PermissionID { get; set; }
        public Nullable<int> UserRoleID { get; set; }
    
        public virtual Permission Permission { get; set; }
        public virtual User_Role User_Role { get; set; }
    }
}
