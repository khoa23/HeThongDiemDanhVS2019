//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HeThongDiemDanh.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class NAMHOC
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NAMHOC()
        {
            this.HOCKies = new HashSet<HOCKY>();
        }
    
        public int IDNAMHOC { get; set; }
        public string MANAMHOC { get; set; }
        public string TENNAMHOC { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOCKY> HOCKies { get; set; }
    }
}
