namespace Travel_Nest.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Prenotazioni")]
    public partial class Prenotazioni
    {
        [Key]
        public int IDPrenotazione { get; set; }

        public int? IDUtente { get; set; }

        public int? IDAlloggio { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DataCheckIn { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DataCheckOut { get; set; }

        [StringLength(20)]
        public string StatoPrenotazione { get; set; }

        public virtual Alloggi Alloggi { get; set; }

        public virtual Utenti Utenti { get; set; }
    }
}
