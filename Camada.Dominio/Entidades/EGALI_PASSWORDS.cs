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
    
    public partial class EGALI_PASSWORDS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EGALI_PASSWORDS()
        {
            this.EGALI_PASSWORDS_HISTORY = new HashSet<EGALI_PASSWORDS_HISTORY>();
        }
    
        public int ID { get; set; }
        public string Item { get; set; }
        public string Login { get; set; }
        public Nullable<System.DateTime> DT_Delete { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EGALI_PASSWORDS_HISTORY> EGALI_PASSWORDS_HISTORY { get; set; }
    }
}