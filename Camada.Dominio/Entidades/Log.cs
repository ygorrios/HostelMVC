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
    
    public partial class Log
    {
        public int ID { get; set; }
        public string ControllerAction { get; set; }
        public System.DateTime DT_Reg { get; set; }
        public string LogLogin { get; set; }
        public bool Error { get; set; }
        public string Description { get; set; }
        public string ActionParameters { get; set; }
        public int ID_LOG_TYPE { get; set; }
    
        public virtual Log_Type Log_Type { get; set; }
    }
}
