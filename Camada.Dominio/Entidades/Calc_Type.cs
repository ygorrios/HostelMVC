//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Camada.Dominio.Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class Calc_Type
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Calc_Type()
        {
            this.Money_Count = new HashSet<Money_Count>();
        }
    
        public int ID { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> DT_End { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Money_Count> Money_Count { get; set; }
    }
}
