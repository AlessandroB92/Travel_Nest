namespace Travel_Nest.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Recensioni")]
    public partial class Recensioni
    {
        [Key]
        public int IDRecensione { get; set; }

        public int? IDUtente { get; set; }

        public int? IDAlloggio { get; set; }

        public string TestoRecensione { get; set; }

        public int? Valutazione { get; set; }

        [Column(TypeName = "date")]
        public DateTime DataRecensione { get; set; }

        public virtual Alloggi Alloggi { get; set; }

        public virtual Utenti Utenti { get; set; }
    }
}
