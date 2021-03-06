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
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.BestellingProducts = new HashSet<BestellingProduct>();
        }
    
        public int ProductID { get; set; }
        public string Naam { get; set; }
        public decimal Marge { get; set; }
        public string Eenheid { get; set; }
        public decimal BTW { get; set; }
        public int LeverancierID { get; set; }
        public int CategorieID { get; set; }
        public int AantalOpVooraad { get; set; }
        public Nullable<int> AantalNaBesteld { get; set; }
        public Nullable<int> AantalBesteld { get; set; }
        public Nullable<int> AantalBeschikbaar { get; set; }
        public decimal AankoopPrijs { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BestellingProduct> BestellingProducts { get; set; }
        public virtual Categorie Categorie { get; set; }
        public virtual Leverancier Leverancier { get; set; }
    }
}
