//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectweekWPF.TussentijdsProjectYannick
{
    using System;
    using System.Collections.Generic;
    
    public partial class AdminRechten
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AdminRechten()
        {
            this.Personeelslids = new HashSet<Personeelslid>();
        }
    
        public int AdminRechtenID { get; set; }
        public string titel { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Personeelslid> Personeelslids { get; set; }
    }
}
