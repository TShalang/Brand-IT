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
    
    public partial class Supplier_Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Supplier_Order()
        {
            this.Supplier_Order_Line = new HashSet<Supplier_Order_Line>();
        }
    
        public int Supplier_Order_ID { get; set; }
        public Nullable<int> Supplier_ID { get; set; }
        public Nullable<int> Supplier_Order_Status_ID { get; set; }
        public Nullable<int> Quotation_ID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<decimal> Amount { get; set; }
    
        public virtual Quotation_Line Quotation_Line { get; set; }
        public virtual Supplier Supplier { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Supplier_Order_Line> Supplier_Order_Line { get; set; }
        public virtual Supplier_Order_Status Supplier_Order_Status { get; set; }
    }
}