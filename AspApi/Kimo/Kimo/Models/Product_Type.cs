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
    
    public partial class Product_Type
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product_Type()
        {
            this.Designs = new HashSet<Design>();
            this.Supplier_Order_Line = new HashSet<Supplier_Order_Line>();
        }
    
        public int Product_Type_ID { get; set; }
        public Nullable<int> Product_Category_ID { get; set; }
        public Nullable<int> Template_ID { get; set; }
        public string Name { get; set; }
        public string ImageString { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Design> Designs { get; set; }
        public virtual Product_Category Product_Category { get; set; }
        public virtual Template Template { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Supplier_Order_Line> Supplier_Order_Line { get; set; }
    }
}
