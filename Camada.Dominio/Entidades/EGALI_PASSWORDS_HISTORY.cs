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
    
    public partial class EGALI_PASSWORDS_HISTORY
    {
        public int ID { get; set; }
        public string Password { get; set; }
        public System.DateTime DT_Reg { get; set; }
        public string LogLogin { get; set; }
        public int ID_EGALI_PASSWORDS { get; set; }
    
        public virtual EGALI_PASSWORDS EGALI_PASSWORDS { get; set; }
    }
}
