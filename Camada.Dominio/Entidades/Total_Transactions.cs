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
    
    public partial class Total_Transactions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Total_Transactions()
        {
            this.Stocks = new HashSet<Stock>();
            this.Total_Transactions1 = new HashSet<Total_Transactions>();
            this.Transactions = new HashSet<Transaction>();
        }
    
        public int ID { get; set; }
        public Nullable<decimal> TotalTransactions { get; set; }
        public Nullable<decimal> TotalFinal { get; set; }
        public Nullable<decimal> DifferenceFinalCalc { get; set; }
        public Nullable<System.DateTime> DT_Reg { get; set; }
        public string LogLogin { get; set; }
        public Nullable<int> ID_Calc { get; set; }
        public Nullable<int> ID_Last_Transaction { get; set; }
        public Nullable<int> ID_Report_Type { get; set; }
        public Nullable<decimal> Last_Cashier_Total { get; set; }
        public Nullable<int> ID_SHIFT_TYPE { get; set; }
        public Nullable<System.DateTime> DayMonthYearReference { get; set; }
    
        public virtual Calc Calc { get; set; }
        public virtual Report_Type Report_Type { get; set; }
        public virtual SHIFT_TYPE SHIFT_TYPE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stock> Stocks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Total_Transactions> Total_Transactions1 { get; set; }
        public virtual Total_Transactions Total_Transactions2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}