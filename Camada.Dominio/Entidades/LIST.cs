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
    
    public partial class LIST
    {
        public int ID { get; set; }
        public int ID_LIST_TYPE { get; set; }
        public Nullable<System.DateTime> CHECK_IN { get; set; }
        public Nullable<System.DateTime> CHECK_OUT { get; set; }
        public Nullable<System.DateTime> DT_START_BLOCK { get; set; }
        public Nullable<System.DateTime> DT_START_BOOK { get; set; }
        public string NOTES { get; set; }
        public string FIRST_NAME_GUEST { get; set; }
        public string LAST_NAME_GUEST { get; set; }
        public string LOGLOGIN { get; set; }
        public Nullable<int> ID_DOCUMENT { get; set; }
    
        public virtual DOCUMENT DOCUMENT { get; set; }
        public virtual LIST_TYPE LIST_TYPE { get; set; }
    }
}
