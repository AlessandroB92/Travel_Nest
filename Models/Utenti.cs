namespace Travel_Nest.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Utenti")]
    public partial class Utenti
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Utenti()
        {
            Alloggi = new HashSet<Alloggi>();
            Prenotazioni = new HashSet<Prenotazioni>();
            Recensioni = new HashSet<Recensioni>();
        }

        [Key]
        public int IDUtente { get; set; }

        [StringLength(50)]
        public string Nome { get; set; }

        [StringLength(50)]
        public string Cognome { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        public string Ruolo { get; set; } = "User";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Alloggi> Alloggi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prenotazioni> Prenotazioni { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Recensioni> Recensioni { get; set; }
    }
}
