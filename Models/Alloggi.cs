namespace Travel_Nest.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Alloggi")]
    public partial class Alloggi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Alloggi()
        {
            Prenotazioni = new HashSet<Prenotazioni>();
            Recensioni = new HashSet<Recensioni>();
        }

        [Key]
        public int IDAlloggio { get; set; }

        public int? IDUtente { get; set; }

        [StringLength(100)]
        public string NomeAlloggio { get; set; }

        public string Descrizione { get; set; }

        [StringLength(100)]
        public string Citta { get; set; }

        public decimal? PrezzoPerNotte { get; set; }

        public bool? Disponibilita { get; set; }

        public virtual Utenti Utenti { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prenotazioni> Prenotazioni { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Recensioni> Recensioni { get; set; }
    }
}
