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
    
    public partial class Quotation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Quotation()
        {
            this.Quotation_Line = new HashSet<Quotation_Line>();
        }
    
        public int Quotation_ID { get; set; }
        public Nullable<int> Customer_ID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Additional_Notes { get; set; }
        public Nullable<int> Quotation_Status_ID { get; set; }
        public Nullable<decimal> Amount { get; set; }
    
        public virtual Customer Customer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Quotation_Line> Quotation_Line { get; set; }
        public virtual Quotation_Status Quotation_Status { get; set; }
    }
}
