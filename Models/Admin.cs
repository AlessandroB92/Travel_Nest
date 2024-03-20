namespace Travel_Nest.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Admin")]
    public partial class Admin
    {
        [Key]
        public int IDAdmin { get; set; }

        [StringLength(50)]
        public string Nome { get; set; }

        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        public string Ruolo { get; set; }
    }
}
