//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectTermStore
{
    using System;
    using System.Collections.Generic;
    
    public partial class IncomeComStore
    {
        public int iId { get; set; }
        public System.DateTime date { get; set; }
        public string list { get; set; }
        public decimal income { get; set; }
        public int oId { get; set; }
    
        public virtual OrderComStore OrderComStore { get; set; }
    }
}