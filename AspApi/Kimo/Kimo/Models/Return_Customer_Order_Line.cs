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
    
    public partial class Return_Customer_Order_Line
    {
        public int Return_Line_ID { get; set; }
        public Nullable<int> Return_Customer_Order_ID { get; set; }
        public Nullable<int> Customer_Order_ID { get; set; }
    
        public virtual Customer_Order_Line Customer_Order_Line { get; set; }
        public virtual Return_Customer_Order Return_Customer_Order { get; set; }
    }
}
