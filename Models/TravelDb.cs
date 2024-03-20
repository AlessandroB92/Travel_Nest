using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Travel_Nest.Models
{
    public partial class TravelDb : DbContext
    {
        public TravelDb()
            : base("name=TravelDb")
        {
        }

        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Alloggi> Alloggi { get; set; }
        public virtual DbSet<ImmaginiAlloggi> ImmaginiAlloggi { get; set; }
        public virtual DbSet<Prenotazioni> Prenotazioni { get; set; }
        public virtual DbSet<Recensioni> Recensioni { get; set; }
        public virtual DbSet<Utenti> Utenti { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alloggi>()
                .Property(e => e.PrezzoPerNotte)
                .HasPrecision(10, 2);
        }
    }
}
