namespace Travel_Nest.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ImmaginiAlloggi")]
    public partial class ImmaginiAlloggi
    {
        [Key]
        public int IDImmagine { get; set; }

        public int? IDAlloggio { get; set; }

        [StringLength(255)]
        public string URLImmagine { get; set; }

        public byte[] FileData { get; set; }
    }
}
