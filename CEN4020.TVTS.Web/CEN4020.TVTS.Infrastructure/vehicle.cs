//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CEN4020.TVTS.Infrastructure
{
    using System;
    using System.Collections.Generic;
    
    public partial class vehicle
    {
        public System.Guid Id { get; set; }
        public string ModelName { get; set; }
        public string StyleTrim { get; set; }
        public string StyleId { get; set; }
        public string Color { get; set; }
        public string Options { get; set; }
        public int Purchased { get; set; }
        public Nullable<System.Guid> Customer { get; set; }
        public string ModelIdName { get; set; }
    }
}
